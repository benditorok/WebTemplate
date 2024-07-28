using WebTemplate.Core.Common.Messaging;
using WebTemplate.Core.Domain.Entities;

namespace WebTemplate.UseCases.Products;

public sealed record CreateProductCommand(Product Product) : ICommand<Product>;

public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Product>
{
    public async Task<Result<Product>> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Do validation
        // ...

        await repository.CreateAsync(command.Product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var createdProductWithId = command.Product;
        return Result<Product>.Success(createdProductWithId);

        //return Result<Product>.Error($"Creation of product with id {createdProductWithId?.Id} failed!");
    }   
}