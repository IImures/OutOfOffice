namespace OutOfOffice.DTO.Responses;

public class ApprovalRequestResponse
{
    public int Id { get; set; }
    public int LeaveRequestId { get; set; }
    public string Comment { get; set; }
    public string Status { get; set; }
    public EmployeeResponse Employee { get; set; }
}