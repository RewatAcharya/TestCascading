using Microsoft.EntityFrameworkCore;
using TestCascading.Models;

namespace TestCascading.Data
{
    public class ApplicationDbContext : DbContext
    {
        // public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-2TPLGS3\\SQLEXPRESS;Database=SoftechTest;Trusted_Connection=True;TrustServerCertificate=True");
        }

        public DbSet<State> States { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Cascading> Cascadings { get; set; }
    }
}
