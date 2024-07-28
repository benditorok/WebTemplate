namespace WebTemplate.Core.Common.Messaging;

/// <summary>
/// Query handler base marker interface.
/// </summary>
public interface IBaseQueryHandler;

/// <summary>
/// Query handler interface with a response.
/// </summary>
public interface IQueryHandler<in TQuery, TResponse> : IBaseQueryHandler
	where TQuery : IQuery<TResponse>
{
	Task<Result<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken);
}
