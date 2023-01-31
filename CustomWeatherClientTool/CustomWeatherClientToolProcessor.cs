using CustomWeatherClientTool.API;
using CustomWeatherClientTool.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomWeatherClientTool
{
    public class CustomWeatherClientToolProcessor
    {
        private List<CityList> _cityList;
        private WeatherInfoAPI _weatherInfoAPI;
        public CustomWeatherClientToolProcessor(IOptions<List<CityList>> citylist, WeatherInfoAPI weatherInfoAPI)
        {

            _cityList = citylist.Value;
            _weatherInfoAPI = weatherInfoAPI;

        }

        public void Execute()
        {
            Process();
        }

        private async void Process()
        {
            Console.WriteLine();
            Console.WriteLine("Please enter a city name:");
            string city = Console.ReadLine();
            if (IsCityValid(city))
            {
                var lonlat = GetCityDeatils(city);

                var details = await _weatherInfoAPI.Getforecast(lonlat.Item1, lonlat.Item2);
                if (details != null)
                {
                    Console.WriteLine("Weather forecast for the city {0} is as follows:", city);
                    Console.WriteLine();

                    Console.WriteLine("{0}={1}", "Temperature", details.current_weather.temperature);
                    Console.WriteLine("{0}={1}", "Windspeed", details.current_weather.windspeed);
                    Console.WriteLine("{0}={1}", "Winddirection", details.current_weather.winddirection);
                    Console.WriteLine("{0}={1}", "Weathercode", details.current_weather.weathercode);
                    Console.WriteLine("{0}={1}", "Time", details.current_weather.time);
                }

                Console.WriteLine("Do you wish to fetch forecast for another city y/n?");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Y)
                {
                    Execute();
                }

            }
            else
            {
                Console.WriteLine("Please enter a valid city name and try again. Do you wish to continue y/n?");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Y)
                {
                    Execute();
                }


            }


        }

        private Tuple<decimal, decimal> GetCityDeatils(string cityname)
        {

            decimal lat, lng;
            var cityinfo = _cityList?.Where(x => x.city.Trim().ToLower() == cityname.Trim().ToLower()).FirstOrDefault();

            decimal.TryParse(cityinfo?.lat, out lat);
            decimal.TryParse(cityinfo?.lng, out lng);

            return new Tuple<decimal, decimal>(lat, lng);

        }

        private bool IsCityValid(string cityname)
        {

            if (!string.IsNullOrEmpty(cityname))
            {
                var cityinfo = _cityList?.Where(x => x.city.Trim().ToLower() == cityname.Trim().ToLower()).FirstOrDefault();
                if (cityinfo != null)
                    return true;
                else
                    return false;
            }
            return false;
        }
    }

}
