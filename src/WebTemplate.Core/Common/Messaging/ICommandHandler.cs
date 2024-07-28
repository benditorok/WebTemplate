namespace WebTemplate.Core.Common.Messaging;

/// <summary>
/// Command handler base marker interface.
/// </summary>
public interface IBaseCommandHandler;

/// <summary>
/// Command handler interface without a response.
/// </summary>
public interface ICommandHandler<in TCommand> : IBaseCommandHandler
	where TCommand : ICommand
{
	Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}

/// <summary>
/// Command handler interface with a response.
/// </summary>
public interface ICommandHandler<in TCommand, TResponse> : IBaseCommandHandler
	where TCommand : ICommand<TResponse>
{
	Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken);
}
