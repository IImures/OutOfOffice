namespace OutOfOffice.DTO.Responses;

public class LoginResponse
{
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}