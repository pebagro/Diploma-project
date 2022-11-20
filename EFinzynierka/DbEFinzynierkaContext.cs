using Microsoft.EntityFrameworkCore;
using EFinzynierka.Models;
namespace EFinzynierka
{
    public class DbEFinzynierkaContext : DbContext
    {
        // konstruktor
        public DbEFinzynierkaContext(DbContextOptions<DbEFinzynierkaContext> options) : base(options) { }


        // Tworzymy tabele po modelu Employee - automat
        public DbSet<EmployeeModel> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
