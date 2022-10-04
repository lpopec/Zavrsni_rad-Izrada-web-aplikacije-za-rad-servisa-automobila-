using Auto_Servis.Pages.Raèuni;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace Auto_Servis.Pages.Racuni
{
    public class IspisRacunaModel : PageModel
    {
        public InfoRacuna infoRacuna = new InfoRacuna();
        public void OnGet()
        {
            string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";

            string sqlQuery = "SELECT Raèun.ID_racuna, Raèun.Šifra_raèuna, Raèun.Naziv, Raèun.Opis, Raèun.Korišteni_materijal, Raèun.Ukupna_cijena, Raèun.Cijena_dijelova, Raèun.pdv, Raèun.Datum_upisa, Vlasnik.Ime + ' ' + Vlasnik.Prezime AS 'Ime i prezime vlasnika', Servis_automobila.Naziv AS 'Naziv_servisa'  FROM ((Raèun INNER JOIN Vlasnik ON Raèun.ID_vlasnika = Vlasnik.ID) INNER JOIN Servis_automobila ON Raèun.ID_servisa = Servis_automobila.ID);";

            SqlConnection con = new SqlConnection(connectionString);

            con.Open();
            using (SqlCommand sc = new SqlCommand(sqlQuery, con))
            {
                using (SqlDataReader reader = sc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        infoRacuna.ID = "" + reader.GetInt32(0);
                        infoRacuna.Šifra_racuna = "" + reader.GetInt32(1);
                        infoRacuna.Naziv = reader.GetString(2);
                        infoRacuna.Opis = reader.GetString(3);
                        infoRacuna.Korišteni_materijal = reader.GetString(4);
                        infoRacuna.Ukupna_cijena = "" + reader.GetDecimal(5);
                        infoRacuna.Cijena_dijelova = "" + reader.GetDecimal(6);
                        infoRacuna.pdv = "" + reader.GetInt32(7);
                        DateTime datum = reader.GetDateTime(8);
                        infoRacuna.Datum_unosa = "" + DateOnly.FromDateTime(datum);
                        infoRacuna.Ime_prezime_vlasnika = reader.GetString(9);
                        infoRacuna.Naziv_servisa = reader.GetString(10);
                    }
                }
            }
            con.Close();
        }
    }
}
