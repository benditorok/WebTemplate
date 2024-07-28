namespace WebTemplate.Core.Common.Messaging;

/// <summary>
/// Command base marker interface.
/// </summary>
public interface IBaseCommand;

/// <summary>
/// Command marker interface without a response.
/// </summary>
public interface ICommand : IBaseCommand;

/// <summary>
/// Command marker interface with a response.
/// </summary>
public interface ICommand<TResponse> : IBaseCommand;
