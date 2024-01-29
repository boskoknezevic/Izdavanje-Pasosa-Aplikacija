using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Interfejsi;
using DomenskiSloj;

namespace AplikacioniSloj
{
    public class clsTerminServis
    {
        private ITerminRepo _repo;
        private clsPoslovnaPravila _poslovnaPravila;

        //konstruktor
        public clsTerminServis(ITerminRepo repo, clsPoslovnaPravila poslovnaPravila)
        {
            _repo = repo;
            _poslovnaPravila = poslovnaPravila;
        }


        public DataSet Prikazi()
        {
            return _repo.DajSveTermine();
        }

        //izlistava sve pasose i daje njihov datum isteka,
        //ime i prezime korisnika kao i datum i vreme termina
        public List<DateOnly> Izlistaj()
        {
            return _repo.IzlistajDatume();
        }
    }
}
