using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Auto_Servis.Pages.Dobavljaci.PrikazDobavljacaModel;

namespace Auto_Servis.Pages.Dobavljaci
{
    public class UrediDobavljacaModel : PageModel
    {
        public InfoDobavljaca infoDobavljaca = new InfoDobavljaca();
        public string errorMessage = "";
        public void OnGet()
        {
            String ID = Request.Query["ID"];
            try
            {
                string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT ID, �ifra_dobavlja�a, Naziv, Broj_telefona, Adresa, Web_stranica FROM Dobavlja� WHERE ID=@ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                infoDobavljaca.ID = "" + reader.GetInt32(0);
                                infoDobavljaca.�ifra_dobavlja�a = "" + reader.GetInt32(1);
                                infoDobavljaca.Naziv = reader.GetString(2);
                                infoDobavljaca.Broj_telefona = reader.GetString(3);
                                infoDobavljaca.Adresa = reader.GetString(4);
                                infoDobavljaca.Web_stranica = reader.GetString(5);
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
            infoDobavljaca.ID = Request.Form["ID"];
            infoDobavljaca.�ifra_dobavlja�a = Request.Form["�ifra_dobavljaca"];
            infoDobavljaca.Naziv = Request.Form["Naziv_dobavljaca"];
            infoDobavljaca.Broj_telefona = Request.Form["Broj_telefona"];
            infoDobavljaca.Adresa = Request.Form["Adresa"];
            infoDobavljaca.Web_stranica = Request.Form["Web_stranica"];

            if (infoDobavljaca.ID.Length == 0 || infoDobavljaca.�ifra_dobavlja�a.Length == 0 || infoDobavljaca.Naziv.Length == 0 || infoDobavljaca.Broj_telefona.Length == 0 || infoDobavljaca.Adresa.Length == 0 || infoDobavljaca.Web_stranica.Length == 0)
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
                    String sql = "UPDATE Dobavlja� SET �ifra_dobavlja�a = @�ifra_dobavlja�a,Naziv = @Naziv,Broj_telefona = @Broj_telefona, Adresa = @Adresa, Web_stranica =@Web_stranica Where ID=@ID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@�ifra_dobavlja�a", infoDobavljaca.�ifra_dobavlja�a);
                        command.Parameters.AddWithValue("@Naziv", infoDobavljaca.Naziv);
                        command.Parameters.AddWithValue("@Broj_telefona", infoDobavljaca.Broj_telefona);
                        command.Parameters.AddWithValue("@Adresa", infoDobavljaca.Adresa);
                        command.Parameters.AddWithValue("@Web_stranica", infoDobavljaca.Web_stranica);
                        command.Parameters.AddWithValue("@ID", infoDobavljaca.ID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Dobavljaci/PregledDobavljaca");

        }
    }
}
