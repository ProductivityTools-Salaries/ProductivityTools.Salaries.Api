using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProductivityTools.Sallaries.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalaryController : Controller
    {
        [HttpGet("Test")]
        public string Test ()
        {
            return "pawel";
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}