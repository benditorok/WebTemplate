using Microsoft.EntityFrameworkCore;
using WebTemplate.Core.Common.Messaging;
using WebTemplate.Core.Domain.Entities;
using WebTemplate.Infrastructure.Data;

namespace WebTemplate.UseCases.Products.Commands;

public sealed record CreateProductCommand(Product Product) : ICommand<Product>;

public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Product>
{
    private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;

    public CreateProductCommandHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<Result<Product>> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Do validation
        // Inject a proxy, etc
        // ...

        using (var ctx = dbContextFactory.CreateDbContext())
        {
            await ctx.AddAsync(command.Product, cancellationToken);
            await ctx.SaveChangesAsync(cancellationToken);

            var createdProductWithId = command.Product;
            return Result<Product>.Success(createdProductWithId);

            //return Result<Product>.Error($"Creation of product with id {createdProductWithId?.Id} failed!");
        }
    }
}
