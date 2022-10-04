using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Zaposlenici
{
    public class UrediZaposlenikaModel : PageModel
    {

        public InfoZaposlenika infoZaposlenika = new InfoZaposlenika();
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
                    String sql = "Select ID, OIB_radnika, Ime, Prezime, Broj_telefona, Adresa, Email From Radnik Where ID=@ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                infoZaposlenika.ID = "" + reader.GetInt32(0);
                                infoZaposlenika.OIB_zaposlenika = reader.GetString(1);
                                infoZaposlenika.Ime = reader.GetString(2);
                                infoZaposlenika.Prezime = reader.GetString(3);
                                infoZaposlenika.Broj_telefona = reader.GetString(4);
                                infoZaposlenika.Adresa = reader.GetString(5);
                                infoZaposlenika.Email = reader.GetString(6);
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
            infoZaposlenika.ID = Request.Form["ID"];
            infoZaposlenika.OIB_zaposlenika = Request.Form["OIB_zaposlenika"];
            infoZaposlenika.Ime = Request.Form["Ime"];
            infoZaposlenika.Prezime = Request.Form["Prezime"];
            infoZaposlenika.Broj_telefona = Request.Form["Broj_telefona"];
            infoZaposlenika.Adresa = Request.Form["Adresa"];
            infoZaposlenika.Email = Request.Form["Email"];

            if (infoZaposlenika.ID.Length == 0 || infoZaposlenika.OIB_zaposlenika.Length == 0 || infoZaposlenika.Ime.Length == 0 || infoZaposlenika.Prezime.Length == 0 || infoZaposlenika.Broj_telefona.Length == 0 || infoZaposlenika.Adresa.Length == 0 || infoZaposlenika.Email.Length == 0)
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
                    String sql = "UPDATE Radnik SET OIB_radnika = @OIB_zaposlenika,Ime = @Ime,Prezime = @Prezime, Broj_telefona = @Broj_telefona, Adresa =@Adresa, Email=@Email Where ID=@ID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@OIB_zaposlenika", infoZaposlenika.OIB_zaposlenika);
                        command.Parameters.AddWithValue("@Ime", infoZaposlenika.Ime);
                        command.Parameters.AddWithValue("@Prezime", infoZaposlenika.Prezime);
                        command.Parameters.AddWithValue("@Broj_telefona", infoZaposlenika.Broj_telefona);
                        command.Parameters.AddWithValue("@Adresa", infoZaposlenika.Adresa);
                        command.Parameters.AddWithValue("@Email", infoZaposlenika.Email);
                        command.Parameters.AddWithValue("@ID", infoZaposlenika.ID);

                        command.ExecuteNonQuery();
                    }
                }
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
