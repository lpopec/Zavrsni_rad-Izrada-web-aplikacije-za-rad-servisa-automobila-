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
                    String sql = "Select Radni_nalog.ID, �ifra_radnog_naloga, Opis, Napomena, Utro�eni_materijal, Predvi�eno_vrijeme_izvr�avanja, Radnik.OIB_radnika, Vozilo.Broj_�asije From ((Radni_nalog INNER JOIN Radnik ON Radni_nalog.ID_radnika=Radnik.ID) INNER JOIN Vozilo ON Radni_nalog.ID_vozila = Vozilo.ID) Where Radni_nalog.ID=@ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                infoNaloga.ID = "" + reader.GetInt32(0);
                                infoNaloga.�ifra_radnog_naloga = "" + reader.GetInt32(1);
                                infoNaloga.Opis = reader.GetString(2);
                                infoNaloga.Napomena = reader.GetString(3);
                                infoNaloga.Materijal = reader.GetString(4);
                                infoNaloga.Predvi�eno_vrijeme = reader.GetString(5);
                                infoNaloga.OIB_radnika = reader.GetString(6);
                                infoNaloga.Broj_�asije = reader.GetString(7);
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
            infoNaloga.�ifra_radnog_naloga = Request.Form["�ifra_radnog_naloga"];
            infoNaloga.Opis = Request.Form["Opis"];
            infoNaloga.Napomena = Request.Form["Napomena"];
            infoNaloga.Materijal = Request.Form["Materijal"];
            infoNaloga.Predvi�eno_vrijeme = Request.Form["Predvi�eno_vrijeme_izvr�avanja"];
            infoNaloga.OIB_radnika = Request.Form["OIB_radnika"];
            infoNaloga.Broj_�asije = Request.Form["Broj_�asije"];

            if (infoNaloga.ID.Length == 0 || infoNaloga.�ifra_radnog_naloga.Length == 0  || infoNaloga.Opis.Length == 0 || infoNaloga.Napomena.Length == 0 || infoNaloga.Materijal.Length == 0 || infoNaloga.Predvi�eno_vrijeme.Length == 0 || infoNaloga.OIB_radnika.Length == 0 || infoNaloga.Broj_�asije.Length == 0)
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
                    String sql = "UPDATE Radni_nalog SET �ifra_radnog_naloga = @�ifra_radnog_naloga, Opis = @Opis, Napomena=@Napomena, Utro�eni_materijal=@Materijal, Predvi�eno_vrijeme_izvr�avanja = @Predvi�eno_vrijeme, ID_radnika = (SELECT Radnik.ID FROM Radnik WHERE OIB_radnika = @OIB_radnika), ID_vozila = (SELECT Vozilo.ID FROM Vozilo WHERE Broj_�asije = @Broj_�asije) Where ID=@ID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@�ifra_radnog_naloga", infoNaloga.�ifra_radnog_naloga);
                        command.Parameters.AddWithValue("@Opis", infoNaloga.Opis);
                        command.Parameters.AddWithValue("@Napomena", infoNaloga.Napomena);
                        command.Parameters.AddWithValue("@Materijal", infoNaloga.Materijal);
                        command.Parameters.AddWithValue("@Predvi�eno_vrijeme", infoNaloga.Predvi�eno_vrijeme);
                        command.Parameters.AddWithValue("@OIB_radnika", infoNaloga.OIB_radnika);
                        command.Parameters.AddWithValue("@Broj_�asije", infoNaloga.Broj_�asije);
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

