using Microsoft.AspNetCore.Mvc;
using CarsApp.Data;
using CarsApp.Data.Models;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarDbContext _dbContext;

        public CarsController(CarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //a. Get all cars
        [HttpGet]
        public IActionResult GetAllCars() 
        {
            var cars = _dbContext.Cars.ToList();
            return Ok(cars);
        }

        //b. Get cars by brand
        [HttpGet("brand/{brand}")]
        public IActionResult GetCarsByBrand(String brand)
        {
            var cars = _dbContext.Cars.Where(c => c.Brand == brand).ToList();
            return Ok(cars);
        }

        //c. Get car by car type
        [HttpGet("cartype/{carType}")]
        public IActionResult GetCarsByCarType(String carType)
        {
            var cars = _dbContext.Cars.Where(c => c.CarType == carType).ToList();
            return Ok(cars);
        }

        //d. Get car by model name 
        [HttpGet("modelname/{modelName}")]
        public IActionResult GetCarsByModelName(String modelName)
        {
            var cars = _dbContext.Cars.Where(c => c.ModelName == modelName).ToList();
            return Ok(cars);
        }

        //e. Get car by model year 
        [HttpGet("modelyear/{modelYear}")]
        public IActionResult GetCarsByModelYear(int modelYear)
        {
            var cars = _dbContext.Cars.Where(c => c.ModelYear == modelYear).ToList();
            return Ok(cars);
        }

        //f. Get car by fuel type
        [HttpGet("fueltype/{fuelType}")]
        public IActionResult GetCarsByModelYear(string fuelType)
        {
            var cars = _dbContext.Cars.Where(c => c.FuelType == fuelType).ToList();
            return Ok(cars);
        }

        //g. Get car by transmission
        [HttpGet("transmission/{transmission}")]
        public IActionResult GetCarsByTransmission(string transmission)
        {
            var cars = _dbContext.Cars.Where(c => c.Transmission == transmission).ToList();
            return Ok(cars);
        }

        //h. Create car with all parameters
        [HttpPost]
        public IActionResult CreateCar([FromBody] Car car)
        {
            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetCarsByModelName), new { modelName = car.ModelName }, car);

        }

        //i. delete car
        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = _dbContext.Cars.Find(id);
            if (car == null)
            { 
                return NotFound();
            }
            _dbContext.Cars.Remove(car);
            _dbContext.SaveChanges();
            return NoContent();

        }

        //j. update car (only variant name and price can be updated)
        [HttpPut("{id}")]
        public IActionResult UpdateCar(int id, [FromBody] Car updatedCar)
        {
            var car = _dbContext.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }
            
            car.ModelVariant= updatedCar.ModelVariant;
            car.Price= updatedCar.Price;
            _dbContext.SaveChanges();
            return NoContent();
        }

        //k. get Car by Price Range
        [HttpGet("pricerange")]
        public IActionResult GetCarsByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var cars = _dbContext.Cars.Where(c => c.Price >= maxPrice && c.Price <= minPrice).ToList();
            return Ok(cars);
        }

        //l. get car by multiple filters
        [HttpGet("filters")]
        public IActionResult GetCarsByMultipleFilter([FromQuery] string? brand, 
                                                     [FromQuery] string? carType,
                                                     [FromQuery] string? fuelType,
                                                     [FromQuery] string? transmission)                                        
        {
            var cars = _dbContext.Cars.AsQueryable();
            if (!string.IsNullOrEmpty(brand))
                cars = cars.Where(c => c.Brand == brand);

            if (!string.IsNullOrEmpty(carType))
                cars = cars.Where(c => c.CarType == carType);

            if (!string.IsNullOrEmpty(fuelType))
                cars = cars.Where(c => c.FuelType == fuelType);

            if (!string.IsNullOrEmpty(transmission))
                cars = cars.Where(c => c.Transmission == transmission);

            return Ok(cars.ToList());


        }


        /* / GET: api/<CarsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CarsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CarsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CarsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        } */
    }
}
