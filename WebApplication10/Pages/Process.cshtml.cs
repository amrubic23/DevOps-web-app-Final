using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication7.Pages
{
    public class ProcessModel : PageModel
    {
        public void OnGet()
        {
            if ((String)HttpContext.Session.GetString("username") != "admin")
                Response.Redirect("Request");
        }
    }
}
