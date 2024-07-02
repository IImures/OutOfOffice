namespace OutOfOffice.DTO.Requests;

public class UpdateLeaveRequestRequest
{
    public string? Reason { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Comment { get; set; }
    public int? StatusId { get; set; }
}