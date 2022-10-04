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
            infoNaloga.Šifra_radnog_naloga = Request.Form["Šifra_radnog_naloga"];
            infoNaloga.Opis = Request.Form["Opis"];
            infoNaloga.Napomena = Request.Form["Napomena"];
            infoNaloga.Materijal = Request.Form["Materijal"];
            infoNaloga.Predviðeno_vrijeme = Request.Form["Predviðeno_vrijeme_izvršavanja"];
            infoNaloga.OIB_radnika = Request.Form["OIB_radnika"];
            infoNaloga.Broj_šasije = Request.Form["Broj_šasije"];



            if (infoNaloga.Šifra_radnog_naloga.Length == 0 || infoNaloga.Napomena.Length == 0 || infoNaloga.Materijal.Length == 0 || infoNaloga.Opis.Length == 0 || infoNaloga.Predviðeno_vrijeme.Length == 0 || infoNaloga.OIB_radnika.Length == 0 || infoNaloga.Broj_šasije.Length == 0)
            {
                errorMessage = "Sva polja moraju biti popunjena";
                return;
            }
            try
            {
                string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";
                string sqlQuery = "INSERT INTO Radni_nalog(Šifra_radnog_naloga, Opis, Napomena, Utrošeni_materijal, Predviðeno_vrijeme_izvršavanja, ID_radnika, ID_vozila) VALUES (" + infoNaloga.Šifra_radnog_naloga + "," + "'" + infoNaloga.Opis + "'" + "," + "'" + infoNaloga.Napomena + "'" + "," + "'" + infoNaloga.Materijal + "'" + "," + "'" + infoNaloga.Predviðeno_vrijeme + "'" + "," + "(SELECT ID FROM Radnik WHERE Radnik.OIB_radnika = '" + infoNaloga.OIB_radnika + "')" + "," + "(SELECT ID FROM Vozilo WHERE Vozilo.Broj_šasije = '" + infoNaloga.Broj_šasije + "'));";

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

