using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductivityTools.Salaries.Api.Database;
using ProductivityTools.Sallaries.Api.Contract;

namespace ProductivityTools.Sallaries.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalaryController : Controller
    {
        SalaryContext SalaryContext;

        public SalaryController(SalaryContext context)
        {
            this.SalaryContext = context;
        }

        [HttpGet("Test")]
        public string Test ()
        {
            return "pawel";
        }

        [HttpGet("List")]
        public List<Salary> GetSallaries()
        {
            var salaries=SalaryContext.Salaries.ToList();
            return salaries;
        }

        public IActionResult Index()
        {
            return View();
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
    }
}