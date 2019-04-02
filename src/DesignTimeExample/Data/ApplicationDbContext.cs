using System.Reflection;
using DesignTimeExample.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignTimeExample.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Flight> Flights { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    public class ApplicationContextDesignFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>
    {
        public ApplicationContextDesignFactory() : base("DefaultConnection", typeof(Startup).GetTypeInfo().Assembly.GetName().Name)
        { }
        protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
        {
            return new ApplicationDbContext(options);
        }
    }
}