using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AutoNaJuz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalsController : Controller
    {
        private readonly ILogger<CarRentalsController> _logger;

        public CarRentalsController(ILogger<CarRentalsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}