using Microsoft.EntityFrameworkCore;
using WebTemplate.Core.Domain.Entities;
using WebTemplate.Infrastructure.Data;
using WebTemplate.UnitTests.Utils;
using WebTemplate.UseCases.Products.Queries;

namespace WebTemplate.UnitTests.UseCases.Products.Queries;

public class ProductQueriesTests
{
    private readonly TestDatabaseProvider<ApplicationDbContext> _databaseProvider;

    public ProductQueriesTests()
    {
        _databaseProvider = new TestDatabaseProvider<ApplicationDbContext>();
        _databaseProvider.SetSeedFunction(context =>
        {
            context.Products.AddRange(
                new Product { Name = "Test Product 1", Price = 10.99m },
                new Product { Name = "Test Product 2", Price = 20.99m },
                new Product { Name = "Other Product", Price = 15.99m }
            );
        });
    }

    [Fact]
    public async Task GetProductsByName_WithExactName_ReturnsMatchingProducts()
    {
        // Arrange
        var handler = new GetProductsByNameQueryHandler(_databaseProvider.GetDbFactory());
        var query = new GetProductsByNameQuery("Test Product 1");

        // Act
        var result = await handler.HandleAsync(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess(out var resultValue));
        var products = resultValue.ToList();
        Assert.Single(products);
        Assert.Equal("Test Product 1", products[0].Name);
    }

    [Fact]
    public async Task GetProductsByName_WithNullName_ReturnsAllProducts()
    {
        // Arrange
        var handler = new GetProductsByNameQueryHandler(_databaseProvider.GetDbFactory());
        var query = new GetProductsByNameQuery(null);

        // Act
        var result = await handler.HandleAsync(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess(out var resultValue));
        Assert.Equal(3, resultValue.Count());
    }

    [Fact]
    public async Task GetProductsByName_WithNonExistentName_ReturnsError()
    {
        // Arrange
        var handler = new GetProductsByNameQueryHandler(_databaseProvider.GetDbFactory());
        var query = new GetProductsByNameQuery("Non Existent Product");

        // Act
        var result = await handler.HandleAsync(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess(out var resultValue));
        //Assert.Contains("not found", result.Errors);
    }
}
