using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka
{
    // Class: Pasos - Prati podatke o pasošu, korisniku i terminu.

    // Responsibility:
    // - Prati podatke o pasošu (ID, datum isteka).
    // - Povezuje se sa korisnikom (JMBG korisnika).
    // - Povezuje se sa terminom za preuzimanje pasoša.

    // Collaboration:
    // - Sa klasom Korisnik (veza sa JMBG korisnika).
    // - Sa klasom Termin (veza sa ID termina).


    [Table("PASOS")]
    internal class clsPasos
    {
        //polja
        [Key]
        private int _idPasosa;

        [Required]
        [RegularExpression(@"^[0 - 9] +$")]
        private string _jmbgKorisnika;

        [Required]
        private int _idTermina;

        [Required]
        private DateOnly _datumIsteka;



        //property
        public int IdPasosa { get => _idPasosa; set => _idPasosa = value; }
        public string JmbgKorisnika { get => _jmbgKorisnika; set => _jmbgKorisnika = value; }
        public int IdTermina { get => _idTermina; set => _idTermina = value; }
        public DateOnly DatumIsteka { get => _datumIsteka; set => _datumIsteka = value; }
        
    }
}
