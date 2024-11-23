using AutoNaJuz.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoNaJuz.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController(IImagesService imageService) : Controller
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var images = await imageService.GetAll();

        return Ok(images);
    }
    
    [HttpGet("{id}/info")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetInfo(int id)
    {
        var image = await imageService.GetById(id);

        if (image is null)
        {
            return NotFound();
        }

        return Ok(image);
    }
    
    [HttpGet("{id}/blob")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBlob(int id)
    {
        var image = await imageService.GetBlobById(id);

        if (image is null)
        {
            return NotFound();
        }

        return File(image.Value.blob, image.Value.mime);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        await using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var bytes = ms.ToArray();
        
        var id = await imageService.Upload(bytes, file.ContentType);
        
        return CreatedAtAction(nameof(Upload), new { id }, id);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var image = await imageService.GetById(id);

        if (image is null)
        {
            return NotFound();
        }

        await imageService.Delete(id);

        return NoContent();
    }
}