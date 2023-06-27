using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using ILogger = Serilog.ILogger;
namespace CRMCar.Controllers
{
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ILogger _logger;

        public CarsController()
        {
            _logger = Log.Logger;
        }

        private static List<Car> cars = new List<Car>()
        {
        new Car()
        {
            Id = 0,
            Name = "Marda",
            Brand = "Toyota",
            Price = 100000,
        }
    };

        [HttpGet]
        [Route("/api/[controller]/get-all-cars")]
        public IActionResult Get() 
        { 
            return Ok(cars);
        }



        [HttpPost]
        [Route("/api/[controller]/add-car")]
        public IActionResult AddNewCar([FromBody] Car car)
        {
            _logger.Information("Start add new car");
            
            var existCar = cars.Where(x => x.Id == car.Id).FirstOrDefault();
            if (existCar != null)
            {
                return BadRequest($"Car has Id = {car.Id} is existed");
            }   
            cars.Add(car);
            var jsonCar = JsonConvert.SerializeObject(car);
            _logger.Information(jsonCar);
            return Ok(cars);
        }

        [HttpPut]
        [Route("/api/[controller]/update-car")]
        public IActionResult UpdateCar([FromBody] Car car)
        {
            _logger.Information("Update car");
            //var existCar = cars.Where(x => x.Id == car.Id).FirstOrDefault();
            //if (existCar == null)
            //{
            //    return BadRequest($"The car is not exist");
            //}
            var car1 = cars.Where(x => x.Id == car.Id).SingleOrDefault();
            if (car1 == null)
            {
                return BadRequest("car not exist");
            }
            car1.Name = car.Name;
            car1.Brand = car.Brand;
            car1.Price = car.Price;

            var jsonCar = JsonConvert.SerializeObject(car);
            _logger.Information(jsonCar);
            return Ok(cars);
        }

        [HttpDelete]
        [Route("/api/[controller]/delete-car/{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = cars.Where(x => x.Id == id).FirstOrDefault();
            if (car == null)
            {
                return BadRequest("Car not found");
            }
            cars.Remove(car);
            return Ok(cars);
        }

        [HttpGet]
        [Route("/api/[controller]/get-car-by-id/{id}")]
        public IActionResult GetCarById(int id)
        {
            return Ok(cars.Where(_ => _.Id == id).SingleOrDefault());
        }
    }
}
