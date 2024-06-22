

namespace OutOfOffice.Exceptions;

public class BadCredentialsException : Exception
{
    
    public int StatusCode { get; set; }
    public BadCredentialsException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public BadCredentialsException()
    {
    }
    
}