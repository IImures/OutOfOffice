namespace OutOfOffice.Exceptions;

public class UserExitsException : Exception
{
    public int StatusCode { get; set; }
    
    public UserExitsException(string message) : base(message)
    {
    }
    
    public UserExitsException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
    
}