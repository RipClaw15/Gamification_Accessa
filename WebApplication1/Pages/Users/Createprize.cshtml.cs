using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.IO;

namespace WebApplication1.Pages.Users
{
    public class CreateprizeModel : PageModel
    {
        public PrizeInfo listPrize = new PrizeInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            var userId = HttpContext.Session.GetString("id");
            if (userId == null)
            {
                Response.Redirect("/Users/Login");
            }
           
        }
        public void OnPost()
        {
            listPrize.name = Request.Form["name"];

            listPrize.address = Request.Form["address"];

            listPrize.description = Request.Form["description"];

            if (listPrize.name.Length == 0 ||
               listPrize.address.Length == 0 ||
               listPrize.description.Length == 0
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
                    String sql = "insert into prizes " +
                                "(name, address, description) VALUES " +
                                "(@name, @address, @description);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", listPrize.name);
                        command.Parameters.AddWithValue("@address", listPrize.address);

                        command.Parameters.AddWithValue("@description", listPrize.description);


                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            listPrize.name = "";
            listPrize.address = ""; listPrize.description = "";
            successMessage = "New Prize Added Successfully";

            Response.Redirect("/Users/Prizes");
        }
    }
}
