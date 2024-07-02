using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.DTO.Requests;

public class AddProjectRequest
{
    [Required]
    public int ProjectTypeId { get; set; }
    [Required] //is it needed?
    public int ProjectStatusId { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    [Required]
    public int ProjectManagerId { get; set; }
    
    public string? Comment { get; set; }
    [MinLength(1)]
    public int[] EmployeeIds { get; set; } = null!;
}