using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDocker.Model
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Gadget> Gadgets { get; set; }
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
