using System;
using Microsoft.EntityFrameworkCore;
using WebTemplate.Infrastructure.Data;

namespace WebTemplate.UnitTests.Utils;

public class TestDatabaseProvider<TContext> : IDisposable
    where TContext : DbContext
{
    private static bool _sqliteInitialized;
    private static readonly object _initLock = new();
    private static readonly DbContextOptions<ApplicationDbContext> _options =
        new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;
    private TContext _context;
    private Action<TContext>? _seedFunction;

    public TestDatabaseProvider()
    {
        InitializeSqlite();

        _context = (TContext)(
            Activator.CreateInstance(typeof(TContext), _options)
            ?? throw new InvalidOperationException(
                $"Could not create instance of {typeof(TContext).Name}"
            )
        );
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();
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

    public void SetSeedFunction(Action<TContext> seed)
    {
        if (_seedFunction != null)
            throw new InvalidOperationException("Seed function already set");

        _seedFunction = seed;
    }

    public TContext GetContext()
    {
        return _context;
    }

    public IDbContextFactory<TContext> GetFactory()
    {
        return new TestDbContextFactory<TContext>(this);
    }

    public void ResetDatabase()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

public class TestDbContextFactory<TContext> : IDbContextFactory<TContext>
    where TContext : DbContext
{
    private readonly TestDatabaseProvider<TContext> _database;

    public TestDbContextFactory(TestDatabaseProvider<TContext> database)
    {
        _database = database;
    }

    public TContext CreateDbContext()
    {
        return _database.GetContext();
    }

    public TContext CreateDbContext(string[] args)
    {
        return CreateDbContext();
    }
}
