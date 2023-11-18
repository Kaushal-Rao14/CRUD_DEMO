using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace CRUD_DEMO.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<clientsInfo> list=new   List<clientsInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\SqlExpress;Initial Catalog=dbCRUD_APP;Integrated Security=True;Encrypt=false";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "select * from clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clientsInfo info = new clientsInfo();
                                info.id = reader.GetInt32(0);
                                info.name = reader.GetString(1);
                                info.email = reader.GetString(2);
                                info.mobile = reader.GetString(3);
                                info.address = reader.GetString(4);
                                info.createdAt = reader.GetDateTime(5);
                                list.Add(info);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error - " + ex.Message);
            }
        }
    }
    public class clientsInfo
    {
        public int id;
        public string name;
        public string email;
        public string mobile;
        public string address;
        public DateTime createdAt;



    }

}
