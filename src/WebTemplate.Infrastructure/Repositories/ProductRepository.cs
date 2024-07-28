using Microsoft.EntityFrameworkCore;
using WebTemplate.Application.Products;
using WebTemplate.Domain.Entities;
using WebTemplate.Infrastructure.Data;

namespace WebTemplate.Infrastructure.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext ctx;

    public ProductRepository(ApplicationDbContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task CreateAsync(Product product, CancellationToken cancellationToken)
    {
        await ctx.Products.AddAsync(product, cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await ctx.Products
            .Where(x => x. Name != null && x.Name.Equals(name))
            .ToListAsync(cancellationToken);
    }
}
