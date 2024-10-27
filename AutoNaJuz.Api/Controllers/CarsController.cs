using Microsoft.AspNetCore.Mvc;
using AutoNaJuz.Model;
using AutoNaJuz.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace AutoNaJuz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController(ICarsService carsService) : ControllerBase
    {

        /// <summary>
        /// Retrieves all cars.
        /// </summary>
        /// <param name="search">Search term.</param>
        /// <param name="byBrand">Filter by brand.</param>
        /// <param name="byType">Filter by type.</param>
        /// <returns>List of cars.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Car>>> GetAll(string? search, string? byBrand, string? byType)
        {
            // Replace with actual data retrieval logic
            var cars = await carsService.GetAll();
            return Ok(cars);
        }

        /// <summary>
        /// Retrieves a car by ID.
        /// </summary>
        /// <param name="id">Car ID.</param>
        /// <returns>Car object.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Car>> GetAsync(int id)
        {
            // Replace with actual data retrieval logic
            var car = await carsService.GetById(id);
            return car == null ? NotFound() : Ok(car);
        }

        /// <summary>
        /// Creates a new car.
        /// </summary>
        /// <param name="car">Car object.</param>
        /// <returns>ID of the created car.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<int>> New([FromBody] Car car)
        {
            var id = await carsService.Create(car);
            return CreatedAtAction(nameof(New), new { id }, car.Id);
        }

        /// <summary>
        /// Updates an existing car.
        /// </summary>
        /// <param name="id">Car ID.</param>
        /// <param name="car">Updated car object.</param>
        /// <returns>Action result.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] Car car)
        {
            var exists = (await carsService.GetById(id))?.Id != null;
            if (!exists)
            {
                return BadRequest();
            }

            await carsService.Update(car);
            return Ok();
        }

        /// <summary>
        /// Deletes a car by ID.
        /// </summary>
        /// <param name="id">Car ID.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await carsService.GetById(id);
            if (car == null)
            {
                return BadRequest();
            }

            await carsService.Delete(car);
            return NoContent();
        }

        /// <summary>
        /// Seeds the car database.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpPost("seed")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Seed()
        {
            // Replace with actual data seeding logic
            return NoContent();
        }
    }
}