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
    public NHL_Teams_Data teams;
    public string sortType;
    public void OnGet()
    {
      WebClient wc = new WebClient();
      response = wc.DownloadString("https://statsapi.web.nhl.com/api/v1/teams");

      teams = new NHL_Teams_Data(response);
    }

    public void OnPost()
    {
      sortType = Request.Form["sortType"];

      switch (sortType)
      {
        case "firstYearOfPlayAscending":
          NHL_Teams_Data.SortTeamsByAscendingFirstYearOfPlay();
          break;
        case "firstYearOfPlayDescending":
          NHL_Teams_Data.SortTeamsByDescendingFirstYearOfPlay();
          break;
        default:
          break;
      }
    }
  }
}
