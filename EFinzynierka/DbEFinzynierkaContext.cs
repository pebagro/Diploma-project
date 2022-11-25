using Microsoft.EntityFrameworkCore;
using EFinzynierka.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EFinzynierka
{
    public class DbEFinzynierkaContext : IdentityDbContext<UserModel>
    {
        // konstruktor
        public DbEFinzynierkaContext(DbContextOptions<DbEFinzynierkaContext> options) : base(options) { }


        // Tworzymy tabele po modelu Employee - automat
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<CompanyModel> companyModel { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
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
