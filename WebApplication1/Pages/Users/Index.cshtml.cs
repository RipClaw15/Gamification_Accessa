using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Pages.Users
{
    public class IndexModel : PageModel
    {
        public List<UserInfo> listUsers = new List<UserInfo>();


        public void OnGet()
        {

            var userId = HttpContext.Session.GetString("id");
            if (userId == null)
            {
                Response.Redirect("/Users/Login");
            }


            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=gamification;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM users";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
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
                                userInfo.badge_in_last_quarter = reader.GetString(13);
                                userInfo.cons_units = reader.GetString(14);
                                userInfo.units_year = reader.GetString(15);
                                userInfo.leader = reader.GetString(16);

                                listUsers.Add(userInfo);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }


    }

    public class UserInfo
    {
        public String id;
        public String name;
        public String email;
        public String password;
        public String phone;
        public String address;
        public String position;
        public String units_this_month;
        public String units_last_month;
        public String platinum;
        public String diamond;
        public String gold;
        public String silver;
        public String badge_in_last_quarter;
        public String cons_units;
        public String units_year;
        public String leader;
    }
}
