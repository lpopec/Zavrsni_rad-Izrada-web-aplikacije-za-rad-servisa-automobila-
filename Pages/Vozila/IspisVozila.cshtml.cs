using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Vozila
{
    public class IspisVozilaModel : PageModel
    {
        public List<InfoVozila> ListaVozila = new List<InfoVozila>();
        public void OnGet()
        {
            string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";

            string sqlQuery = "SELECT Vozilo.ID, Broj_šasije, Registarska_oznaka, Naziv, Boja, Vlasnik.OIB_vlasnika, Vlasnik.Ime + ' ' + Vlasnik.Prezime AS 'Ime i prezime vlasnika', Vlasnik.broj_telefona FROM Vozilo INNER JOIN Vlasnik ON Vozilo.ID_vlasnika = Vlasnik.ID;";

            SqlConnection con = new SqlConnection(connectionString);

            con.Open();
            using (SqlCommand sc = new SqlCommand(sqlQuery, con))
            {
                using (SqlDataReader reader = sc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InfoVozila vozilo = new InfoVozila();
                        vozilo.ID = "" + reader.GetInt32(0);
                        vozilo.Broj_šasije = reader.GetString(1);
                        vozilo.Registarska_oznaka = reader.GetString(2);
                        vozilo.Naziv = reader.GetString(3);
                        vozilo.Boja = reader.GetString(4);
                        vozilo.OIB_vlasnika = reader.GetString(5);
                        vozilo.Ime_prezime_vlasnika = reader.GetString(6);
                        vozilo.Broj_telefona_vlasnika = reader.GetString(7);

                        ListaVozila.Add(vozilo);
                    }
                }
            }
            con.Close();
        }
    }
    public class InfoVozila
    {
        public string ID;
        public string Broj_šasije;
        public string Registarska_oznaka;
        public string Naziv;
        public string Boja;
        public string OIB_vlasnika;
        public string Ime_prezime_vlasnika;
        public string Broj_telefona_vlasnika;
    }
}

