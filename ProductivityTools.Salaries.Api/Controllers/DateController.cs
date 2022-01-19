using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ProductivityTools.Salaries.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DateController : ControllerBase
    {
        [HttpPost("GetDate")]
        public string GetDate()
        {
            return DateTime.Now.ToString(); 
        }
    }
}
