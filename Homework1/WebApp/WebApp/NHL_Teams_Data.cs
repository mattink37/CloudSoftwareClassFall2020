using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace WebApp.wwwroot
{
    public class NHL_Teams_Data
    {
        public static string sortStatus { get; set; }
        public static List<string> nameList { get; set; }
        public static List<int> firstYearOfPlayList { get; set; }

        public NHL_Teams_Data(string json)
        {
            sortStatus = "None";
            nameList = new List<string>();
            firstYearOfPlayList = new List<int>();
            JObject jObject = JObject.Parse(json);
            JToken jTeams = jObject["teams"];
            for (int i = 0; i < jTeams.Count(); i++)
            {
                nameList.Add((string)jTeams[i]["name"]);
                firstYearOfPlayList.Add(int.Parse((string)jTeams[i]["firstYearOfPlay"]));
            }
            nameList.Add("Seattle Kracken");
            firstYearOfPlayList.Add(2021);
        }

        public static void SortTeamsByFirstYearOfPlay()
        {
            int n = firstYearOfPlayList.Count;
            for (int i = 1; i < n; ++i)
            {
                int year_key = firstYearOfPlayList[i];
                string name_key = nameList[i];
                int j = i - 1;
 
                while (j >= 0 && firstYearOfPlayList[j] > year_key)
                {
                    firstYearOfPlayList[j + 1] = firstYearOfPlayList[j];
                    nameList[j + 1] = nameList[j];
                    j = j - 1;
                }
                firstYearOfPlayList[j + 1] = year_key;
                nameList[j + 1] = name_key;
            }
            sortStatus = "Sorted by first year of play";
        }
    }
}
