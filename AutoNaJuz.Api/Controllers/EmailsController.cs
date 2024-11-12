using AutoNaJuz.Services.ConcreteServices;
using AutoNaJuz.Web;
using Microsoft.AspNetCore.Mvc;

namespace AutoNaJuz.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailsController(EmailService emailService) : ControllerBase
    {
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDto emailDto)
        {
            await emailService.SendEmailAsync(emailDto.To, emailDto.Subject, emailDto.Message);
            return Ok("E-mail został wysłany pomyślnie");
        }
    }
}
