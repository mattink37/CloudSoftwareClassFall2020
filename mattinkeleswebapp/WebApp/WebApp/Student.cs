using System.Collections.Generic;

namespace WebApp
{
  public class Student
  {
    public int id { get; set; }
    public List<float> grades { get; set; }

    public Student(int id, List<float> grades)
    {
      this.id = id;
      this.grades = grades;
    }
  }
}
