using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Auto_Servis.Pages.Vozila
{
    public class DodajVoziloModel : PageModel
    {
        public InfoVozila infoVozila = new InfoVozila();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {

        }

        public void OnPost()
        {
            infoVozila.Broj_šasije = Request.Form["Broj_šasije"];
            infoVozila.Registarska_oznaka = Request.Form["Registarska_oznaka"];
            infoVozila.Naziv = Request.Form["Naziv"];
            infoVozila.Boja = Request.Form["Boja"];
            infoVozila.OIB_vlasnika = Request.Form["OIB_vlasnika"];

            if (string.IsNullOrEmpty(infoVozila.Broj_šasije) || string.IsNullOrEmpty(infoVozila.Registarska_oznaka) || string.IsNullOrEmpty(infoVozila.Naziv) || string.IsNullOrEmpty(infoVozila.Boja) || string.IsNullOrEmpty(infoVozila.OIB_vlasnika))
            {
                errorMessage = "Sva polja moraju biti popunjena";
                return;
            }
            try
            {
                string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";

                string sqlQuery = "INSERT INTO Vozilo(Broj_šasije,Registarska_oznaka,Naziv,Boja,ID_vlasnika) VALUES (" + "'" + infoVozila.Broj_šasije + "'" + ","+ "'" + infoVozila.Registarska_oznaka + "'" + "," + "'" + infoVozila.Naziv + "'" + "," + "'" + infoVozila.Boja + "'" + "," + "(SELECT ID FROM Vlasnik WHERE Vlasnik.OIB_vlasnika = '"+ infoVozila.OIB_vlasnika + "'));";


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

            successMessage = "Novo vozilo dodano.";

            Response.Redirect("/Vozila/IspisVozila");
        }
    }
}
