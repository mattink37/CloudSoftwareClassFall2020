using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApp.wwwroot;

namespace WebApp.Pages
{
    public class nhl_statsModel : PageModel
    {
        private string response;
        public NHL_Teams_Data team;
        public void OnGet()
        {
            WebClient wc = new WebClient();
            response = wc.DownloadString("https://statsapi.web.nhl.com/api/v1/teams");

            team = new NHL_Teams_Data(response);
        }
    }
}
