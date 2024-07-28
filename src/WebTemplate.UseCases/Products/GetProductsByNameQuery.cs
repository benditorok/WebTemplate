using WebTemplate.Core.Common.Messaging;
using WebTemplate.Core.Domain.Entities;

namespace WebTemplate.UseCases.Products;

public sealed record GetProductsByNameQuery(string Name) : IQuery<IEnumerable<Product>>;

public sealed class GetProductsByNameQueryHandler : IQueryHandler<GetProductsByNameQuery, IEnumerable<Product>>
{
    public async Task<Result<IEnumerable<Product>>> HandleAsync(GetProductsByNameQuery query, CancellationToken cancellationToken)
    {
        var products = await productsRepository.GetByNameAsync(query.Name, cancellationToken);

        if (products is null)
            return Result<IEnumerable<Product>>.Error($"Products with name {query.Name} not found!");

        return Result<IEnumerable<Product>>.Success(products);
    }
}