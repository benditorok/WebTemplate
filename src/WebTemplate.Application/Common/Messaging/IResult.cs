namespace WebTemplate.Application.Common.Messaging;

public interface IResult
{
    ResultStatus ResultStatus { get; }

    IEnumerable<string> Errors { get; }

    Type ValueType { get; }

    bool IsSuccess { get; }

    object GetValue();
}