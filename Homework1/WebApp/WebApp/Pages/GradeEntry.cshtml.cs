using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApp.Pages
{
  public class GradeEntryModel : PageModel
  {
    public void OnPost()
    {
      var studentName = Request.Form["studentname"];
      var numericalGrade = Request.Form["numericalgrade"];
    }
    public void OnGet()
    {

    }
  }
}
