using Auto_Servis.Pages.Ra�uni;
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

            string sqlQuery = "SELECT Ra�un.ID_racuna, Ra�un.�ifra_ra�una, Ra�un.Naziv, Ra�un.Opis, Ra�un.Kori�teni_materijal, Ra�un.Ukupna_cijena, Ra�un.Cijena_dijelova, Ra�un.pdv, Ra�un.Datum_upisa, Vlasnik.Ime + ' ' + Vlasnik.Prezime AS 'Ime i prezime vlasnika', Servis_automobila.Naziv AS 'Naziv_servisa'  FROM ((Ra�un INNER JOIN Vlasnik ON Ra�un.ID_vlasnika = Vlasnik.ID) INNER JOIN Servis_automobila ON Ra�un.ID_servisa = Servis_automobila.ID);";

            SqlConnection con = new SqlConnection(connectionString);

            con.Open();
            using (SqlCommand sc = new SqlCommand(sqlQuery, con))
            {
                using (SqlDataReader reader = sc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        infoRacuna.ID = "" + reader.GetInt32(0);
                        infoRacuna.�ifra_racuna = "" + reader.GetInt32(1);
                        infoRacuna.Naziv = reader.GetString(2);
                        infoRacuna.Opis = reader.GetString(3);
                        infoRacuna.Kori�teni_materijal = reader.GetString(4);
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
