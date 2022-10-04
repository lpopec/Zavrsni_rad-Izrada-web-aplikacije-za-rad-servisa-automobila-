using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Vozila
{
    public class UrediPodatkeVozilaModel : PageModel
    {
        public InfoVozila infoVozila = new InfoVozila();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            String ID = Request.Query["ID"];
            try
            {
                string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select ID, Broj_šasije, Registarska_oznaka, Naziv, Boja From Vozilo Where ID=@ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                infoVozila.ID = "" + reader.GetInt32(0);
                                infoVozila.Broj_šasije = reader.GetString(1);
                                infoVozila.Registarska_oznaka = reader.GetString(2);
                                infoVozila.Naziv = reader.GetString(3);
                                infoVozila.Boja = reader.GetString(4);
                            }
                        }

                    }
                }

            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            infoVozila.ID = Request.Form["ID"];
            infoVozila.Broj_šasije = Request.Form["Broj_šasije"];
            infoVozila.Registarska_oznaka = Request.Form["Registarska_oznaka"];
            infoVozila.Naziv = Request.Form["Naziv"];
            infoVozila.Boja = Request.Form["Boja"];

            if (infoVozila.ID.Length == 0 || infoVozila.Broj_šasije.Length == 0 || infoVozila.Registarska_oznaka.Length == 0 || infoVozila.Naziv.Length == 0 || infoVozila.Boja.Length == 0)
            {
                errorMessage = "Sva polja moraju biti popunjena";
                return;
            }

            try
            {
                string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Vozilo SET Broj_šasije = @Broj_šasije, Registarska_oznaka = @Registarska_oznaka, Naziv = @Naziv, Boja = @Boja Where ID=@ID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", infoVozila.ID);
                        command.Parameters.AddWithValue("@Broj_šasije", infoVozila.Broj_šasije);
                        command.Parameters.AddWithValue("@Registarska_oznaka", infoVozila.Registarska_oznaka);
                        command.Parameters.AddWithValue("@Naziv", infoVozila.Naziv);
                        command.Parameters.AddWithValue("@Boja", infoVozila.Boja);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Vozila/IspisVozila");

        }
    }
}

