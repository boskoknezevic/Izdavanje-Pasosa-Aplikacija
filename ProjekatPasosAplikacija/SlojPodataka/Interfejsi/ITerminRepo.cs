using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface ITerminRepo
    {
        DataSet DajSveTermine();
        List<DateOnly> IzlistajDatume();
    }
}
