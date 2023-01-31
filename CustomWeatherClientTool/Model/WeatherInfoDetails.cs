using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomWeatherClientTool.Model
{
    public class WeatherInfoDetails
    {
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }

        public decimal? generationtime_ms { get; set; }
        public int? utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }

        public decimal? elevation { get; set; }
        public CurrentWeather current_weather { get; set; }

    }
    public class CurrentWeather
    {
        public decimal? temperature { get; set; }
        public decimal? windspeed { get; set; }
        public decimal? winddirection { get; set; }
        public int? weathercode { get; set; }
        public string time { get; set; }
    }
}
