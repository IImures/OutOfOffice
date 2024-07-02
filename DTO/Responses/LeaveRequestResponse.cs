using OutOfOffice.DTO.Responses;

namespace OutOfOffice.DTO.Requests;

public class LeaveRequestResponse
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Comment { get; set; }
    public string Status { get; set; } = null!;
    public string AbsenceReason { get; set; } = null!;
    public EmployeeResponse Employee { get; set; } = null!;
}