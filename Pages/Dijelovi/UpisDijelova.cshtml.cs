using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Dijelovi
{
    public class UpisDijelovaModel : PageModel
    {
        public InfoDijelova infoDijela = new InfoDijelova();
        public string errorMessage = "";
        public int kol = 0;
        public int cijena = 0;
        public void OnGet()
        {

        }

        public void OnPost()
        {
            infoDijela.Šifra_dijela = Request.Form["Šifra_dijela"];
            infoDijela.Naziv = Request.Form["Naziv_dijela"];
            infoDijela.Kolièina = Request.Form["Kolièina"];
            infoDijela.Cijena = Request.Form["Cijena"];
         

            

          if (infoDijela.Šifra_dijela.Length == 0 || infoDijela.Naziv.Length == 0 || infoDijela.Cijena.Length == 0 || infoDijela.Kolièina.Length == 0)
            {
                errorMessage = "Sva polja moraju biti popunjena";
                return;
            }
            try
            {
                string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";

                string sqlQuery = "INSERT INTO Dio(Šifra_dijela, Naziv, Kolièina, Cijena, ID_skladišta) VALUES (" + "'" + infoDijela.Šifra_dijela + "'" + "," + "'" + infoDijela.Naziv + "'" + "," + Int32.Parse(infoDijela.Kolièina) + "," + Int32.Parse(infoDijela.Cijena) + "," + "2" + ")";

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

            Response.Redirect("/Dijelovi/PregledDijelova");
        }
    }
}
