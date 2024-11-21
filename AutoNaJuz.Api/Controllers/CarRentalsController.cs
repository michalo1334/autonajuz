using AutoNaJuz.Services.Interfaces;
using AutoNaJuz.ViewModels.CarRental;
using AutoNaJuz.ViewModels.RenterInfo;
using Microsoft.AspNetCore.Mvc;

namespace AutoNaJuz.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarRentalsController(
    ICarRentalsService carRentalsService,
    IRenterInfoService renterInfoService) : Controller
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GetCarRentalVm>>?> GetAll()
    {
        var carRentals = await carRentalsService.GetAll();
        return Ok(carRentals);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetCarRentalVm>> Get(int id)
    {
        var carRentalVm = await carRentalsService.GetById(id);
        if (carRentalVm == null)
        {
            return NotFound();
        }

        return Ok(carRentalVm);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> New([FromBody] CreateCarRentalVm request)
    {
        var id = await carRentalsService.Create(request);
        return CreatedAtAction(nameof(New), new { id }, id);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Update([FromBody] EditCarRentalVm request)
    {
        await carRentalsService.Update(request);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(int id)
    {
        await carRentalsService.Delete(id);
        return NoContent();
    }
    
    [HttpGet("{id}/renter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetRenterInfoVm>> GetRenter(int id)
    {
        var carRentalVm = await carRentalsService.GetById(id);
        if (carRentalVm == null)
        {
            return NotFound();
        }

        var renterInfoVm = await renterInfoService.GetById(carRentalVm.RenterId);
        if (renterInfoVm == null)
        {
            return NotFound();
        }

        return Ok(renterInfoVm);
    }
}