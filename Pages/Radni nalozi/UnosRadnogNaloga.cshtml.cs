using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Radni_nalozi
{
    public class UnosRadnogNalogaModel : PageModel
    {
        public InfoRadnogNaloga infoNaloga = new InfoRadnogNaloga();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {

        }

        public void OnPost()
        {
            infoNaloga.�ifra_radnog_naloga = Request.Form["�ifra_radnog_naloga"];
            infoNaloga.Opis = Request.Form["Opis"];
            infoNaloga.Napomena = Request.Form["Napomena"];
            infoNaloga.Materijal = Request.Form["Materijal"];
            infoNaloga.Predvi�eno_vrijeme = Request.Form["Predvi�eno_vrijeme_izvr�avanja"];
            infoNaloga.OIB_radnika = Request.Form["OIB_radnika"];
            infoNaloga.Broj_�asije = Request.Form["Broj_�asije"];



            if (infoNaloga.�ifra_radnog_naloga.Length == 0 || infoNaloga.Napomena.Length == 0 || infoNaloga.Materijal.Length == 0 || infoNaloga.Opis.Length == 0 || infoNaloga.Predvi�eno_vrijeme.Length == 0 || infoNaloga.OIB_radnika.Length == 0 || infoNaloga.Broj_�asije.Length == 0)
            {
                errorMessage = "Sva polja moraju biti popunjena";
                return;
            }
            try
            {
                string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";
                string sqlQuery = "INSERT INTO Radni_nalog(�ifra_radnog_naloga, Opis, Napomena, Utro�eni_materijal, Predvi�eno_vrijeme_izvr�avanja, ID_radnika, ID_vozila) VALUES (" + infoNaloga.�ifra_radnog_naloga + "," + "'" + infoNaloga.Opis + "'" + "," + "'" + infoNaloga.Napomena + "'" + "," + "'" + infoNaloga.Materijal + "'" + "," + "'" + infoNaloga.Predvi�eno_vrijeme + "'" + "," + "(SELECT ID FROM Radnik WHERE Radnik.OIB_radnika = '" + infoNaloga.OIB_radnika + "')" + "," + "(SELECT ID FROM Vozilo WHERE Vozilo.Broj_�asije = '" + infoNaloga.Broj_�asije + "'));";

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

            successMessage = "Novi radni nalog dodan.";

            Response.Redirect("/Radni nalozi/PrikazRadnihNaloga");
        }
    }
}

