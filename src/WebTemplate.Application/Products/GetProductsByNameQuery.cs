using WebTemplate.Application.Common.Messaging;
using WebTemplate.Domain.Entities;

namespace WebTemplate.Application.Products;

public sealed record GetProductsByNameQuery(string Name) : IQuery<IEnumerable<Product>>;

public sealed class GetProductsByNameQueryHandler : IQueryHandler<GetProductsByNameQuery, IEnumerable<Product>>
{
    private readonly IProductRepository productsRepository;

    public GetProductsByNameQueryHandler(IProductRepository productsRepository)
    {
        this.productsRepository = productsRepository;
    }

    public async Task<Result<IEnumerable<Product>>> Handle(GetProductsByNameQuery query, CancellationToken cancellationToken)
    {
        var products = await productsRepository.GetByNameAsync(query.Name, cancellationToken);

        if (products is null)
            return Result<IEnumerable<Product>>.Error($"Products with name {query.Name} not found!");

        return Result<IEnumerable<Product>>.Success(products);
    }
}