using System;

namespace ProductivityTools.Sallaries.Api.Contract
{
    public class Salary
    {
        public int SalaryId { get; set; }
        public string Position { get; set; }
        public int Value { get; set; }
        public int Bonus { get; set; }
        public int Equity { get; set; }
        public string Comment { get; set; }
        public string Company { get; set; }
        public int? ValueConfirmed { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public bool? B2b { get; set; }
    }
}
