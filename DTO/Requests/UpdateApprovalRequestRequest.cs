namespace OutOfOffice.DTO.Requests;

public class UpdateApprovalRequestRequest
{
    public string? Comment { get; set; }
    public int? StatusId { get; set; }
    public int? EmployeeId { get; set; }
    public int? LeaveRequestId { get; set; }
}