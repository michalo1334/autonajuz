using AutoNaJuz.Services.Interfaces;
using AutoNaJuz.ViewModels.RenterInfo;
using Microsoft.AspNetCore.Mvc;

namespace AutoNaJuz.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RenterInfosController(
    IRenterInfoService renterInfoService,
    ICarRentalsService carRentalsService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GetRenterInfoVm>>> GetAll()
    {
        var renterInfos = await renterInfoService.GetAll();
        return Ok(renterInfos);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetRenterInfoVm>> Get(int id)
    {
        var renterInfoVm = await renterInfoService.GetById(id);
        if (renterInfoVm == null)
        {
            return NotFound();
        }

        return Ok(renterInfoVm);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> New([FromBody] CreateRenterInfoVm request)
    {
        var id = await renterInfoService.Create(request);
        return CreatedAtAction(nameof(New), new { id }, id);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Update([FromBody] EditRenterInfoVm request)
    {
        await renterInfoService.Update(request);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(int id)
    {
        await renterInfoService.Delete(id);
        return NoContent();
    }
    
    [HttpGet("{id}/rentals")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GetRenterInfoVm>>> GetRentals(int id)
    {
        var rentalInfos = await carRentalsService.GetAllByRenterId(id);
        return Ok(rentalInfos);
    }
}