namespace WebTemplate.Core.Domain.Exceptions;

public class InvalidProductException : Exception
{
    public InvalidProductException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
