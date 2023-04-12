using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Pages.Users
{
    public class PrizesModel : PageModel
    {

        public List<PrizeInfo> listPrize = new List<PrizeInfo>();
        public String errorMessage = "";
        public String successMessage = "";
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
                    String sql = "SELECT * FROM prizes";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PrizeInfo prizeInfo = new PrizeInfo();
                                prizeInfo.id = "" + reader.GetInt32(0);
                                prizeInfo.name = reader.GetString(1);

                                prizeInfo.address = reader.GetString(2);

                                prizeInfo.description = reader.GetString(3);

                                listPrize.Add(prizeInfo);
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
        public void OnPost()
        {

        }
    }
    public class PrizeInfo
    {
        public String id;
        public String name;
        public String address;
        public String description;
    }
}
