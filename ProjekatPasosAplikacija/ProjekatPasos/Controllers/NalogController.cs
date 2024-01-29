using AplikacioniSloj;
using Microsoft.AspNetCore.Mvc;
using SlojPodataka;

public class NalogController : Controller
{
    private readonly clsKorisnikServis _korisnikServis;

    public NalogController(clsKorisnikServis korisnikServis)
    {
        _korisnikServis = korisnikServis;
    }

    [HttpGet]
    public ActionResult Registracija()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Registracija(RegistracijaModel model)
    {
        if (ModelState.IsValid)
        {
            bool uspesnaRegistracija = _korisnikServis.Dodaj(new clsKorisnik
            {
                Jmbg = model.JMBG,
                Ime = model.Ime,
                Prezime = model.Prezime,
                Drzavljanstvo = model.Drzavljanstvo,
                Email = model.Email,
                Lozinka = model.Lozinka
            });

            if (uspesnaRegistracija)
            {
                // Ako je registracija uspešna, preusmerite korisnika na odgovarajući view ili akciju
                return RedirectToAction("Prijava");
            }
            else
            {
                // Ako registracija nije uspela, možete dodati odgovarajuću logiku ili poruku
                ModelState.AddModelError(string.Empty, "Регистрација није успешна. Молимо покушајте поново.");
            }
        }

        // Ako ModelState nije validan, vraća se isti view sa postojećim podacima
        return View(model);
    }

    [HttpGet]
    public ActionResult Prijava()
    {
        return View();
    }
    [HttpPost]
    public ActionResult Prijava(PrijavaModel model)
    {
        if (ModelState.IsValid)
        {
            // Pozovite metodu iz servisa koja proverava korisničke podatke
            var prijavljeniKorisnik = _korisnikServis.PronadjiPoEmail(model.Email);

            if (prijavljeniKorisnik != null)
            {
                // Ako je pronađen korisnik sa datom e-poštom, proverite lozinku
                if (prijavljeniKorisnik.Lozinka == model.Lozinka)
                {
                    // Lozinka je ispravna, postavite korisničke podatke u sesiju
                    HttpContext.Session.SetString("JMBG", prijavljeniKorisnik.Jmbg);
                    HttpContext.Session.SetString("Ime", prijavljeniKorisnik.Ime);
                    HttpContext.Session.SetString("Prezime", prijavljeniKorisnik.Prezime);
                    HttpContext.Session.SetString("Email", prijavljeniKorisnik.Email);
                    HttpContext.Session.SetString("Lozinka", prijavljeniKorisnik.Lozinka);
                    HttpContext.Session.SetString("Drzavljanstvo", prijavljeniKorisnik.Drzavljanstvo);
                    HttpContext.Session.SetInt32("TipKorisnika", prijavljeniKorisnik.TipKorisnika);

                    // Redirekcija na odgovarajući view u zavisnosti od tipa korisnika
                    if (prijavljeniKorisnik.TipKorisnika == 1)
                    {
                        return RedirectToAction("AdminPocetna", "Admin");
                    }
                    else if (prijavljeniKorisnik.TipKorisnika == 2)
                    {
                        return RedirectToAction("KorisnikPocetna", "Korisnik");
                    }
                }
                else
                {
                    // Pogrešna lozinka
                    ModelState.AddModelError(string.Empty, "Погрешна лозинка");
                }
            }
            else
            {
                // Nema korisnika sa datom e-poštom
                ModelState.AddModelError(string.Empty, "Нема корисника у бази података са тим имејлом!");
            }
        }

        // Ako ModelState nije validan, vraća se isti view sa postojećim podacima
        return View(model);
    }

}
