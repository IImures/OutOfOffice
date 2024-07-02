using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.DTO.Requests;

public class AddApprovalRequestRequest
{
    [Required]
    public string Comment { get; set; }
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public int StatusId { get; set; }
    [Required]
    public int LeaveRequestId { get; set; }
}