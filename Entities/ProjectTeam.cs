using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OutOfOffice.Entities;

[Table("project_teams")]
[PrimaryKey("EmployeeId", "ProjectId")]
public class ProjectTeam
{
    [Column("employee_id")]
    public Employee Employee { get; set; } = null!;
    
    [Column("project_id"), ]
    public Project Project { get; set; } = null!;
    
    [ForeignKey("Project")]
    public int ProjectId { get; set; }
    
    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
    
}