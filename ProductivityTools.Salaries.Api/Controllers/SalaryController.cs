using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductivityTools.Salaries.Api.Database;
using ProductivityTools.Salaries.Api.Views;
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
        [Authorize]
        public IEnumerable<Salary> GetSallaries(SalaryFilter filter)
        {
            var salaries = SalaryContext.Salaries.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Position))
            {
                salaries = salaries.Where(x => x.Position.Contains(filter.Position));
            }
            if (filter.B2b.HasValue)
            {
                salaries = salaries.Where(x => x.B2b == filter.B2b.Value);
            }

            if (!string.IsNullOrEmpty(filter.Company))
            {
                salaries = salaries.Where(x => x.Company.Contains(filter.Company));
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                salaries = salaries.Where(x => x.Name.Contains(filter.Name));
            }

            if (!string.IsNullOrEmpty(filter.Source))
            {
                salaries = salaries.Where(x => x.Source.Contains(filter.Source));
            }

            if (!string.IsNullOrEmpty(filter.Comment))
            {
                salaries = salaries.Where(x => x.Comment.Contains(filter.Comment));
            }

            if (!string.IsNullOrEmpty(filter.OrderBy))
            {
                switch (filter.OrderBy)
                {
                    case "position": salaries = salaries.OrderBy(x => x.Position); break;
                    case "value": salaries = salaries.OrderBy(x => x.Value); break;
                    case "totalvalue": salaries = salaries.OrderBy(x => x.Total); break;
                    case "equity": salaries = salaries.OrderBy(x => x.Equity); break;
                    case "b2b": salaries = salaries.OrderBy(x => x.B2b); break;
                    case "company": salaries = salaries.OrderBy(x => x.Company); break;
                    case "name": salaries = salaries.OrderBy(x => x.Name); break;
                    case "creationDate": salaries = salaries.OrderBy(x => x.CreationDate); break;
                    default: salaries = salaries.OrderBy(x => x.CreationDate); break;
                }
            }

            if (!string.IsNullOrEmpty(filter.OrderByDescending))
            {
                switch (filter.OrderByDescending)
                {
                    case "position": salaries = salaries.OrderByDescending(x => x.Position); break;
                    case "value": salaries = salaries.OrderByDescending(x => x.Value); break;
                    case "totalvalue": salaries = salaries.OrderByDescending(x => x.Total); break;
                    case "equity": salaries = salaries.OrderByDescending(x => x.Equity); break;
                    case "b2b": salaries = salaries.OrderByDescending(x => x.B2b); break;
                    case "company": salaries = salaries.OrderByDescending(x => x.Company); break;
                    case "name": salaries = salaries.OrderByDescending(x => x.Name); break;
                    case "creationDate": salaries = salaries.OrderByDescending(x => x.CreationDate); break;
                    default: salaries = salaries.OrderByDescending(x => x.CreationDate); break;
                }
            }

            var result = salaries;
            return result;
        }

        [HttpPost("Save")]
        public IActionResult AddSalary(Salary salary)
        {
            if (salary.SalaryId == 0)
            {
                SalaryContext.Add(salary);
            }
            else
            {
                SalaryContext.Update(salary);

            }
            SalaryContext.SaveChanges();
            return Ok(salary);
        }

        [HttpPost("Remove")]
        [Authorize]
        public IActionResult RemoveSalary(int salaryId)
        {
            var salary = SalaryContext.Salaries.Find(salaryId);
            SalaryContext.Remove(salary);
            SalaryContext.SaveChanges();
            return Ok($"Removed {salaryId}");
        }

        [HttpPost("Get")]
        [Authorize]
        public IActionResult GetSalary([FromQuery] int salaryId)
        {

            var salary = SalaryContext.Salaries.Find(salaryId);
            return Ok(salary);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}