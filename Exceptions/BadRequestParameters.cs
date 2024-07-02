namespace OutOfOffice.Exceptions;

public class BadRequestParameters : Exception
{
    public int StatusCode { get; set; }
    
    public BadRequestParameters(string message) : base(message)
    {
    }
    
    public BadRequestParameters(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}