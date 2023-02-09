using Microsoft.EntityFrameworkCore;
using EFinzynierka.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Linq;
using System;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFinzynierka
{
    public class DbEFinzynierkaContext : IdentityDbContext<UserModel>
    {
        // konstruktor
        public DbEFinzynierkaContext(DbContextOptions<DbEFinzynierkaContext> options) : base(options) { }

        // Tworzymy tabele po modelu Employee - automat
        public DbSet<EmployeeModel> Employees { get; set; }

        public DbSet<Shift> Shifts { get; set; }
        public DbSet<RFIDLog> RFIDLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<EmployeeModel>().ToTable("Employee");

            builder.Entity<Shift>().ToTable("Shift");

            builder.Entity<RFIDLog>().ToTable("RFIDLog");

            builder.Entity<Shift>()
              .HasOne(s => s.Employee)
              .WithMany(e => e.Shifts)
              .HasForeignKey(s => s.EmployeeId);

        }
    }


}
