using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace CRUD_DEMO.Pages.Clients
{
    
    public class EditModel : PageModel
    {
        public clientsInfo client=new clientsInfo();
        public string errorMessage = string.Empty;
        public string successMessage = string.Empty;
        public string connectionString = "Data Source=.\\SqlExpress;Initial Catalog=dbCRUD_APP;Integrated Security=True;Encrypt=false";
        public void OnGet()
        {

            string id = Request.Query["id"];
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "select  * from clients where id=@id";
                    SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                    cmd.Parameters.AddWithValue("@id", Int32.Parse(id));
                    using(SqlDataReader rd=cmd.ExecuteReader())
                    {
                       if(rd.Read())
                        {
                            client.id = Convert.ToInt32(rd["id"]);
                            client.name = rd.GetString(1);
                            client.email = rd.GetString(2);
                            client.mobile = rd.GetString(3);
                            client.address = rd.GetString(4);
                        }
                        else
                        {
                            errorMessage = "sorry unable to read...";
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
             }
        }


        public void OnPost()
        {
            try
            {
                client.id = Int32.Parse(Request.Form["id"]);
                client.name = Request.Form["name"];
                client.email = Request.Form["email"];
                client.mobile = Request.Form["mobile"];
                client.address = Request.Form["address"];

                //validating 
                if (client.name.Length == 0 || client.email.Length == 0 || client.mobile.Length == 0 || client.address.Length == 0)
                {
                    errorMessage = "All field required";
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    int id = Int32.Parse(Request.Query["id"]);
                    string sqlQuery = "update clients " +
                                      "set name=@name,email=@email," +
                                      "mobile=@mobile, address=@address"+
                                      " where id=@id";
                    using(SqlCommand cmd=new SqlCommand(sqlQuery, connection)) 
                    {
                        cmd.Parameters.AddWithValue("@name", client.name);
                        cmd.Parameters.AddWithValue("@email", client.email);
                        cmd.Parameters.AddWithValue("@mobile", client.mobile);
                        cmd.Parameters.AddWithValue("@address", client.address);
                        cmd.Parameters.AddWithValue("@id", client.id);
                        cmd.ExecuteNonQuery();
                        
                    }
                   
       
                }
            }
            catch (Exception e)
            {

                errorMessage = e.Message;
            }

            Response.Redirect("/Clients/index");
        }
    }
}
