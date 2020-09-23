using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace WebApplication7.Pages
{
    public class RequestModel : PageModel
    {
        public void OnGet()
        {
            if (((String)HttpContext.Session.GetString("loggedin") != "t"))
                Response.Redirect("login");
            if ((String)HttpContext.Session.GetString("username") == "admin")
                Response.Redirect("Process");

        }

        public IActionResult OnPost()
        {
            String query = "INSERT INTO dbo.request (username,status) VALUES (@username, @status)";
            String connectionString = "Data Source=192.168.0.212;Initial Catalog=testDB;User ID=sa;Password=7v!SkU{r";
            using (SqlConnection connection2 = new SqlConnection(
                connectionString))
            {
                SqlCommand command = new SqlCommand(
                    query, connection2);
                command.Parameters.AddWithValue("@username", (String)HttpContext.Session.GetString("username"));
                command.Parameters.AddWithValue("@Status", 0);
                connection2.Open();
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(String.Format("{0}, {1}",
                                reader[0], reader[1]));
                        }
                    }
                    //lb_error.Visible = false;
                }
                catch (System.Data.SqlClient.SqlException) //request is pending
                {

                }
            }
            return RedirectToPage("Request");
        }
    }
}
