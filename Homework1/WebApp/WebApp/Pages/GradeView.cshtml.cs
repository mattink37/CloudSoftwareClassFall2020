using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
  public class GradeViewModel : PageModel
  {
    public List<string> students { get; set; }
    public void OnGet()
    {
      SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
      builder.DataSource = "cloudcomputingclassdb.database.windows.net";
      builder.UserID = "mattink37";
      builder.Password = "a:U7wp_a";
      builder.InitialCatalog = "CloudComputingClassDB";
      SqlConnection connection = new SqlConnection(builder.ConnectionString);

      string query = "select * from students";
      SqlCommand cmd = new SqlCommand(query, connection);
      connection.Open();

      SqlDataReader reader = cmd.ExecuteReader();
      students = new List<string>();
      while (reader.Read()) { students.Add(reader.GetString(0) + " " + reader.GetString(1) + ": " + reader.GetValue(2)); }
    }
  }
}
