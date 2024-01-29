using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using AplikacioniSloj;
using SlojPodataka;
using System.Data;

namespace PrezentacioniSloj.Controllers
{
    public class KorisnikController : Controller
    {

        private readonly clsKorisnikServis _korisnikServis;
        private readonly clsZahtevServis _zahtevServis;
        private readonly clsPasosServis _pasosServis;

        public KorisnikController(clsKorisnikServis korisnikServis, clsZahtevServis zahtevServis, clsPasosServis pasosServis)
        {
            _korisnikServis = korisnikServis;
            _zahtevServis = zahtevServis;
            _pasosServis = pasosServis;
        }
        public IActionResult KorisnikPocetna()
        {
            return View();
        }

        public IActionResult KorisnikProfil()
        {
            // Dobijanje podataka iz sesije
            var jmbg = HttpContext.Session.GetString("JMBG");
            var ime = HttpContext.Session.GetString("Ime");
            var prezime = HttpContext.Session.GetString("Prezime");
            var lozinka = HttpContext.Session.GetString("Lozinka");
            var email = HttpContext.Session.GetString("Email");

            var drzavljanstvo = HttpContext.Session.GetString("Drzavljanstvo");

            // Kreiranje modela sa podacima iz sesije
            var model = new RegistracijaModel
            {
                JMBG = jmbg,
                Ime = ime,
                Prezime = prezime,
                Email = email,
                Lozinka = lozinka,
                Drzavljanstvo = drzavljanstvo

            };

            return View(model);
        }

        public IActionResult KorisnikZahtev()
        {
            var jmbg = HttpContext.Session.GetString("JMBG");
            DataSet rezultat = _zahtevServis.PrikaziPoJMBG(jmbg);

            return View(rezultat);
        }

        [HttpPost]
        public IActionResult DodajZahtev(string action)
        {
            if (action == "dodaj")
            {
                var jmbg = HttpContext.Session.GetString("JMBG");
                if (!string.IsNullOrEmpty(jmbg))
                {
                    if (_zahtevServis.Dodaj(jmbg))
                        return RedirectToAction("KorisnikPocetna");
                    else return RedirectToAction("KorisnikZahtev");
                }
                else return RedirectToAction("KorisnikZahtev");
            }
            return RedirectToAction("KorisnikZahtev");
        }

        [HttpPost]
        public IActionResult IzmeniPodatke(RegistracijaModel model, string action)
        {

            if (action == "izmeni")
            {
                // Dobavi JMBG korisnika iz sesije
                var jmbgIzSesije = HttpContext.Session.GetString("JMBG");

                if (!string.IsNullOrEmpty(jmbgIzSesije))
                {
                    clsKorisnik korisnik = new clsKorisnik();
                    korisnik.Jmbg = model.JMBG;
                    korisnik.Ime = model.Ime;
                    korisnik.Prezime = model.Prezime;
                    korisnik.Email = model.Email;
                    korisnik.Lozinka = model.Lozinka;
                    korisnik.Drzavljanstvo = model.Drzavljanstvo;

                    if (_korisnikServis.Izmeni(jmbgIzSesije, korisnik))
                        return RedirectToAction("KorisnikPocetna");
                    return View();
                }
                return View();

            }

            else if (action == "obrisi")
            {   
                var jmbg = HttpContext.Session.GetString("JMBG");

                if (!string.IsNullOrEmpty(jmbg))
                {
                    if (_korisnikServis.Obrisi(jmbg))
                        return RedirectToAction("Pocetna", "Home");
                    return View();
                }
                return View();
            }
            return View();
        }

        public IActionResult KorisnikPasosStampa()
        {
            var jmbg = HttpContext.Session.GetString("JMBG");
            if (!string.IsNullOrEmpty(jmbg))
                { DataSet dataSet = _pasosServis.PrikaziPoJMBG(jmbg);
                if (dataSet != null)
                    return View("KorisnikPasosStampa", dataSet);
                else return RedirectToAction("KorisnikProfil");
            }
            return RedirectToAction("KorisnikZahtev");
        }
    }
}
