using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.wwwroot
{
    public class NHL_Teams_Data
    {
        public List<string> nameList { get; set; }
        public List<int> firstYearOfPlayList { get; set; }

        public NHL_Teams_Data(string json)
        {
            nameList = new List<string>();
            firstYearOfPlayList = new List<int>();
            JObject jObject = JObject.Parse(json);
            JToken jTeams = jObject["teams"];
            for (int i = 0; i < jTeams.Count(); i++)
            {
                nameList.Add((string)jTeams[i]["name"]);
                firstYearOfPlayList.Add(int.Parse((string)jTeams[i]["firstYearOfPlay"]));
            }
        }
    }
}
