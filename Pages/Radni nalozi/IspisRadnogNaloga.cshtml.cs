using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Radni_nalozi
{
    public class IspisRadnogNalogaModel : PageModel
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
                    String sql = "Select Radni_nalog.ID, Šifra_radnog_naloga, Opis, Napomena, Utrošeni_materijal, Predviðeno_vrijeme_izvršavanja, Radnik.Ime + ' ' + Radnik.Prezime AS 'Ime i prezime radnika', Vozilo.Broj_šasije, Vozilo.Naziv From ((Radni_nalog INNER JOIN Radnik ON Radni_nalog.ID_radnika=Radnik.ID) INNER JOIN Vozilo ON Radni_nalog.ID_vozila = Vozilo.ID) Where Radni_nalog.ID=@ID;";
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
                                infoNaloga.Ime_Prezime_Radnika = reader.GetString(6);
                                infoNaloga.Broj_šasije = reader.GetString(7);
                                infoNaloga.Model_vozila = reader.GetString(8);
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
    }
}
