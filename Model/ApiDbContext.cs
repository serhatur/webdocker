using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace WebDocker.Model
{
    public class ApiDbContext : DbContext,IApiDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Gadget> Gadgets { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Gadget>().HasKey(m => m.Id);

            builder.Entity<Gadget>().HasData
    (
    new Gadget { Id = 1,Name = "MSDN Order" },
    new Gadget { Id = 2,Name = "Docker Order" },
    new Gadget { Id = 3,Name = "EFCore Order" }
    );


            base.OnModelCreating(builder);
        }

        public static void EnsureCreated(IServiceProvider provider)
        {
            var context = provider.GetService<ApiDbContext>();
            context.Database.Migrate();
        }
    }

    public interface IApiDbContext
    {
        DbSet<Gadget> Gadgets { get; set; }
    }

    public static class DbInitializer
    {
        public static void Initialize(ApiDbContext ctx)
        {
            ctx.Database.EnsureCreated();
            if (!ctx.Gadgets.Any())
            {
                ctx.Gadgets.Add(new Gadget { Name = "plumbus" });
                ctx.Gadgets.Add(new Gadget { Name = "flux capacitor" });
                ctx.Gadgets.Add(new Gadget { Name = "spline reticulator" });
                ctx.SaveChanges();
            } else {
                ctx.Gadgets.Add(new Gadget { Name = DateTime.Now.ToString()});
                ctx.SaveChanges();
            }
        }
    }
}
