using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Vlasnici
{
    public class UrediVlasnikaModel : PageModel
    {

        public InfoVlasnika infoVlasnika = new InfoVlasnika();
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
                    String sql = "Select * From Vlasnik Where ID=@ID";
                    using (SqlCommand command= new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                infoVlasnika.ID = "" + reader.GetInt32(0);
                                infoVlasnika.OIB_vlasnika = reader.GetString(1);
                                infoVlasnika.Ime = reader.GetString(2);
                                infoVlasnika.Prezime = reader.GetString(3);
                                infoVlasnika.Broj_telefona = reader.GetString(4);
                                infoVlasnika.Adresa = reader.GetString(5);
                            }
                        }

                    }
                }
             
             }
       
            catch (Exception ex)
            {
                errorMessage=ex.Message;
            }
        }

        public void OnPost()
        {
            infoVlasnika.ID = Request.Form["ID"];
            infoVlasnika.OIB_vlasnika = Request.Form["OIB_vlasnika"];
            infoVlasnika.Ime = Request.Form["Ime_vlasnika"];
            infoVlasnika.Prezime = Request.Form["Prezime_vlasnika"];
            infoVlasnika.Broj_telefona = Request.Form["Broj_telefona"];
            infoVlasnika.Adresa = Request.Form["Adresa"];

          if (infoVlasnika.ID.Length == 0 || infoVlasnika.OIB_vlasnika.Length == 0 || infoVlasnika.Ime.Length == 0 || infoVlasnika.Prezime.Length == 0 || infoVlasnika.Broj_telefona.Length == 0|| infoVlasnika.Adresa.Length == 0)
            {
                errorMessage = "Sva polja moraju biti popunjena";
                return;
            }
          
            try
            {
                string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Vlasnik SET OIB_vlasnika = @OIB_vlasnika,Ime = @Ime,Prezime = @Prezime, broj_telefona = @Broj_telefona, Adresa =@Adresa Where ID=@ID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@OIB_vlasnika", infoVlasnika.OIB_vlasnika);
                        command.Parameters.AddWithValue("@Ime", infoVlasnika.Ime);
                        command.Parameters.AddWithValue("@Prezime", infoVlasnika.Prezime);
                        command.Parameters.AddWithValue("@Broj_telefona", infoVlasnika.Broj_telefona);
                        command.Parameters.AddWithValue("@Adresa", infoVlasnika.Adresa);
                        command.Parameters.AddWithValue("@ID", infoVlasnika.ID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Vlasnici/PregledVlasnika");

        }
    }
}
