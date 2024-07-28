using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebTemplate.Core.Domain.Entities;

namespace WebTemplate.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    protected internal virtual DbSet<Product> Products { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
