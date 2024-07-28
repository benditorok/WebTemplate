namespace WebTemplate.Core.Common.Messaging;

/// <summary>
/// Command base marker interface.
/// </summary>
public interface IBaseQuery;

/// <summary>
/// Query marker interface with a response.
/// </summary>
public interface IQuery<TResponse> : IBaseQuery;
