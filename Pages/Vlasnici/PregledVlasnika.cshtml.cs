using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Vlasnici
{
    public class PregledVlasnikaModel : PageModel
    {
        public List<InfoVlasnika> ListaVlasnika = new List<InfoVlasnika>();
        public void OnGet()
        {
            string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";

            string sqlQuery = "SELECT * From Vlasnik;";

            SqlConnection con = new SqlConnection(connectionString);

            con.Open();
            using (SqlCommand sc = new SqlCommand(sqlQuery, con))
            {
                using (SqlDataReader reader = sc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InfoVlasnika vlasnika = new InfoVlasnika();
                        vlasnika.ID = "" + reader.GetInt32(0);
                        vlasnika.OIB_vlasnika = reader.GetString(1);
                        vlasnika.Ime = reader.GetString(2);
                        vlasnika.Prezime = reader.GetString(3);
                        vlasnika.Broj_telefona = reader.GetString(4);
                        vlasnika.Adresa = reader.GetString(5);

                        ListaVlasnika.Add(vlasnika);
                    }
                }
            }
            con.Close();
        }
    }
    public class InfoVlasnika
    {
        public string ID;
        public string OIB_vlasnika;
        public string Ime;
        public string Prezime;
        public string Broj_telefona;
        public string Adresa;
    }
}

