using Microsoft.AspNetCore.Mvc;
using AutoNaJuz.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using AutoNaJuz.Model.Car;
using AutoNaJuz.ViewModels.Car;

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
        public async Task<ActionResult<IEnumerable<GetCarVM>>> GetAll(string? search, string? byBrand, string? byType)
        {
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
        public async Task<ActionResult<GetCarVM>> Get(int id)
        {
            // Replace with actual data retrieval logic
            var carVm = await carsService.GetById(id);
            if(carVm == null)
            {
                return NotFound();
            }
            return Ok(carVm);
        }

        /// <summary>
        /// Creates a new car.
        /// </summary>
        /// <param name="car">Car object.</param>
        /// <returns>ID of the created car.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<int>> New([FromBody] CreateOrEditCarVM request)
        {
            var id = await carsService.Create(request);
            return CreatedAtAction(nameof(New), new { id }, id);
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
        public async Task<IActionResult> Update(int id, [FromBody] CreateOrEditCarVM request)
        {
            var exists = (await carsService.GetById(id))?.Id != null;
            if (!exists)
            {
                return BadRequest();
            }

            await carsService.Update(id, request);
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
            var carVm = await carsService.GetById(id);
            if (carVm == null)
            {
                return BadRequest();
            }

            await carsService.Delete(id);
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