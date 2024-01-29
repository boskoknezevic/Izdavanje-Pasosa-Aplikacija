using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface IKorisnikRepo
    {
        DataSet DajSveKorisnike();
        DataSet DajKorisnikaPoPrezimenu(string Prezime);
        bool NoviKorisnik(clsKorisnik objNoviKorisnik);
        bool ObrisiKorisnika(string JMBG);
        bool IzmeniKorisnika(string StariJMBG, clsKorisnik objNoviKorisnik);
        bool IzmeniKorisnika(clsKorisnik objKorisnik);
        clsKorisnik PronadjiPoEmail(string email);
    }
}
