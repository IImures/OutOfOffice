using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.DTO.Requests;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = null!;
}