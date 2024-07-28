using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebTemplate.Domain.Entities;

namespace WebTemplate.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    protected internal virtual DbSet<Product> Products { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
