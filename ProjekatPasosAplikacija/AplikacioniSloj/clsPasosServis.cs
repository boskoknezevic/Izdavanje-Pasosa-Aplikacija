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
    public class clsPasosServis
    {
        private IPasosRepo _repo;
        private clsPoslovnaPravila _poslovnaPravila;

        //konstruktor
        public clsPasosServis(IPasosRepo repo, clsPoslovnaPravila poslovnaPravila)
        {
            _repo = repo;
            _poslovnaPravila = poslovnaPravila;
        }


        //izlistava sve pasose i daje njihov datum isteka,
        //ime i prezime korisnika kao i datum i vreme termina
        public DataSet Prikazi()
        {
            return _repo.DajSvePasose();
        }

        public DataSet PrikaziPoJMBG(string jmbg)
        {
            return _repo.DajPasosPoJMBG(jmbg);
        }

        //filtrira pasose po datumu
        public DataSet PrikaziPoDatumu(DateOnly datum)
        {
            return _repo.DajPasosPoDatumu(datum);
        }

        public bool Obrisi(int IDTermina)
        {
           return _repo.ObrisiPasosITermin(IDTermina);
        }
    }
}
