using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Vlasnici
{
    public class UpisVlasnikaModel : PageModel
    {
        public InfoVlasnika infoVlasnika = new InfoVlasnika();
        public string errorMessage = "";
        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            infoVlasnika.OIB_vlasnika = Request.Form["OIB_vlasnika"];
            infoVlasnika.Ime = Request.Form["Ime_vlasnika"];
            infoVlasnika.Prezime = Request.Form["Prezime_vlasnika"];
            infoVlasnika.Broj_telefona = Request.Form["Broj_telefona"];
            infoVlasnika.Adresa = Request.Form["Adresa"];

            if (string.IsNullOrEmpty(infoVlasnika.OIB_vlasnika) || string.IsNullOrEmpty(infoVlasnika.Ime) || string.IsNullOrEmpty(infoVlasnika.Prezime) || string.IsNullOrEmpty(infoVlasnika.Broj_telefona) || string.IsNullOrEmpty(infoVlasnika.Adresa))
            {
                errorMessage = "Sva polja moraju biti popunjena";
                return;
            }
            try
            {
                string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";

                string sqlQuery = "INSERT INTO Vlasnik (OIB_vlasnika, Ime, Prezime, broj_telefona, Adresa) VALUES (" + "'" + infoVlasnika.OIB_vlasnika + "'" + "," + "'" + infoVlasnika.Ime + "'" + "," + "'" + infoVlasnika.Prezime + "'" + "," + "'" + infoVlasnika.Broj_telefona + "'" + "," + "'" + infoVlasnika.Adresa + "'" + ")";

                SqlConnection con = new SqlConnection(connectionString);

                con.Open();
                SqlCommand sc = new SqlCommand(sqlQuery, con);
                sc.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Vlasnici/PregledVlasnika");
        }
    }
}
