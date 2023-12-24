using Microsoft.EntityFrameworkCore;
using MyBizApi.Configurations;
using MyBizApi.Entities;

namespace MyBizApi.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Profession> Professions { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfessionConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
