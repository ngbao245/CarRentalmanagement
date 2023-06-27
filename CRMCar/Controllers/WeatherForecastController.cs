using Microsoft.AspNetCore.Mvc;
using Ilogger = Serilog.ILogger;
using Serilog;
using Newtonsoft.Json;

namespace CRMCar.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        //private readonly ILogger<WeatherForecastController> _logger; //log này của microsoft, mình sẽ dùng log của mình
        private readonly Ilogger _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            //_logger = logger;
            _logger = Log.Logger;

        }

        [HttpGet()]
        [Route("[controller]/thoi-tiet")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.Information("Start get thoi-tiet");
            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList();
            var dataJson = JsonConvert.SerializeObject(data);
            _logger.Information(data.ToString());

            return data;
        }
    }
}