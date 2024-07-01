using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.DTO.Requests;

public class RegisterRequest
{
    [Required] 
    [MaxLength(100)] 
    public string FullName { get; set; } = null!;
    [Required] 
    public short OufOfOfficeBalance { get; set; }
    [Required] 
    public int SubdivisionId { get; set; }
    [Required] 
    public int PositionId { get; set; }
    [Required] 
    public int StatusId { get; set; }
     
    public int? PeoplePartnerId { get; set; }
    [Required] 
    public int[] RolesId { get; set; } = null!;
    [Required] 
    [MaxLength(50)] 
    public string Password { get; set; } = null!;
}