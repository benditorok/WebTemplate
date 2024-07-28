namespace WebTemplate.Domain.Exceptions;

public class InvalidProductException : Exception
{
    public InvalidProductException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
