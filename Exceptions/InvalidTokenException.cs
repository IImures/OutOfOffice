namespace OutOfOffice.Exceptions;

public class InvalidTokenException : Exception
{
    
    public int StatusCode { get; set; }
    public InvalidTokenException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
    
}