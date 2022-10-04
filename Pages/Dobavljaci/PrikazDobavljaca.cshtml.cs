using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Dobavljaci
{
    public class PrikazDobavljacaModel : PageModel
    {
        public List<InfoDobavljaca> ListaDobavljaca = new List<InfoDobavljaca>();
        public void OnGet()
        {
            string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";

            string sqlQuery = "SELECT ID, Šifra_dobavljaèa, Naziv, Adresa, Broj_telefona, Web_stranica FROM Dobavljaè;";

            SqlConnection con = new SqlConnection(connectionString);

            con.Open();
            using (SqlCommand sc = new SqlCommand(sqlQuery, con))
            {
                using (SqlDataReader reader = sc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InfoDobavljaca dobavljac = new InfoDobavljaca();
                        dobavljac.ID = "" + reader.GetInt32(0);
                        dobavljac.Šifra_dobavljaèa = "" + reader.GetInt32(1);
                        dobavljac.Naziv = reader.GetString(2);
                        dobavljac.Adresa = reader.GetString(3);
                        dobavljac.Broj_telefona = reader.GetString(4);
                        dobavljac.Web_stranica = reader.GetString(5);

                        ListaDobavljaca.Add(dobavljac);
                    }
                }
            }
            con.Close();
        }
        public class InfoDobavljaca
        {
            public string ID;
            public string Šifra_dobavljaèa;
            public string Naziv;
            public string Broj_telefona;
            public string Adresa;
            public string Web_stranica;
        }
    }
}
