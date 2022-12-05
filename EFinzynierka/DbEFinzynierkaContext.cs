using Microsoft.EntityFrameworkCore;
using EFinzynierka.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Linq;
using System.Reflection.Metadata;

namespace EFinzynierka
{
    public class DbEFinzynierkaContext : IdentityDbContext<UserModel>
    { 
        // konstruktor
        public DbEFinzynierkaContext(DbContextOptions<DbEFinzynierkaContext> options) : base(options) { }

        // Tworzymy tabele po modelu Employee - automat
        public DbSet<EmployeeModel> Employees { get; set; }
        //public DbSet<CompanyModel> CompanyModels { get; set; }
        public DbSet<SchedulerModel> Scheduler { get; set; }
        public DbSet<MonthlyModel> MonthlyModel { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
          //  builder.Entity<CompanyModel>().ToTable("Company");
            builder.Entity<MonthlyModel>().ToTable("MonthlyModel");
         /*   builder.Entity<MonthlyModel>().HasKey(b => b.TypeId);
            builder.Entity<MonthlyModel>().HasAlternateKey(b => b.IdEmployee);
            builder.Entity<MonthlyModel>().HasAlternateKey(b => b.IdScheduler);
            builder.Entity<MonthlyModel>().HasOne(p => p.EmployeeModel).WithMany(b => b.SchedulerModels);
            */
            builder.Entity<EmployeeModel>().ToTable("Employee");
            /*builder.Entity<EmployeeModel>().HasKey(b => b.IdEmployee).HasName("PrimaryKey_IdEmployee");
            builder.Entity<EmployeeModel>().HasOne(p => p.SchedulerModels).HasMany(p => p.MonthlyModel).HasForeignKey(e => e.Id);
*/
            builder.Entity<SchedulerModel>().ToTable("Scheduler");
            /*builder.Entity<SchedulerModel>().HasKey(b => b.IdScheduler);
            builder.Entity<SchedulerModel>().HasAlternateKey(b => b.IdEmployee);
            builder.Entity<SchedulerModel>().HasAlternateKey(b =>b.TypeId);
            builder.Entity<SchedulerModel>().HasOne(p => p.EmployeeModel).HasMany(b => b.MonthlyModel);*/

        }
    }
}
