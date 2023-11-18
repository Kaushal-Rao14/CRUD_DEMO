using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace CRUD_DEMO.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public clientsInfo client = new clientsInfo();
        public string errorMessage = string.Empty;
        public string successMessage = string.Empty;
        public void OnGet()
        {
        }
        public void OnPost()
        {
            client.name = Request.Form["name"];
            client.email = Request.Form["email"];
            client.mobile = Request.Form["phone"];
            client.address = Request.Form["address"];
           
            if(client.name.Length==0 || client.email.Length==0 || client.mobile.Length==0 || client.address.Length==0)
            {
                errorMessage = "All field required";
                return;
            }
            //saving to database

            try
            {
                string connectionString = "Data Source=.\\SqlExpress;Initial Catalog=dbCRUD_APP;Integrated Security=True;Encrypt=false";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlquery = "insert into clients(name,email,mobile,address) values(@name,@email,@mobile,@address)";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", client.name);
                        cmd.Parameters.AddWithValue("@email", client.email);
                        cmd.Parameters.AddWithValue("@mobile", client.mobile);
                        cmd.Parameters.AddWithValue("@address", client.address);
                        cmd.Parameters.AddWithValue("@id", client.id);
                        cmd.ExecuteNonQuery();
                        Response.Redirect("/Clients/index");

                    }
                }
             }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
            client.name = "";
            client.email = "";
            client.mobile = "";
            client.address = "";
            successMessage = "New Client added successfully";
        }
    }
}
