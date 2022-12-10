using Microsoft.EntityFrameworkCore;
using EFinzynierka.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Linq;
using System;
using System.Reflection.Metadata;

namespace EFinzynierka
{
    public class DbEFinzynierkaContext : IdentityDbContext<UserModel>
    { 
        // konstruktor
        public DbEFinzynierkaContext(DbContextOptions<DbEFinzynierkaContext> options) : base(options) { }

        // Tworzymy tabele po modelu Employee - automat
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<SchedulerModel> Scheduler { get; set; }
        public DbSet<MonthlyModel> MonthlyModel { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MonthlyModel>().ToTable("MonthlyModel");
         
            builder.Entity<EmployeeModel>().ToTable("Employee");
           
            builder.Entity<SchedulerModel>().ToTable("Scheduler");
            
        }
    }
}
