using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.Salaries.Api.Database;
using System;
using System.Linq;

namespace ProductivityTools.Salaries.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebugController : Controller
    {
        private readonly SalaryContext SalaryContext;

        public DebugController(SalaryContext context)
        {
            this.SalaryContext = context;
        }

        [HttpGet]
        [Route("Date")]
        public string Date()
        {
            return DateTime.Now.ToString();
        }

        [HttpGet]
        [Route("AppName")]
        public string AppName()
        {
            return "PTTransfers";
        }

        [HttpGet]
        [Route("Hello")]
        public string Hello(string name)
        {
            return string.Concat($"Hello {name.ToString()} Current date:{DateTime.Now}");
        }

        [HttpGet]
        [Route("ServerName")]
        public string ServerName()
        {
            string server = this.SalaryContext.Database.SqlQuery<string>($"select @@SERVERNAME as value").Single();
            return server;
        }
    }
}
