
using SlojPodataka;
using SlojPodataka.Interfejsi;
using System.Data;
using System.Xml.Linq;

namespace DomenskiSloj
{
    public class clsPoslovnaPravila
    {
        private IKorisnikRepo _repoKorisnik;
        private IZahtevRepo _repoZahtev;
        private ITerminRepo _repoTermin;

        //konstruktor
        //dobija se string konekcije pri pozivanju
        public clsPoslovnaPravila(IKorisnikRepo repoKorisnik, IZahtevRepo repoZahtev, ITerminRepo repoTermin)
        {
            _repoKorisnik = repoKorisnik;
            _repoZahtev= repoZahtev;
            _repoTermin = repoTermin;
        }

        //proverava se da li moze korisnik da se unapredi u admina,
        //maks moze biti 2 admina (prvobitni iz baze + 1 unapredjeni)
        public bool ProveraMogucnostiDodeljivanjaAdmina()
        {
            bool proveraUspesnosti = false;
            // ucitavanje XML fajla
            XDocument doc = XDocument.Load(@"C:\Users\bosko\source\repos\ProjekatPasosAplikacija\XMLOgranicenja\MaksimumBrojAdmina.xml");

            // pronalaženje maksimalnog broja korisnika za tip 1
            XElement tipKorisnikaElement = doc.Descendants("tipKorisnika").FirstOrDefault(e => e.Attribute("id")?.Value == "1");

            int maksimum = Convert.ToInt32(tipKorisnikaElement.Attribute("maksimum")?.Value);

            DataSet korisnici = _repoKorisnik.DajSveKorisnike();
            

            int? brojPostojecihAdmina = korisnici.Tables[0]
            .AsEnumerable()
            .Count(row => row.Field<string>("TipKorisnikaOpis") == "admin");

            if (brojPostojecihAdmina != null && brojPostojecihAdmina < maksimum)
            {
                proveraUspesnosti = true;
            }

            return proveraUspesnosti;
        }

        //provera da li korisnik moze poslati zahtev,
        //jer u slucaju da jos nije poslao ili je odbijen on opet moze da posalje
        public bool ProveraZahteva(string jmbg)
        {
            bool proveraUspesnosti = false;

            DataSet dsPodaci = _repoZahtev.DajSveZahteve();

            if (dsPodaci != null)
            {
                var rezultat = from DataRow row in dsPodaci.Tables[0].AsEnumerable()
                               where row.Field<string>("JMBGKorisnika") == jmbg
                               select row;

                if (rezultat != null)
                {
                    DataRow[] nizRedova = rezultat.ToArray();

                    if (nizRedova.Length == 0)
                    {
                        proveraUspesnosti = true;
                    }
                    else
                    {
                        DataRow najskorijiZahtev = nizRedova[0];

                        foreach (DataRow red in nizRedova)
                        {
                            if (red.Field<int>("IDZahteva") > najskorijiZahtev.Field<int>("IDZahteva"))
                            {
                                najskorijiZahtev = red;
                            }
                        }

                        // ucitavanje XML fajla sa poslovnim pravilima
                        XDocument doc = XDocument.Load(@"C:\Users\bosko\source\repos\ProjekatPasosAplikacija\XMLOgranicenja\DozvoljeniStatusZahteva.xml");

                        XElement statusElement = doc.Descendants("statusZahteva")
                                                    .FirstOrDefault(e => e.Attribute("opis")?.Value == "Odbijen");

                        if (statusElement != null)
                        {
                            int idOdbijenogIzXml = int.Parse(statusElement.Attribute("id")?.Value);

                            if (najskorijiZahtev.Field<int>("IDZahteva") == idOdbijenogIzXml)
                            {
                                proveraUspesnosti = true;
                            }
                        }
                    }
                }
            }

            return proveraUspesnosti;
        }


        //kad se prihvati zahtev, treba da se izracuna kad moze da se zakaze termin
        public DateTime ZakazivanjeTermina()
        {
            // dohvati sve termine
            DataSet sviTermini = _repoTermin.DajSveTermine();

            DateOnly noviTerminDatum;
            TimeOnly noviTerminVreme;

            if (sviTermini.Tables.Count > 0 && sviTermini.Tables[0].Rows.Count > 0)
            {
                // pronadji najnoviji termin
                DataRow naskorijiTermin = sviTermini.Tables[0].AsEnumerable()
                    .OrderByDescending(row => row.Field<int>("IDTermina"))
                    .First();

                // proveri vreme izvucenog termina
                TimeSpan vremeNajskorijegTermina = naskorijiTermin.Field<TimeSpan>("Vreme");
                TimeOnly timeOnlyVremeNajskorijegTermina = TimeOnly.FromTimeSpan(vremeNajskorijegTermina);

                DateTime datumNajskorijegTermina = naskorijiTermin.Field<DateTime>("Datum");
                DateOnly dateOnlyDatumNajskorijegTermina = DateOnly.FromDateTime(datumNajskorijegTermina.Date);


                if (timeOnlyVremeNajskorijegTermina == TimeOnly.Parse("13:45"))
                {
                    // ako je vreme 13:45, postavi vreme 15 minuta nakon izvucenog termina
                    noviTerminDatum = naskorijiTermin.Field<DateOnly>("Datum").AddDays(1);

                    if (noviTerminDatum.DayOfWeek == DayOfWeek.Saturday)
                    {
                        noviTerminDatum.AddDays(2);
                    }

                    noviTerminVreme = new TimeOnly(8, 0);
                }
                else
                {
                    noviTerminDatum = dateOnlyDatumNajskorijegTermina;
                    noviTerminVreme = timeOnlyVremeNajskorijegTermina.AddMinutes(15);
                }
            }
            else
            {
                DateOnly danasnjiDatum = DateOnly.FromDateTime(DateTime.Now);
                noviTerminDatum = danasnjiDatum.AddDays(1);

                if (noviTerminDatum.DayOfWeek == DayOfWeek.Saturday)
                {
                    noviTerminDatum = danasnjiDatum.AddDays(2);
                }
                else if (noviTerminDatum.DayOfWeek == DayOfWeek.Sunday)
                {
                    noviTerminDatum = danasnjiDatum.AddDays(1);
                }

                noviTerminVreme = new TimeOnly(8, 0);
            }

            DateOnly datum = noviTerminDatum;
            TimeOnly vreme = noviTerminVreme;
            DateTime datumIVreme = datum.ToDateTime(vreme);

            return datumIVreme;
        }
    }
}