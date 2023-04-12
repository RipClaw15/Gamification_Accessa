using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Common;
using System.Data.SqlClient;

namespace WebApplication1.Pages.Users
{


    public class LoginModel : PageModel
    {

        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";



        public void OnGet()
        {
            var userId = HttpContext.Session.GetString("id");
            if (userId != null)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.Remove("id");
                Response.Redirect("/Users/Login");
            }

        }


        public void OnPost()
        {
            String email = Request.Form["email"];
            String password = Request.Form["password"];

            if (email.Length == 0 ||
               password.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=gamification;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "select * from users where email=@email and password=@password";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", password);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                UserInfo userInfo = new UserInfo();
                                userInfo.id = "" + reader.GetInt32(0);
                                userInfo.name = reader.GetString(1);
                                userInfo.email = reader.GetString(2);
                                userInfo.password = reader.GetString(3);
                                userInfo.phone = reader.GetString(4);
                                userInfo.address = reader.GetString(5);
                                userInfo.position = reader.GetString(6);
                                userInfo.units_this_month = reader.GetString(7);
                                userInfo.units_last_month = reader.GetString(8);
                                userInfo.platinum = reader.GetString(9);
                                userInfo.diamond = reader.GetString(10);
                                userInfo.gold = reader.GetString(11);
                                userInfo.silver = reader.GetString(12);
                                userInfo.leader = reader.GetString(13);
                                HttpContext.Session.SetString("id", userInfo.id);


                            }
                            else
                            {
                                errorMessage = "Invalid email or password";
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Users/Profile");
        }
    }
}
