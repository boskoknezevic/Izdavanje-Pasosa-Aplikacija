using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlojPodataka
{
    // Class: Korisnik - Upisuje podatke i omogućava registraciju, prijavu i izmenu informacija.

    // Responsibility:
    // - Upisuje podatke o korisniku (JMBG, ime, prezime, državljanstvo, email, lozinka).
    // - Prati tip korisnika (admin ili običan korisnik).
    // - Omogućava registraciju i prijavu.
    // - Omogućava promenu informacija o korisniku.

    // Collaboration:
    // - Sa klasom Zahtev (kreiranje, provera statusa).
    // - Sa klasom Pasos (praćenje izdavanja pasoša korisniku).


    [Table("KORISNIK")]
    public class clsKorisnik
    {
        //polja
        [Key]
        [RegularExpression(@"^[0-9]{13}$")]
        private string _jmbg;
        
        [Required]
        [StringLength(20)]
        private string _ime;
        
        [Required]
        [StringLength(40)]
        private string _prezime;

        [Required]
        [StringLength(40)]
        private string _drzavljanstvo;

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}$")]
        private string _email;

        [Required]
        [StringLength(20)]
        private string _lozinka;

        
        [Range(1,2)]
        private int _tipKorisnika;


        //property
         public string Jmbg { get => _jmbg; set => _jmbg = value; }
        public string Ime { get => _ime; set => _ime = value; }
        public string Prezime { get => _prezime; set => _prezime = value; }
        public string Drzavljanstvo { get => _drzavljanstvo; set => _drzavljanstvo = value; }
        public string Email { get => _email; set => _email = value; }
        public string Lozinka { get => _lozinka; set => _lozinka = value; }
        public int TipKorisnika { get => _tipKorisnika; set => _tipKorisnika = value; }
       
    }
}