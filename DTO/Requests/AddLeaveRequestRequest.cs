using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.DTO.Requests;

public class AddLeaveRequestRequest
{
    [Required]
    public string Reason { get; set; } = null!;
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public int EmployeeId { get; set; }
    public string? Comment { get; set; }
}