using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Radni_nalozi
{
    public class UrediRadniNalogModel : PageModel
    {
        public InfoRadnogNaloga infoNaloga = new InfoRadnogNaloga();
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
                    String sql = "Select Radni_nalog.ID, Šifra_radnog_naloga, Opis, Napomena, Utrošeni_materijal, Predviðeno_vrijeme_izvršavanja, Radnik.OIB_radnika, Vozilo.Broj_šasije From ((Radni_nalog INNER JOIN Radnik ON Radni_nalog.ID_radnika=Radnik.ID) INNER JOIN Vozilo ON Radni_nalog.ID_vozila = Vozilo.ID) Where Radni_nalog.ID=@ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                infoNaloga.ID = "" + reader.GetInt32(0);
                                infoNaloga.Šifra_radnog_naloga = "" + reader.GetInt32(1);
                                infoNaloga.Opis = reader.GetString(2);
                                infoNaloga.Napomena = reader.GetString(3);
                                infoNaloga.Materijal = reader.GetString(4);
                                infoNaloga.Predviðeno_vrijeme = reader.GetString(5);
                                infoNaloga.OIB_radnika = reader.GetString(6);
                                infoNaloga.Broj_šasije = reader.GetString(7);
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
            infoNaloga.ID = Request.Form["ID"];
            infoNaloga.Šifra_radnog_naloga = Request.Form["Šifra_radnog_naloga"];
            infoNaloga.Opis = Request.Form["Opis"];
            infoNaloga.Napomena = Request.Form["Napomena"];
            infoNaloga.Materijal = Request.Form["Materijal"];
            infoNaloga.Predviðeno_vrijeme = Request.Form["Predviðeno_vrijeme_izvršavanja"];
            infoNaloga.OIB_radnika = Request.Form["OIB_radnika"];
            infoNaloga.Broj_šasije = Request.Form["Broj_šasije"];

            if (infoNaloga.ID.Length == 0 || infoNaloga.Šifra_radnog_naloga.Length == 0  || infoNaloga.Opis.Length == 0 || infoNaloga.Napomena.Length == 0 || infoNaloga.Materijal.Length == 0 || infoNaloga.Predviðeno_vrijeme.Length == 0 || infoNaloga.OIB_radnika.Length == 0 || infoNaloga.Broj_šasije.Length == 0)
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
                    String sql = "UPDATE Radni_nalog SET Šifra_radnog_naloga = @Šifra_radnog_naloga, Opis = @Opis, Napomena=@Napomena, Utrošeni_materijal=@Materijal, Predviðeno_vrijeme_izvršavanja = @Predviðeno_vrijeme, ID_radnika = (SELECT Radnik.ID FROM Radnik WHERE OIB_radnika = @OIB_radnika), ID_vozila = (SELECT Vozilo.ID FROM Vozilo WHERE Broj_šasije = @Broj_šasije) Where ID=@ID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Šifra_radnog_naloga", infoNaloga.Šifra_radnog_naloga);
                        command.Parameters.AddWithValue("@Opis", infoNaloga.Opis);
                        command.Parameters.AddWithValue("@Napomena", infoNaloga.Napomena);
                        command.Parameters.AddWithValue("@Materijal", infoNaloga.Materijal);
                        command.Parameters.AddWithValue("@Predviðeno_vrijeme", infoNaloga.Predviðeno_vrijeme);
                        command.Parameters.AddWithValue("@OIB_radnika", infoNaloga.OIB_radnika);
                        command.Parameters.AddWithValue("@Broj_šasije", infoNaloga.Broj_šasije);
                        command.Parameters.AddWithValue("@ID", infoNaloga.ID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Radni nalozi/PrikazRadnihNaloga");
        }
    }
}

