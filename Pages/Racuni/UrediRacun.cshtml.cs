using Auto_Servis.Pages.Raèuni;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Racuni
{
    public class UrediRacunModel : PageModel
    {
        public InfoRacuna infoRacuna = new InfoRacuna();
        public string errorMessage = "";
        public decimal Privremena_cijena;
        public void OnGet()
        {
            String ID = Request.Query["ID"];
            try
            {
                string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select Raèun.ID_racuna, Raèun.Šifra_raèuna, Raèun.Naziv, Raèun.Opis, Raèun.Korišteni_materijal, Raèun.Cijena_dijelova, Raèun.pdv, Vlasnik.OIB_vlasnika FROM (Raèun INNER JOIN Vlasnik ON Raèun.ID_vlasnika = Vlasnik.ID) WHERE Raèun.ID_racuna = @ID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                infoRacuna.ID = "" + reader.GetInt32(0);
                                infoRacuna.Šifra_racuna = "" + reader.GetInt32(1);
                                infoRacuna.Naziv = reader.GetString(2);
                                infoRacuna.Opis = reader.GetString(3);
                                infoRacuna.Korišteni_materijal = reader.GetString(4);
                                Privremena_cijena = reader.GetDecimal(5);
                                infoRacuna.Cijena_dijelova = Privremena_cijena.ToString();
                                infoRacuna.pdv_postotak = "" + reader.GetInt32(6);
                                infoRacuna.OIB_vlasnika = reader.GetString(7);
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
            infoRacuna.ID = Request.Form["ID"];
            infoRacuna.Šifra_racuna = Request.Form["Šifra_racuna"];
            infoRacuna.Naziv = Request.Form["Naziv"];
            infoRacuna.Opis = Request.Form["Opis"];
            infoRacuna.Korišteni_materijal = Request.Form["Materijal"];
            infoRacuna.Cijena_dijelova = Request.Form["Cijena_materijala"];
            infoRacuna.pdv_postotak = Request.Form["pdv"];
            infoRacuna.OIB_vlasnika = Request.Form["OIB_vlasnika"];

            if (infoRacuna.Šifra_racuna.Length == 0 || infoRacuna.Naziv.Length == 0 || infoRacuna.Opis.Length == 0 || infoRacuna.Korišteni_materijal.Length == 0 || infoRacuna.Cijena_dijelova.Length == 0 || infoRacuna.pdv_postotak.Length == 0 || infoRacuna.OIB_vlasnika.Length == 0)
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
                    String sql = @"DECLARE @pdv_postotak INT; 
                                   DECLARE @pdv DECIMAL(10, 2); 
                                   DECLARE @cijena_dijelova DECIMAL(10, 2); 
                                   DECLARE @Ukupna_cijena DECIMAL(10, 2); 
                                   SET @pdv_postotak = @pdv_postotak1;" +
                                   "SET @pdv = @pdv_postotak / 100.0;" +
                                   "SET @cijena_dijelova = @Cijena_dijelova1;" +
                                   "SET @Ukupna_cijena = @cijena_dijelova + (@cijena_dijelova * @pdv); " + 
                                   "UPDATE Raèun SET Šifra_raèuna = @Šifra_raèuna, Naziv=@Naziv, Opis = @Opis, Korišteni_materijal=@Materijal, Cijena_dijelova = @cijena_dijelova, pdv=@pdv_postotak, Ukupna_cijena=@ukupna_cijena, ID_vlasnika = (SELECT Vlasnik.ID FROM Vlasnik WHERE OIB_vlasnika = @OIB_vlasnika) Where Raèun.ID_racuna=@ID;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Šifra_raèuna", infoRacuna.Šifra_racuna);
                        command.Parameters.AddWithValue("@Naziv", infoRacuna.Naziv);
                        command.Parameters.AddWithValue("@Opis", infoRacuna.Opis);
                        command.Parameters.AddWithValue("@Materijal", infoRacuna.Korišteni_materijal);
                        command.Parameters.AddWithValue("@Cijena_dijelova1", float.Parse(infoRacuna.Cijena_dijelova));
                        command.Parameters.AddWithValue("@pdv_postotak1", infoRacuna.pdv_postotak);
                        command.Parameters.AddWithValue("@OIB_vlasnika", infoRacuna.OIB_vlasnika);
                        command.Parameters.AddWithValue("@ID", infoRacuna.ID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Racuni/PrikazRacuna");
        }
    }
}
