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
        public string Test()
        {
            return "pawel";
        }

        [HttpPost("List")]
        public List<Salary> GetSallaries(Salary filter)
        {
            var salaries = SalaryContext.Salaries.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                salaries=salaries.Where(x=>x.Name.Contains(filter.Name));
            }

            if (!string.IsNullOrEmpty(filter.Company))
            {
                salaries = salaries.Where(x => x.Company.Contains(filter.Company));
            }
            var result=salaries.ToList();
            return result;
        }

        [HttpPost("Update")]
        public IActionResult UdpdateSalary(Salary salary)
        {
            
            return Ok("fs");

        }

        [HttpPost("Add")]
        public IActionResult AddSalary(Salary salary)
        {
            SalaryContext.Add(salary);
            SalaryContext.SaveChanges();
            return Ok("fs");

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}