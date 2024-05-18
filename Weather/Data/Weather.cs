using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Data
{
    public class WeatherForApp
    {
        public WeatherForApp(DateTime dateTime, int temperature)
        {
            this.dateTime = dateTime;
            this.temperature = temperature;
            this.WeatherStatus = this.WeatherStatus;
        }
        public DateTime dateTime;

        public int temperature { get; set; } = 0;

        public string WeatherStatus { get; set; } = "";

    }
}
