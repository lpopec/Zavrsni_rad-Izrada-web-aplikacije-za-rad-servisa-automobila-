using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Auto_Servis.Pages.Dobavljaci.PrikazDobavljacaModel;

namespace Auto_Servis.Pages.Dobavljaci
{
    public class UnosDobavljacaModel : PageModel
    {
        public InfoDobavljaca infoDobavljaca = new InfoDobavljaca();
        public string errorMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            infoDobavljaca.Šifra_dobavljaèa = Request.Form["Šifra_dobavljaca"];
            infoDobavljaca.Naziv = Request.Form["Naziv_dobavljaca"];
            infoDobavljaca.Adresa = Request.Form["Adresa"];
            infoDobavljaca.Broj_telefona = Request.Form["Broj_telefona"];
            infoDobavljaca.Web_stranica = Request.Form["Web_stranica"];

            if (string.IsNullOrEmpty(infoDobavljaca.Šifra_dobavljaèa) || string.IsNullOrEmpty(infoDobavljaca.Naziv) || string.IsNullOrEmpty(infoDobavljaca.Adresa) || string.IsNullOrEmpty(infoDobavljaca.Broj_telefona) || string.IsNullOrEmpty(infoDobavljaca.Web_stranica))
            {
                errorMessage = "Sva polja moraju biti popunjena";
                return;
            }
            try
            {
                string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";

                string sqlQuery = "INSERT INTO Dobavljaè (Šifra_dobavljaèa, Naziv, Adresa, Broj_telefona, Web_stranica, ID_servisa) VALUES (" + "'" + infoDobavljaca.Šifra_dobavljaèa + "'" + "," + "'" + infoDobavljaca.Naziv + "'" + "," + "'" + infoDobavljaca.Adresa + "'" + "," + "'" + infoDobavljaca.Broj_telefona + "'" + "," + "'" + infoDobavljaca.Web_stranica + "'" + ", 1)";

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

            Response.Redirect("/Dobavljaci/PrikazDobavljaca");
        }
    }
}
