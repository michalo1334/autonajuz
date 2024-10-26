using Microsoft.AspNetCore.Mvc;
using AutoNaJuz.Model;
using AutoNaJuz.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace AutoNaJuz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController(
        ICarsService carsService
    ) : ControllerBase
    {
        // GET: api/cars
        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetAll(string search, string byBrand, string byType)
        {
            // Replace with actual data retrieval logic
            var cars = new List<Car>();
            return Ok(cars);
        }

        // GET: api/cars/5
        [HttpGet("{id}")]
        public ActionResult<Car> Get(int id)
        {
            // Replace with actual data retrieval logic
            var car = new Car();
            return Ok(car);
        }

        // POST: api/cars
        //[Authorize("Roles = manager")]
        [HttpPost]
        public ActionResult<int> New([FromBody] Car car)
        {
            // Replace with actual data saving logic
            car.Id = 3; // Simulate new ID generation
            return CreatedAtAction(nameof(New), new { id = car.Id }, car.Id);
        }

        // PUT: api/cars/5
        [HttpPut("{id}")]
        //[Authorize("Roles = manager")]
        public IActionResult Update(int id, [FromBody] Car car)
        {
            // Replace with actual data update logic
            if (id != car.Id)
            {
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE: api/cars/5
        //[Authorize("Roles = manager")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Replace with actual data deletion logic
            return NoContent();
        }

        // POST: api/cars/seed
        //[Authorize("Roles = manager")]
        [HttpPost("seed")]
        public IActionResult Seed()
        {
            // Replace with actual data seeding logic
            return NoContent();
        }
    }
}