using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Radni_nalozi
{
    public class PrikazRadnihNalogaModel : PageModel
    {
        public List<InfoRadnogNaloga> ListaNaloga = new List<InfoRadnogNaloga>();
        public void OnGet()
        {
            string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";

            string sqlQuery = "SELECT Radni_nalog.ID, Šifra_radnog_naloga, Predviðeno_vrijeme_izvršavanja ,Opis, Napomena, Utrošeni_materijal, Radnik.Ime + ' ' + Radnik.Prezime AS 'Ime i prezime radnika', Vozilo.Broj_šasije AS 'Broj šasije' FROM ((Radni_nalog INNER JOIN Radnik ON Radni_nalog.ID_radnika = Radnik.ID) INNER JOIN Vozilo ON Radni_nalog.ID_vozila = Vozilo.ID);";

            SqlConnection con = new SqlConnection(connectionString);

            con.Open();
            using (SqlCommand sc = new SqlCommand(sqlQuery, con))
            {
                using (SqlDataReader reader = sc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InfoRadnogNaloga nalog = new InfoRadnogNaloga();
                        nalog.ID = "" + reader.GetInt32(0);
                        nalog.Šifra_radnog_naloga = "" + reader.GetInt32(1);
                        nalog.Predviðeno_vrijeme = reader.GetString(2);
                        nalog.Opis = reader.GetString(3);
                        nalog.Napomena = reader.GetString(4);
                        nalog.Materijal = reader.GetString(5);
                        nalog.Ime_Prezime_Radnika = reader.GetString(6);
                        nalog.Broj_šasije = reader.GetString(7);
               

                        ListaNaloga.Add(nalog);
                    }
                }
            }
            con.Close();
        }
    }
    public class InfoRadnogNaloga
    {
        public string ID;
        public string Šifra_radnog_naloga;
        public string Opis;
        public string Predviðeno_vrijeme;
        public string Ime_Prezime_Radnika;
        public string OIB_radnika;
        public string Broj_šasije;
        public string ID_vozila;
        public string Model_vozila;
        public string Napomena;
        public string ID_radnika;
        public string Materijal;
    }
}
