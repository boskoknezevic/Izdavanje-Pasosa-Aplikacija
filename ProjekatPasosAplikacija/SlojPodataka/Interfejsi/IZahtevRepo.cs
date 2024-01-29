using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface IZahtevRepo
    {
        DataSet DajSveZahteve();
        DataSet DajSveZahtevePoStatusu(int status);
        bool NoviZahtev(string jmbg);
        bool ObrisiZahtev(string IDZahteva);
        bool OdbijZahtev(string IDZahteva);
        bool OdobriZahtev(string IDZahteva);
        public DataSet DajSveZahtevePoJMBG(string jmbg);
    }
}
