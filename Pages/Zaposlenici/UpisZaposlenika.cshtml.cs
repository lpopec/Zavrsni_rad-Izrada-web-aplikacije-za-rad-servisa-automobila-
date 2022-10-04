using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Zaposlenici
{
    public class UpisZaposlenikaModel : PageModel
    {
        public InfoZaposlenika infoZaposlenika = new InfoZaposlenika();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {

        }

        public void OnPost()
        {
            infoZaposlenika.OIB_zaposlenika = Request.Form["OIB_zaposlenika"];
            infoZaposlenika.Ime = Request.Form["Ime"];
            infoZaposlenika.Prezime = Request.Form["Prezime"];
            infoZaposlenika.Broj_telefona = Request.Form["Broj_telefona"];
            infoZaposlenika.Adresa = Request.Form["Adresa"];
            infoZaposlenika.Email = Request.Form["Email"];

            if (string.IsNullOrEmpty(infoZaposlenika.OIB_zaposlenika) || string.IsNullOrEmpty(infoZaposlenika.Ime) || string.IsNullOrEmpty(infoZaposlenika.Prezime) || string.IsNullOrEmpty(infoZaposlenika.Broj_telefona) || string.IsNullOrEmpty(infoZaposlenika.Adresa) ||string.IsNullOrEmpty(infoZaposlenika.Email))
            {
                errorMessage = "Sva polja moraju biti popunjena";
                return;
            }
            try
            {
                string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";

                string sqlQuery = "INSERT INTO Radnik(OIB_radnika, Ime, Prezime, Adresa, Broj_telefona, Email, ID_servisa) VALUES (" + "'" + infoZaposlenika.OIB_zaposlenika + "'" + "," + "'" + infoZaposlenika.Ime + "'" + "," + "'" + infoZaposlenika.Prezime + "'" + "," + "'" + infoZaposlenika.Adresa + "'" + "," + "'" + infoZaposlenika.Broj_telefona + "'" + "," + "'" + infoZaposlenika.Email + "'" + "," + "1" + ")";

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

            Response.Redirect("/Zaposlenici/PregledZaposlenika");
        }
    }
}

