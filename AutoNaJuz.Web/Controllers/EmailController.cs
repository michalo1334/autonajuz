using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace AutoNaJuz.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDto emailDto)
        {
            await _emailService.SendEmailAsync(emailDto.To, emailDto.Subject, emailDto.Message);
            return Ok("E-mail został wysłany pomyślnie");
        }
    }
}
