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
    public List<Student> students { get; set; }
    public void OnGet()
    {
      SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
      builder.DataSource = "cloudcomputingclassdb.database.windows.net";
      builder.UserID = "mattink37";
      builder.Password = "a:U7wp_a";
      builder.InitialCatalog = "CloudComputingClassDB";
      SqlConnection connection = new SqlConnection(builder.ConnectionString);

      string query = "select * from grades";
      SqlCommand cmd = new SqlCommand(query, connection);
      connection.Open();

      SqlDataReader reader = cmd.ExecuteReader();
      students = new List<Student>();
      while (reader.Read()) 
      {
        var studentId = reader.GetValue(0);
        var grade = reader.GetValue(1);

        var studentExists = false;
        foreach (var student in students)
        {
          if (student.id == (int)studentId)
          {
            student.grades.Add(Convert.ToSingle(grade));
            studentExists = true;
            break;
          }
        }
        if (!studentExists)
        {
          students.Add(new Student((int)studentId, new List<float>() { Convert.ToSingle(grade) }));
        }
      }
      connection.Close();
    }
  }
}
