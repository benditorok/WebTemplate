using WebTemplate.Core.Domain.Entities;

namespace WebTemplate.Core.Products;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetByNameAsync(string name, CancellationToken cancellationToken);

    Task CreateAsync(Product product, CancellationToken cancellationToken);
}