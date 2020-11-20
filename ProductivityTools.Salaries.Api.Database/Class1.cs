using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductivityTools.Sallaries.Api.Contract;
using System;

namespace ProductivityTools.Salaries.Api.Database
{
    public class SalaryContext : DbContext
    {
        private readonly IConfiguration configuration;

        public SalaryContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<Sallary> Sallaries { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
        }
    }
}
