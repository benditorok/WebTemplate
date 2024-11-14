using System;
using Microsoft.EntityFrameworkCore;
using WebTemplate.Infrastructure.Data;

namespace WebTemplate.UnitTests.Utils;

public class TestDatabaseProvider<TContext>
    where TContext : DbContext
{
    private static bool _sqliteInitialized;
    private static readonly object _initLock = new();
    private static readonly DbContextOptions<ApplicationDbContext> _options =
        new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;
    private Action<TContext>? _seedFunction;

    public TestDatabaseProvider()
    {
        InitializeSqlite();
    }

    private static void InitializeSqlite()
    {
        if (_sqliteInitialized)
            return;

        lock (_initLock)
        {
            if (_sqliteInitialized)
                return;

            SQLitePCL.Batteries_V2.Init();
            _sqliteInitialized = true;
        }
    }

    private TContext CreateContext()
    {
        var context = (TContext)(
            Activator.CreateInstance(typeof(TContext), _options)
            ?? throw new InvalidOperationException(
                $"Could not create instance of {typeof(TContext).Name}"
            )
        );
        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        if (_seedFunction != null)
        {
            _seedFunction.Invoke(context);
            context.SaveChanges();
        }

        return context;
    }

    public void SetSeedFunction(Action<TContext> seed)
    {
        if (_seedFunction != null)
            throw new InvalidOperationException("Seed function already set");

        _seedFunction = seed;
    }

    public TContext GetDbContext()
    {
        return CreateContext();
    }

    public IDbContextFactory<TContext> GetDbFactory()
    {
        return new TestDbContextFactory<TContext>(this);
    }
}

internal class TestDbContextFactory<TContext> : IDbContextFactory<TContext>
    where TContext : DbContext
{
    private readonly TestDatabaseProvider<TContext> _database;

    public TestDbContextFactory(TestDatabaseProvider<TContext> database)
    {
        _database = database;
    }

    public TContext CreateDbContext()
    {
        return _database.GetDbContext();
    }

    public TContext CreateDbContext(string[] args)
    {
        return CreateDbContext();
    }
}
