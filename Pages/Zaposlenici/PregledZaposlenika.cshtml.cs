using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Zaposlenici
{
    public class PregledZaposlenikaModel : PageModel
    {
        public List<InfoZaposlenika> ListaZaposlenika = new List<InfoZaposlenika>();
        public void OnGet()
        {
            string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";

            string sqlQuery = "SELECT * From Radnik;";

            SqlConnection con = new SqlConnection(connectionString);

            con.Open();
            using (SqlCommand sc = new SqlCommand(sqlQuery, con))
            {
                using (SqlDataReader reader = sc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InfoZaposlenika zaposlenik = new InfoZaposlenika();
                        zaposlenik.ID = "" + reader.GetInt32(0);
                        zaposlenik.OIB_zaposlenika = reader.GetString(1);
                        zaposlenik.Ime = reader.GetString(2);
                        zaposlenik.Prezime = reader.GetString(3);
                        zaposlenik.Adresa = reader.GetString(4);
                        zaposlenik.Email = reader.GetString(5);
                        zaposlenik.ID_servisa = "" + reader.GetInt32(6);
                        zaposlenik.Broj_telefona = reader.GetString(7);

                        ListaZaposlenika.Add(zaposlenik);
                    }
                }
            }
            con.Close();
        }
    }
    public class InfoZaposlenika
    {
        public string ID;
        public string OIB_zaposlenika;
        public string Ime;
        public string Prezime;
        public string Broj_telefona;
        public string Adresa;
        public string ID_servisa;
        public string Email;
    }
}

