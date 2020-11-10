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
    public IActionResult OnPost()
    {
      var id = Request.Form["id"];
      var grade = Request.Form["grade"];

      SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
      builder.DataSource = "cloudcomputingclassdb.database.windows.net";
      builder.UserID = "mattink37";
      builder.Password = "a:U7wp_a";
      builder.InitialCatalog = "CloudComputingClassDB";
      SqlConnection connection = new SqlConnection(builder.ConnectionString);

      string query = $"insert into Grades (id, grade) values ('{id}', '{grade}');";
      SqlCommand cmd = new SqlCommand(query, connection);
      connection.Open();

      SqlDataReader reader = cmd.ExecuteReader();

      string url = "/GradeView";
      return Redirect(url);
    }
  }
}
