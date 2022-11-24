using Microsoft.EntityFrameworkCore;
using EFinzynierka.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EFinzynierka.DbDate
{
    public class DbEmployees : DbContext
    {
        public DbEmployees(DbContextOptions options) : base(options) { }

        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<CompanyModel> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeModel>().ToTable("Employees");
            modelBuilder.Entity<CompanyModel>().ToTable("Company");

            //modelBuilder.Entity<EmployeeSchedule>().HasKey(c => c.EmployeeId);
        }
    }
}