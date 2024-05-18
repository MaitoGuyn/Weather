using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Data
{
    public class DataContext
    {
        public static List<WeatherForApp> Weather = new List<WeatherForApp>();

        public static List<WeatherStatus> weatherStatuses = new List<WeatherStatus>()
        {
            new WeatherStatus { Status = "Sunny" },
            new WeatherStatus { Status = "Cloudy" },
            new WeatherStatus { Status = "Rainy" }
        };


        public static List<SortType> SortTypes = new List<SortType>()
        {
            new SortType { Title = "С самого ранего" },
            new SortType { Title = "С самого позднего" },
            new SortType { Title = "По возрастанию температуры" },
            new SortType { Title = "По убыванию температуры" }
        };
    }
}
