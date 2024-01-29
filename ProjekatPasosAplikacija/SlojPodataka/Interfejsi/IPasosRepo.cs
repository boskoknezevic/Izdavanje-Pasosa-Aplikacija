using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface IPasosRepo
    {
        DataSet DajSvePasose();
        DataSet DajPasosPoDatumu(DateOnly datum);
        bool NoviPasosITermin(string jmbg, DateOnly datum, TimeOnly vreme);
        bool ObrisiPasosITermin(int IDTermina);
        public DataSet DajPasosPoJMBG(string jmbg);
    }
}
