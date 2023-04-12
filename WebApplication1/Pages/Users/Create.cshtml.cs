using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication1.Pages.Users
{
    public class CreateModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            var userId = HttpContext.Session.GetString("id");
            if (userId == null)
            {
                Response.Redirect("/Users/Login");
            }
            if (userId != "1")
            {

                Response.Redirect("/Users/Index");
            }
        }
        public void OnPost()
        {
            userInfo.name = Request.Form["name"];
            userInfo.email = Request.Form["email"];
            userInfo.password = Request.Form["password"];
            userInfo.phone = Request.Form["phone"];
            userInfo.address = Request.Form["address"];
            userInfo.position = Request.Form["position"];
            userInfo.units_this_month = Request.Form["units_this_month"];
            userInfo.units_last_month = Request.Form["units_last_month"];
            userInfo.platinum = Request.Form["platinum"];
            userInfo.diamond = Request.Form["diamond"];
            userInfo.gold = Request.Form["gold"];
            userInfo.silver = Request.Form["silver"];
            userInfo.badge_in_last_quarter = Request.Form["badge_in_last_quarter"];
            userInfo.cons_units = Request.Form["cons_units"];
            userInfo.units_year = Request.Form["units_year"];
            userInfo.leader = Request.Form["leader"];

            if (userInfo.name.Length == 0 || userInfo.email.Length == 0 ||
               userInfo.password.Length == 0 || userInfo.phone.Length == 0 ||
               userInfo.address.Length == 0 || userInfo.position.Length == 0 ||
               userInfo.units_this_month.Length == 0 || userInfo.units_last_month.Length == 0 ||
               userInfo.platinum.Length == 0 || userInfo.diamond.Length == 0 ||
               userInfo.gold.Length == 0 || userInfo.silver.Length == 0 ||
               userInfo.badge_in_last_quarter.Length == 0 || userInfo.cons_units.Length == 0 ||
               userInfo.units_year.Length == 0 || userInfo.leader.Length == 0
                )
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
                    String sql = "insert into users " +
                                "(name, email, password, phone, address, position, units_this_month, units_last_month, platinum, diamond, gold, silver, badge_in_last_quarter, cons_units, units_year,leader) VALUES " +
                                "(@name, @email, @password, @phone, @address, @position, @units_this_month, @units_last_month, @platinum, @diamond, @gold, @silver, @badge_in_last_quarter, @cons_units, @units_year, @leader);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userInfo.name);
                        command.Parameters.AddWithValue("@email", userInfo.email);
                        command.Parameters.AddWithValue("@password", userInfo.password);
                        command.Parameters.AddWithValue("@phone", userInfo.phone);
                        command.Parameters.AddWithValue("@address", userInfo.address);
                        command.Parameters.AddWithValue("@position", userInfo.position);
                        command.Parameters.AddWithValue("@units_this_month", userInfo.units_this_month);
                        command.Parameters.AddWithValue("@units_last_month", userInfo.units_last_month);
                        command.Parameters.AddWithValue("@platinum", userInfo.platinum);
                        command.Parameters.AddWithValue("@diamond", userInfo.diamond);
                        command.Parameters.AddWithValue("@gold", userInfo.gold);
                        command.Parameters.AddWithValue("@silver", userInfo.silver);
                        command.Parameters.AddWithValue("@badge_in_last_quarter", userInfo.badge_in_last_quarter);
                        command.Parameters.AddWithValue("@cons_units", userInfo.cons_units);
                        command.Parameters.AddWithValue("@units_year", userInfo.units_year);
                        command.Parameters.AddWithValue("@leader", userInfo.leader);


                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            userInfo.name = ""; userInfo.email = ""; userInfo.password = ""; userInfo.phone = "";
            userInfo.address = ""; userInfo.position = ""; userInfo.units_this_month = "";
            userInfo.units_last_month = ""; userInfo.platinum = ""; userInfo.diamond = ""; userInfo.gold = "";
            userInfo.silver = ""; userInfo.badge_in_last_quarter = ""; userInfo.cons_units = "";
            userInfo.units_year = ""; userInfo.leader = "";
            successMessage = "New User Added Successfully";

            Response.Redirect("/Users/Index");
        }
    }
}
