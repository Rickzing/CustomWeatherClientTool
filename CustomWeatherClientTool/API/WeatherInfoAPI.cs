using CustomWeatherClientTool.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CustomWeatherClientTool.API
{
    public class WeatherInfoAPI
    {
        private Settings _settings;

        public WeatherInfoAPI(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }


        public async Task<WeatherInfoDetails> Getforecast(decimal lat,decimal lng, bool curr=true)
        {
            WeatherInfoDetails details = null;
            try
            {
                using(var client = new HttpClient())
                {

                    using(var request= new HttpRequestMessage())
                    {
                        string url = _settings.Forecastapi + "?latitude=" + lat + "&longitude=" + lng + "&current_weather=" + curr;

                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        request.Method = HttpMethod.Get;
                        request.RequestUri= new System.Uri(url);
                        var response = client.GetAsync(url).Result;
                        response.EnsureSuccessStatusCode();
                        details= await response.Content.ReadFromJsonAsync<WeatherInfoDetails>();
                    }
                }

            }
            catch (Exception ex) { }

            return details;
        }
    }
}
