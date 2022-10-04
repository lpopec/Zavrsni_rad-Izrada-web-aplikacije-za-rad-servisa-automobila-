using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Računi
{
    public class PrikazRacunaModel : PageModel
    {
        public List<InfoRacuna> ListaRacuna = new List<InfoRacuna>();
        public void OnGet()
        {
            string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";

            string sqlQuery = "SELECT Račun.ID_racuna, Račun.Šifra_računa, Račun.Naziv, Račun.Opis, Račun.Korišteni_materijal, Račun.Ukupna_cijena, Račun.Cijena_dijelova, Račun.pdv, Račun.Datum_upisa, Vlasnik.Ime + ' ' + Vlasnik.Prezime AS 'Ime i prezime vlasnika', Servis_automobila.Naziv AS 'Naziv_servisa'  FROM ((Račun INNER JOIN Vlasnik ON Račun.ID_vlasnika = Vlasnik.ID) INNER JOIN Servis_automobila ON Račun.ID_servisa = Servis_automobila.ID);";

            SqlConnection con = new SqlConnection(connectionString);

            con.Open();
            using (SqlCommand sc = new SqlCommand(sqlQuery, con))
            {
                using (SqlDataReader reader = sc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InfoRacuna racun = new InfoRacuna();
                        racun.ID = "" + reader.GetInt32(0);
                        racun.Šifra_racuna = "" + reader.GetInt32(1);
                        racun.Naziv = reader.GetString(2);
                        racun.Opis = reader.GetString(3);
                        racun.Korišteni_materijal = reader.GetString(4);
                        racun.Ukupna_cijena = "" + reader.GetDecimal(5);
                        racun.Cijena_dijelova = "" + reader.GetDecimal(6);
                        racun.pdv = "" + reader.GetInt32(7);
                        DateTime datum = reader.GetDateTime(8);
                        racun.Datum_unosa = "" + DateOnly.FromDateTime(datum);
                        racun.Ime_prezime_vlasnika = reader.GetString(9);
                        racun.Naziv_servisa = reader.GetString(10);

                        ListaRacuna.Add(racun);
                    }
                }
            }
            con.Close();
        }
    }
    public class InfoRacuna
    {
        public string ID;
        public string Šifra_racuna;
        public string Naziv;
        public string Opis;
        public string Korišteni_materijal;
        public string ID_vlasnika;
        public string ID_servisa;
        public string Ime_prezime_vlasnika;
        public string Naziv_servisa;
        public string Datum_unosa;
        public string Ukupna_cijena;
        public string Cijena_dijelova;
        public string pdv_postotak;
        public string pdv;
        public string OIB_vlasnika;
        public DateTime samo_datum;
    }
}

