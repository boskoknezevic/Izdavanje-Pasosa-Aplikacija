using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Interfejsi;

namespace SlojPodataka.Repozitorijumi
{
    public class clsTerminRepo : ITerminRepo
    {
        //polje konekcije
        private string _stringKonekcije;

        //konstruktor
        //dobija se string konekcije pri pozivanju
        public clsTerminRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        public DataSet DajSveTermine()
        {
            DataSet dsPodaci = new DataSet("Termini");

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveTermine", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        //izlistava sve pasose i daje njihov datum isteka,
        //ime i prezime korisnika kao i datum i vreme termina
        public List<DateOnly> IzlistajDatume()
        {
            List<DateOnly> datumi = new List<DateOnly>();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("IzlistajDatumeTermina", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            using (SqlDataReader reader = Komanda.ExecuteReader())
            {
                while (reader.Read())
                {

                    if (reader["Datum"] != DBNull.Value)
                    {
                        DateTime datum = (DateTime)reader["Datum"];
                        DateOnly datumDateOnly = DateOnly.FromDateTime(datum);
                        datumi.Add(datumDateOnly);
                    }


                }
            }
            Veza.Close();
            Veza.Dispose();

            return datumi;
        }
    }
}
