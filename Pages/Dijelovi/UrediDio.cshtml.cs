using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Dijelovi
{
    public class UrediDioModel : PageModel
    {

        public InfoDijelova infoDijela = new InfoDijelova();
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
                    String sql = "Select ID, �ifra_dijela, Naziv, Koli�ina, Cijena From Dio Where ID=@ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                infoDijela.ID = "" + reader.GetInt32(0);
                                infoDijela.�ifra_dijela = "" + reader.GetInt32(1);
                                infoDijela.Naziv = reader.GetString(2);
                                infoDijela.Koli�ina = "" + reader.GetInt32(3);
                                infoDijela.Cijena = "" + reader.GetInt32(4);
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
            infoDijela.ID = Request.Form["ID"];
            infoDijela.�ifra_dijela = Request.Form["�ifra_dijela"];
            infoDijela.Naziv = Request.Form["Naziv"];
            infoDijela.Koli�ina = Request.Form["Koli�ina"];
            infoDijela.Cijena = Request.Form["Cijena"];

            if (infoDijela.ID.Length == 0 || infoDijela.�ifra_dijela.Length == 0 || infoDijela.Naziv.Length == 0 || infoDijela.Cijena.Length == 0 || infoDijela.Koli�ina.Length == 0)
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
                    String sql = "UPDATE DIO SET �ifra_dijela = @�ifra_dijela,Naziv = @Naziv,Koli�ina = @Koli�ina, Cijena = @Cijena, ID_skladi�ta = 2 Where ID=@ID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@�ifra_dijela", infoDijela.�ifra_dijela);
                        command.Parameters.AddWithValue("@Naziv", infoDijela.Naziv);
                        command.Parameters.AddWithValue("@Cijena", infoDijela.Cijena);
                        command.Parameters.AddWithValue("@Koli�ina", infoDijela.Koli�ina);
                        command.Parameters.AddWithValue("@ID", infoDijela.ID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Dijelovi/PregledDijelova");

        }
    }
}
