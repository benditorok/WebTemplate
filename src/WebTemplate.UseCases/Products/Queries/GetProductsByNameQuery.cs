using Microsoft.EntityFrameworkCore;
using WebTemplate.Core.Common.Messaging;
using WebTemplate.Core.Domain.Entities;
using WebTemplate.Infrastructure.Data;

namespace WebTemplate.UseCases.Products.Queries;

public sealed record GetProductsByNameQuery(string? Name) : IQuery<IEnumerable<Product>>;

public sealed class GetProductsByNameQueryHandler : IQueryHandler<GetProductsByNameQuery, IEnumerable<Product>>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public GetProductsByNameQueryHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<Result<IEnumerable<Product>>> HandleAsync(GetProductsByNameQuery query, CancellationToken cancellationToken)
    {
        await using (var ctx = await _dbContextFactory.CreateDbContextAsync(cancellationToken))
        {
            var products = await ctx.Set<Product>()
                .Where(x => string.IsNullOrWhiteSpace(query.Name) || x.Name == query.Name)
                .ToListAsync(cancellationToken);

            if (products is null || !products.Any())
                return Result<IEnumerable<Product>>.Error($"Products with name {query.Name} not found!");

            return Result<IEnumerable<Product>>.Success(products);
        }
    }
}
