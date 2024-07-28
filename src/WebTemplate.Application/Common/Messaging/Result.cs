using System.Runtime.CompilerServices;

namespace WebTemplate.Application.Common.Messaging;

public enum ResultStatus
{
    Success,
    Error,
    Unavailable,
}

public class Result<T>
{
    public T? Value { get; init; }

    public ResultStatus ResultStatus { get; private set; }

    public IEnumerable<string> Errors { get; private set; } = Array.Empty<string>();

    public Result()
    {
    }

    public Result(T? value)
    {
        Value = value;
    }

    public Result(T? value, ResultStatus resultStatus)
    {
        Value = value;
        ResultStatus = resultStatus;
    }

    public Result(T? value, ResultStatus resultStatus, IEnumerable<string> errors)
    {
        Value = value;
        ResultStatus = resultStatus;
        Errors = errors;
    }

    public bool IsSuccess(out T value)
    {
        if (Value is not null)
        {
            value = Value;
        }
        else
        {
            ResultStatus = ResultStatus.Unavailable;
            value = default!;
        }

        return ResultStatus == ResultStatus.Success;
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
