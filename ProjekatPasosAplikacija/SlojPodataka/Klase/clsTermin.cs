using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka
{
    // Class: Termin - Prati podatke o terminu i omogućava pregled i odabir dostupnih termina.

    // Responsibility:
    // - Prati podatke o terminu (ID, datum i vreme).
    // - Omogućava pregled i odabir dostupnih termina za preuzimanje pasoša.

    // Collaboration:
    // - Sa klasom Pasos (povezivanje sa ID termina).


    [Table("TERMIN")]
    internal class clsTermin
    {

        //polja
        [Key]
        private int _idTermina;

        [Required]
        private DateOnly _datumTermina;

        [Required]
        private TimeOnly _vremeTermina;


        //property

        public int IdTermina { get => _idTermina; set => _idTermina = value; }
        public DateOnly DatumTermina { get => _datumTermina; set => _datumTermina = value; }
        public TimeOnly VremeTermina { get => _vremeTermina; set => _vremeTermina = value; }

    }
}
