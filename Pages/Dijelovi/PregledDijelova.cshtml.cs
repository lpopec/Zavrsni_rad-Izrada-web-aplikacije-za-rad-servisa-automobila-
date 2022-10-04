using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Auto_Servis.Pages.Dijelovi
{
    public class PregledDijelovaModel : PageModel
    {
        public List<InfoDijelova> ListaDijelova = new List<InfoDijelova>();
        public void OnGet()
        {
            string connectionString = "Data Source=Lovro_LAPTOP; Initial Catalog=Auto_servis; Integrated Security=True; TrustServerCertificate=True;";

            string sqlQuery = "SELECT Dio.ID, Dio.�ifra_dijela, Dio.Naziv, Dio.Koli�ina, Dio.Cijena, Skladi�te.Naziv AS 'Naziv skladi�ta' FROM Dio INNER JOIN Skladi�te ON Dio.ID_skladi�ta = Skladi�te.ID";

            SqlConnection con = new SqlConnection(connectionString);

            con.Open();
            using (SqlCommand sc = new SqlCommand(sqlQuery, con))
            {
                using (SqlDataReader reader = sc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InfoDijelova dio = new InfoDijelova();
                        dio.ID = "" + reader.GetInt32(0);
                        dio.�ifra_dijela = "" + reader.GetInt32(1);
                        dio.Naziv = reader.GetString(2);
                        dio.Koli�ina = "" + reader.GetInt32(3);
                        dio.Cijena = "" + reader.GetInt32(4);
                        dio.Naziv_skladi�ta = reader.GetString(5);

                        ListaDijelova.Add(dio);
                    }
                }
            }
            con.Close();
        }
    }
    public class InfoDijelova
    {
        public string ID;
        public string �ifra_dijela;
        public string Naziv;
        public string Koli�ina;
        public string Cijena;
        public string Naziv_skladi�ta;
    }
}

