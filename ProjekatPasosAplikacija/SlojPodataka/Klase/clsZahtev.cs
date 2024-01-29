using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka
{
    // Class: Zahtev - Prati podatke o zahtevu i statusu korisnika.

    // Responsibility:
    // - Prati podatke o zahtevu (ID, datum i vreme).
    // - Proverava i prati status zahteva.
    // - Povezuje se sa korisnikom (JMBG korisnika).

    // Collaboration:
    // - Sa klasom Korisnik (veza sa JMBG korisnika).
    // - Sa klasom StatusZahteva (praćenje statusa).


    [Table("ZAHTEV")]
    internal class clsZahtev
    {

        //polja
        [Key]
        private int _idZahteva;

        [Required]
        private DateTime _datumIVremeZahteva;

        [Required]
        [RegularExpression(@"^[0 - 9] +$")]
        private string _jmbgKorisnika;

        [Required]
        private int _status;


        //property
        public int IdZahteva { get => _idZahteva; set => _idZahteva = value; }
        public DateTime DatumIVremeZahteva { get => _datumIVremeZahteva; set => _datumIVremeZahteva = value; }
        public string JmbgKorisnika { get => _jmbgKorisnika; set => _jmbgKorisnika = value; }
        public int Status { get => _status; set => _status = value; }
    }
}
