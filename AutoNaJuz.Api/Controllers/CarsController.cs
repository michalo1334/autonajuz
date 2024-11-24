using Microsoft.AspNetCore.Mvc;
using AutoNaJuz.Services.Interfaces;
using AutoNaJuz.ViewModels.Car;
using AutoNaJuz.ViewModels.CarRental;
using AutoNaJuz.ViewModels.Image;

namespace AutoNaJuz.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController(
    ICarsService carsService,
    ICarRentalsService carRentalsService) : ControllerBase
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
    public async Task<ActionResult<IEnumerable<GetCarVm>>> GetAll(string? search, string? byBrand, string? byType)
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
    public async Task<ActionResult<GetCarVm>> Get(int id)
    {
        var carVm = await carsService.GetById(id);
        if (carVm == null)
        {
            return NotFound();
        }

        return Ok(carVm);
    }

    /// <summary>
    /// Creates a new car.
    /// </summary>
    /// <param name="request"></param>
    /// <returns>ID of the created car.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> New([FromBody] CreateCarVm request)
    {
        var id = await carsService.Create(request);
        return CreatedAtAction(nameof(New), new { id }, id);
    }

    /// <summary>
    /// Updates an existing car.
    /// </summary>
    /// <param name="id">Car ID.</param>
    /// <param name="request"></param>
    /// <returns>Action result.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] EditCarVm request)
    {
        var exists = (await carsService.GetById(id))?.Id != null;
        if (!exists)
        {
            return BadRequest();
        }

        await carsService.Update(request with { Id = id });
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
    
    [HttpGet("{id}/rentals")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<GetCarRentalVm>>?> GetRentals(int id)
    {
        var carVm = await carsService.GetById(id);
        if (carVm == null)
        {
            return NotFound();
        }

        var rentals = await carRentalsService.GetAllByCarId(id);
        return Ok(rentals);
    }
    
    [HttpPost("{id}/rent")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Rent(int id, [FromBody] CreateCarRentalVm request)
    {
        var carVm = await carsService.GetById(id);
        if (carVm == null)
        {
            return BadRequest();
        }

        var rentalId = await carRentalsService.Create(request with { CarId = id });
        return CreatedAtAction(nameof(Rent), new { id = rentalId }, rentalId);
    }

    [HttpGet("{id}/images")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ImageVm>>> GetImages(int id)
    {
        var carVm = await carsService.GetById(id);
        
        if (carVm == null)
        {
            return NotFound();
        }
        
        var images = await carsService.GetImagesByCarId(id);
        
        return Ok(images);
    }
    
    [HttpGet("features")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CarFeatureVm>>> GetAllFeatures()
    {
        var features = await carsService.GetAllFeatures();
        return Ok(features);
    }
    
    [HttpGet("features/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarFeatureVm>> GetFeature(int id)
    {
        var feature = await carsService.GetFeatureById(id);
        if (feature == null)
        {
            return NotFound();
        }

        return Ok(feature);
    }
    
    [HttpPost("features")]
    public async Task<ActionResult<int>> CreateFeature([FromBody] CarFeatureVm request)
    {
        var id = await carsService.CreateFeature(request.Title);
        return CreatedAtAction(nameof(CreateFeature), new { id }, id);
    }
    
    [HttpPut("features/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateFeature(int id, [FromBody] CarFeatureVm request)
    {
        var exists = (await carsService.GetFeatureById(id))?.Id != null;
        if (!exists)
        {
            return BadRequest();
        }

        await carsService.UpdateFeature(id, request.Title);
        return NoContent();
    }
    
    [HttpDelete("features/{id}")]
    public async Task<IActionResult> DeleteFeature(int id)
    {
        var feature = await carsService.GetFeatureById(id);
        if (feature == null)
        {
            return BadRequest();
        }

        await carsService.DeleteFeature(id);
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