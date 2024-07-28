namespace WebTemplate.Core.Domain.Exceptions;

public class InvalidProductException(string? message, Exception? innerException) : Exception(message, innerException);
