using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductivityTools.Salaries.Api.Views
{
    public class SalaryFilter
    {
        public string Position { get; set; }
        public string Comment { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public bool? B2b { get; set; }

        public string OrderBy { get; set; }
        public string OrderByDescending { get; set; }
    }
}
