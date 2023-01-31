using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomWeatherClientTool.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CustomWeatherClientTool.Model;
using Microsoft.Extensions.Options;

namespace CustomWeatherClientTool.API.Tests
{
    [TestClass()]
    public class WeatherInfoAPITests
    {
        [TestMethod()]
        public void Getforecast_Result_ThrowsException_InvalidAPI()
        {
            decimal lat = 28.6600m;
            decimal lng = 77.2300m;
            bool curr = true;

            var mock = new Mock<IOptions<Settings>>();
            mock.Setup(p => p.Value.Forecastapi).Returns("sdfsdfsfdsdf");
            WeatherInfoAPI weatherInfoAPI = new WeatherInfoAPI(mock.Object);
            var result = weatherInfoAPI.Getforecast(lat, lng, curr);
            
            Assert.Fail("failed", result);
        }
    }
}