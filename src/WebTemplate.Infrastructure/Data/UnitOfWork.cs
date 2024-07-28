using WebTemplate.Application.Common.Data;

namespace WebTemplate.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext ctx;

    public UnitOfWork(ApplicationDbContext ctx)
    {
        this.ctx=ctx;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await ctx.SaveChangesAsync(cancellationToken);
    }
}
