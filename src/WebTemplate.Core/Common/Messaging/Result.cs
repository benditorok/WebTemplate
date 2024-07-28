namespace WebTemplate.Core.Common.Messaging;

/// <summary>
/// Available result statuses.
/// </summary>
public enum ResultStatus
{
	Success,
	Error,
	Unavailable,
}

/// <summary>
/// Result object for CQRS response validation.
/// </summary>
/// <typeparam name="T">Type of the object when the response was Success.</typeparam>
public class Result<T>
{
	private T Value { get; init; } = default!;

	public ResultStatus ResultStatus { get; init; } = ResultStatus.Unavailable;

	public IEnumerable<string> Errors { get; private set; } = [];

	public IEnumerable<string> Informations { get; private set; } = new List<string>();

	private Result()
	{
	}

	private Result(T value)
	{
		Value = value;
	}

	private Result(T value, ResultStatus resultStatus)
	{
		Value = value;
		ResultStatus = resultStatus;
	}

	private Result(T value, ResultStatus resultStatus, IEnumerable<string> errors)
	{
		Value = value;
		ResultStatus = resultStatus;
		Errors = errors;
	}

	private Result(ResultStatus resultStatus, IEnumerable<string> errors)
	{
		ResultStatus = resultStatus;
		Errors = errors;
	}

	/// <summary>
	/// Get the value from the result.
	/// </summary>
	/// <param name="value">Object from the response.</param>
	/// <returns>The result if the response status of the command or query was success.</returns>
	public bool IsSuccess(out T value)
	{
		value = Value;
		return ResultStatus == ResultStatus.Success;
	}

	/// <summary>
	/// Add additional informations (like warning messages) to the Result.
	/// </summary>
	/// <param name="informations">Additional informations.</param>
	public void AddAdditionalInformation(params string[] informations)
	{
        foreach (var information in informations)
        {
            Informations.Append(information);
        }
    }

	/// <summary>
	/// Create a successful result.
	/// </summary>
	/// <param name="value">Response object.</param>
	/// <returns>New Result with a Success status.</returns>
	public static Result<T> Success(T value)
	{
		return new Result<T>(value, ResultStatus.Success);
	}

	/// <summary>
	/// Create a failed result.
	/// </summary>
	/// <param name="messages">Error messages.</param>
	/// <returns>New Result with an Error status.</returns>
	public static Result<T> Error(params string[] messages)
	{
		return new Result<T>(ResultStatus.Error, messages);
	}
}
