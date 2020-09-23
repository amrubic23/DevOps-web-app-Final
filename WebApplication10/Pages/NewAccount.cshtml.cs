using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication7.Pages
{
    public class NewAccountModel : PageModel
    {
        public string User => (string)TempData[nameof(User)];

        public string Pass1 => (string)TempData[nameof(Pass1)];

        public string Pass2 => (string)TempData[nameof(Pass2)];
        public void OnGet()
        {
        }

        public IActionResult OnPost([FromForm] string user, [FromForm] string pass1, [FromForm] string pass2)
        {
            if ((user == null) || (pass1 == null) || (pass2 == null))
            { return RedirectToPage("NewAccount"); }
            else if (pass1 != pass2)
            { return RedirectToPage("NewAccount"); }
            else
            {
                
                String query = "INSERT INTO dbo.users (username,password) VALUES (@username, @password)";
                String connectionString = "Data Source=192.168.0.212;Initial Catalog=testDB;User ID=sa;Password=7v!SkU{r";
                using (SqlConnection connection = new SqlConnection(
                    connectionString))
                {
                    SqlCommand command = new SqlCommand(
                        query, connection);
                    command.Parameters.AddWithValue("@username", user);
                    command.Parameters.AddWithValue("@password", pass1);
                    connection.Open();
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
                        connection.Close();
                        return RedirectToPage("Login");
                        
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {

                        connection.Close();
                        return RedirectToPage("NewAccount");
                    }
                }
            }

        }
    }
}
