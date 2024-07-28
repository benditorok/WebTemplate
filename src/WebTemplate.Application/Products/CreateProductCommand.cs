using WebTemplate.Application.Common.Data;
using WebTemplate.Application.Common.Messaging;
using WebTemplate.Domain.Entities;

namespace WebTemplate.Application.Products;

public sealed record CreateProductCommand(Product Product) : ICommand<Product>;

public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository repository;
    private readonly IUnitOfWork unitOfWork;

    public CreateProductCommandHandler(IProductRepository repository, IUnitOfWork unitOfWork)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
    }

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