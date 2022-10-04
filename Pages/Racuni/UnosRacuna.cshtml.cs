using Auto_Servis.Pages.Raèuni;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Racuni
{
    public class UnosRacunaModel : PageModel
    {
        public InfoRacuna infoRacuna = new InfoRacuna();
        public string errorMessage = "";
        public void OnGet()
        {

        }

        public void OnPost()
        {
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
                string sqlQuery = @"DECLARE @pdv_postotak INT; 
                                    DECLARE @pdv DECIMAL(10, 2); 
                                    DECLARE @cijena_dijelova DECIMAL(10, 2); 
                                    DECLARE @Ukupna_cijena DECIMAL(10, 2); 
                                    SET @pdv_postotak = "+ infoRacuna.pdv_postotak +";" +
                                    "SET @pdv = @pdv_postotak / 100.0;" +
                                    "SET @cijena_dijelova = "+ infoRacuna.Cijena_dijelova +";" +
                                    "SET @Ukupna_cijena = @cijena_dijelova + (@cijena_dijelova * @pdv); " +
                                    "INSERT INTO Raèun(Šifra_raèuna, Naziv, Opis, Korišteni_materijal, Datum_upisa, ID_vlasnika, ID_servisa, Ukupna_cijena, Cijena_dijelova,pdv) " +
                                        "VALUES (" + infoRacuna.Šifra_racuna + "," + "'" + infoRacuna.Naziv + "'" + "," + "'" + infoRacuna.Opis + "'" + "," + "'" + infoRacuna.Korišteni_materijal + "'" + "," + "(SELECT GETDATE())," + "(SELECT ID FROM Vlasnik WHERE OIB_vlasnika = '"+ infoRacuna.OIB_vlasnika + "')" + "," +"1,@Ukupna_cijena, @cijena_dijelova , @pdv_postotak);";

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

            Response.Redirect("/Radni nalozi/PrikazRadnihNaloga");
        }
    }
}
