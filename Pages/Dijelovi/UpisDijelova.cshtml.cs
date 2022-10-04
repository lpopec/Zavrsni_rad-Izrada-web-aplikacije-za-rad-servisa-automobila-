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
            infoDijela.�ifra_dijela = Request.Form["�ifra_dijela"];
            infoDijela.Naziv = Request.Form["Naziv_dijela"];
            infoDijela.Koli�ina = Request.Form["Koli�ina"];
            infoDijela.Cijena = Request.Form["Cijena"];
         

            

          if (infoDijela.�ifra_dijela.Length == 0 || infoDijela.Naziv.Length == 0 || infoDijela.Cijena.Length == 0 || infoDijela.Koli�ina.Length == 0)
            {
                errorMessage = "Sva polja moraju biti popunjena";
                return;
            }
            try
            {
                string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";

                string sqlQuery = "INSERT INTO Dio(�ifra_dijela, Naziv, Koli�ina, Cijena, ID_skladi�ta) VALUES (" + "'" + infoDijela.�ifra_dijela + "'" + "," + "'" + infoDijela.Naziv + "'" + "," + Int32.Parse(infoDijela.Koli�ina) + "," + Int32.Parse(infoDijela.Cijena) + "," + "2" + ")";

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
