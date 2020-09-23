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
    public class HomeModel : PageModel
    {
        public string Message { get; set; }

        public string User => (string)TempData[nameof(User)];

        public string Pass => (string)TempData[nameof(Pass)];



        public void OnGet()
        {

        }

        public IActionResult OnPostLogin([FromForm] string user, [FromForm] string pass)
        {

            if ((user != null) && (pass != null))
            {
                //TempData["loggedIn"] = "t";
                //TempData["username"] = user;
                HttpContext.Session.SetString("loggedin", "t");
                HttpContext.Session.SetString("username", user);
                String query = "SELECT username, password FROM dbo.users WHERE username = @username AND password = @password;";
                String connectionString = "Data Source=192.168.0.212;Initial Catalog=testDB;User ID=sa;Password=7v!SkU{r";
                using (SqlConnection connection = new SqlConnection(
                    connectionString))
                {
                    SqlCommand command = new SqlCommand(
                        query, connection);
                    command.Parameters.AddWithValue("@username", user);
                    command.Parameters.AddWithValue("@password", pass);
                    connection.Open();
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            /* while (reader.Read())
                             {
                                 Console.WriteLine(String.Format("{0}, {1}",
                                     reader[0], reader[1]));
                                 string test = reader.GetString(0);
                                 string test2 = reader.GetString(1);

                             }*/
                            if (reader.Read())
                            {
                                if (reader.GetString(0) == "admin")
                                {
                                    connection.Close();
                                    return RedirectToPage("Process");
                                }
                                else
                                {
                                    connection.Close();
                                    return RedirectToPage("Request");
                                }
                            }
                            else //incorrect login information
                            {
                                connection.Close();
                                return RedirectToPage("login");
                            }

                        }


                        
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        //incorrect login information
                        connection.Close();
                        return RedirectToPage("login");
                    }
                }






            }
            else
                return RedirectToPage("login");

        }

        public IActionResult OnPostCreate()
        {
            return RedirectToPage("NewAccount");
        }



    }
}
