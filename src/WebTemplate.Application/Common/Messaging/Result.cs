using System.Runtime.CompilerServices;

namespace WebTemplate.Application.Common.Messaging;

public enum ResultStatus
{
    Success,
    Error,
}

public class Result<T>
{
    public T? Value { get; init; }

    public Type ValueType => typeof(T);

    public ResultStatus ResultStatus { get; private set; }

    public IEnumerable<string> Errors { get; private set; } = Array.Empty<string>();

    public bool IsSuccess => ResultStatus == ResultStatus.Success;

    public Result()
    {
    }

    public Result(T? value)
    {
        Value = value;
    }

    public Result(T? value, ResultStatus resultStatus)
    {
        Value=value;
        ResultStatus=resultStatus;
    }

    public Result(T? value, ResultStatus resultStatus, IEnumerable<string> errors)
    {
        Value=value;
        ResultStatus=resultStatus;
        Errors=errors;
    }

    public T GetValue()
    {
        if (Value is not null && IsSuccess)
            return Value;

        throw new InvalidOperationException("The result was not successful!");
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value, ResultStatus.Success);
    }

    public static Result<T> Error(params string[] messages)
    {
        return new Result<T>(default, ResultStatus.Error, messages);
    }
}
