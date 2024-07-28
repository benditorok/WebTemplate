using Microsoft.EntityFrameworkCore;
using WebTemplate.Core.Common.Messaging;
using WebTemplate.Core.Domain.Entities;
using WebTemplate.Infrastructure.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebTemplate.UseCases.Products.Queries;

public sealed record GetProductsByNameQuery(string Name) : IQuery<IEnumerable<Product>>;

public sealed class GetProductsByNameQueryHandler : IQueryHandler<GetProductsByNameQuery, IEnumerable<Product>>
{
    private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;

    public GetProductsByNameQueryHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<Result<IEnumerable<Product>>> HandleAsync(GetProductsByNameQuery query, CancellationToken cancellationToken)
    {
        using (var ctx = dbContextFactory.CreateDbContext())
        {
            var filter = query.Name.ToUpper();

            var products = await ctx.Set<Product>()
                .Where(x => x.Name != null && x.Name.ToUpper().Contains(filter))
                .ToListAsync(cancellationToken);

            if (products is null)
                return Result<IEnumerable<Product>>.Error($"Products with name {query.Name} not found!");

            return Result<IEnumerable<Product>>.Success(products);
        }
    }
}