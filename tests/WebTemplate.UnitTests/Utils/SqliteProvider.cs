using System;
using Microsoft.EntityFrameworkCore;
using WebTemplate.Infrastructure.Data;

namespace WebTemplate.UnitTests.Utils;

public class SqliteProvider : IDisposable
{
    public static readonly DbContextOptions<ApplicationDbContext> Options =
        new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

    public static ApplicationDbContext GetApplicationDbContext()
    {
        var context = new ApplicationDbContext(Options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();
        return context;
    }

    public void Dispose() { }
}
