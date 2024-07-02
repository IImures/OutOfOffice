using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.DTO.Requests;

public class UpdateProjectRequest
{

    public int? ProjectTypeId { get; set; }
    public int? ProjectStatusId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? ProjectManagerId { get; set; }
    public string? Comment { get; set; }
    [MinLength(1)]
    public int[]? EmployeeIds { get; set; } = null!;
}