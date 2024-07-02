namespace OutOfOffice.DTO.Responses;

public class ProjectResponse
{
    public int Id { get; set; }
    public string ProjectType { get; set; } = null!;
    public string ProjectStatus { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string ProjectManager { get; set; } = null!;
    public string? Comment { get; set; } = null!;
    public ICollection<string> Employees { get; set; } = null!;
}