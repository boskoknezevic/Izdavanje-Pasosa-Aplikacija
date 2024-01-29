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
    public class clsZahtevServis
    {
        private IZahtevRepo _repoZahtev;
        private IPasosRepo _repoPasos;
        private clsPoslovnaPravila _poslovnaPravila;

        //konstruktor
        public clsZahtevServis(IZahtevRepo repo, IPasosRepo pasosRepo, clsPoslovnaPravila poslovnaPravila)
        {
            _repoZahtev = repo;
            _poslovnaPravila = poslovnaPravila;
            _repoPasos= pasosRepo;
        }

        public DataSet Prikazi()
        {
            return _repoZahtev.DajSveZahteve();
        }

        public DataSet PrikaziPoStatusu(int status)
        {
            return _repoZahtev.DajSveZahtevePoStatusu(status);

        }

        public bool Dodaj(string jmbg)
        {
            if(_poslovnaPravila.ProveraZahteva(jmbg))
                return _repoZahtev.NoviZahtev(jmbg);
            else return false;
        }

        public bool Obrisi(string IDZahteva)
        {
           return _repoZahtev.ObrisiZahtev(IDZahteva);
        }

        public bool Odbij(string IDZahteva)
        {
            return _repoZahtev.OdbijZahtev(IDZahteva);
        }

        public bool Odobri(string IDZahteva, string jmbg)
        {
            if (_repoZahtev.OdobriZahtev(IDZahteva))
            {
                DateTime datumIVreme = _poslovnaPravila.ZakazivanjeTermina();
                DateOnly datum = DateOnly.FromDateTime(datumIVreme);
                TimeOnly vreme = TimeOnly.FromDateTime(datumIVreme);
                if (_repoPasos.NoviPasosITermin(jmbg, datum, vreme))
                { return true; }
                else return false; 
            }
            else
                return false; 
        }

        public DataSet PrikaziPoJMBG(string jmbg)
        {
            return _repoZahtev.DajSveZahtevePoJMBG(jmbg);

        }
    }
}
