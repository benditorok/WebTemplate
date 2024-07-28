using WebTemplate.Domain.Entities;

namespace WebTemplate.Application.Products;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetByNameAsync(string name, CancellationToken cancellationToken);

    Task CreateAsync(Product product, CancellationToken cancellationToken);
}