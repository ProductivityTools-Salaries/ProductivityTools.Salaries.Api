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

        public DbSet<Salary> Salaries { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = configuration["ConnectionString"];
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Salary>().ToTable("Salary","s").HasKey("SalaryId");
          
        }
    }
}
