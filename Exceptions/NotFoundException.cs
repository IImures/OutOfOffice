namespace OutOfOffice.Exceptions;

public class NotFoundException : Exception
{
    
    public int StatusCode { get; set; }
    public NotFoundException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public NotFoundException()
    {
    }
}