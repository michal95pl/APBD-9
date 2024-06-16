namespace WebApplication1.Exceptions;

public class InvalidTokenException : Exception 
{
    public InvalidTokenException(string message) : base(message) { }
    public InvalidTokenException() : base("") { }
}