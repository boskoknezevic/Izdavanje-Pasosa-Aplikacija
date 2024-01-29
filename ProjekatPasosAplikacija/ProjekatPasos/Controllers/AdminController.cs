using AplikacioniSloj;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlojPodataka;
using System.Data;

namespace PrezentacioniSloj.Controllers
{
    public class AdminController : Controller
    {
        private readonly clsKorisnikServis _korisnikServis;
        private readonly clsPasosServis _pasosServis;
        private readonly clsTerminServis _terminServis;
        private readonly clsZahtevServis _zahtevServis;

        public AdminController(clsKorisnikServis korisnikServis, clsPasosServis pasosServis, clsTerminServis terminServis, clsZahtevServis zahtevServis)
        {
            _korisnikServis = korisnikServis;
            _pasosServis = pasosServis;
            _terminServis = terminServis;
            _zahtevServis = zahtevServis;
        }

        public IActionResult AdminPocetna()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminPregledKorisnika(string prezime)
        {
            DataSet rezultat;

            if (!string.IsNullOrEmpty(prezime))
            {
                rezultat = _korisnikServis.PrikaziPoPrezimenu(prezime);
            }
            else
            {
                rezultat = _korisnikServis.Prikazi();
            }

            return View(rezultat);
        }


        [HttpGet]
        public IActionResult AdminPregledPasosa(string datum)
        {
            if (string.IsNullOrEmpty(datum))
            {
                DataSet rezultat = _pasosServis.Prikazi();
                ViewBag.ListaDatuma = new SelectList(_terminServis.Izlistaj());
                return View(rezultat);
            }
            else if (DateOnly.TryParse(datum, out DateOnly izabraniDatum))
            {
                DataSet rezultat = _pasosServis.PrikaziPoDatumu(izabraniDatum);
                ViewBag.ListaDatuma = new SelectList(_terminServis.Izlistaj());
                ViewBag.IzabraniDatum = izabraniDatum;
                return View(rezultat);
            }
            else
            {
                // Handle nevalidan datum
                return BadRequest("Nevalidan datum");
            }
        }



        public IActionResult AdminPregledTermina()
        {
            DataSet rezultat = _terminServis.Prikazi();

            return View(rezultat);
        }

        [HttpGet]
        public IActionResult AdminPregledZahteva(string opis)
        {
            if (string.IsNullOrEmpty(opis))
            { DataSet rezultat = _zahtevServis.Prikazi();
            return View(rezultat); } 
            else {
                int status;
                if (opis == "odbijen") status = 1;
                else if (opis == "odobren") status = 3;
                else status = 2;
                DataSet rezultat = _zahtevServis.PrikaziPoStatusu(status);
                return View(rezultat);
            }
        }

        [HttpPost]
        public IActionResult UpravljajZahtevima(string IDZahteva, string action, string jmbg)
        {
            if (action == "odobri")
            {
                _zahtevServis.Odobri(IDZahteva, jmbg);
            }
            else if (action == "odbij")
            {
                _zahtevServis.Odbij(IDZahteva);
            }

            return RedirectToAction("AdminPregledZahteva");
        }


        public IActionResult AdminPregledKorisnikaDetalji()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IzmeniKorisnika(string? email, string? jmbg, string? action)
        {
            if (!string.IsNullOrEmpty(email))
            {
                if (action == "izmeni")
                {
                    clsKorisnik korisnik = _korisnikServis.PronadjiPoEmail(email);
                    return View("AdminPregledKorisnikaDetalji", korisnik);
                }
                else if (action == "obrisi")
                {
                    _korisnikServis.Obrisi(jmbg);
                }
                else if (action == "dodeliAdmina")
                {
                    clsKorisnik korisnik = _korisnikServis.PronadjiPoEmail(email);
                    _korisnikServis.DodeliAdminUlogu(korisnik);
                }
                else if (action == "oduzmiAdmina")
                {
                    clsKorisnik korisnik = _korisnikServis.PronadjiPoEmail(email);
                    _korisnikServis.OduzmiAdminUlogu(korisnik);
                }
            }

            // Preusmeri nazad na AdminPregledKorisnika
            return RedirectToAction("AdminPregledKorisnika");
        }

        [HttpPost]
        public IActionResult IzmeniPodatke(string action, clsKorisnik model, string StariJMBG)
        {
            if (action == "izmeni")
            {
                string stariJMBG = StariJMBG;

                _korisnikServis.Izmeni(stariJMBG, model);

                return RedirectToAction("AdminPocetna"); 
            }

            return View(); 
        }


        public IActionResult AdminTerminStampa()
        {
                DataSet dataSet = _terminServis.Prikazi();
                if (dataSet != null)
                    return View("AdminTerminStampa", dataSet);
                else return RedirectToAction("AdminPregledTermina");
        }
    }
}
