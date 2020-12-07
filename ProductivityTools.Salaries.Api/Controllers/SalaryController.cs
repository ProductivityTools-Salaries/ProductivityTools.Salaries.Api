using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        public IEnumerable<Salary> GetSallaries(Salary filter)
        {
            var salaries = SalaryContext.Salaries.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                salaries = salaries.Where(x => x.Name.Contains(filter.Name));
            }

            if (!string.IsNullOrEmpty(filter.Company))
            {
                salaries = salaries.Where(x => x.Company.Contains(filter.Company));
            }

            if (!string.IsNullOrEmpty(filter.Comment))
            {
                salaries = salaries.Where(x => x.Comment.Contains(filter.Comment));
            }
            if (filter.B2b.HasValue && filter.B2b.Value)
            {
                salaries = salaries.Where(x => x.Comment.Contains(filter.Comment));
            }
            var result = salaries.OrderByDescending(x => x.CreationDate);
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
            return Ok(salary);
        }

        [HttpPost("Remove")]
        public IActionResult RemoveSalary(int salaryId)
        {

            var salary = SalaryContext.Salaries.Find(salaryId);
            SalaryContext.Remove(salary);
            SalaryContext.SaveChanges();
            return Ok($"Removed {salaryId}");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}