using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOffice.Entities;

[Table("projects")]
public class Project
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [Column("project_status_id")]
    public ProjectStatus Status { get; set; } = null!;
    
    [ForeignKey(nameof(Status))]
    public int ProjectStatusId { get; set; }
    
    [Required]
    [Column("project_type_id")]
    public ProjectType Type { get; set; } = null!;
    
    [ForeignKey(nameof(Type))]
    public int ProjectTypeId { get; set; }
    
    [Required]
    [Column("start_date")]
    public DateTime StartDate { get; set; }
    
    [Required]
    [Column("end_date")]
    public DateTime EndDate { get; set; }
    
    [Required]
    [Column("project_manager_id")]
    public Employee ProjectManager { get; set; } = null!;
    
    [ForeignKey(nameof(ProjectManager))]
    public int ProjectManagerId { get; set; }
    
    [Required]
    [Column("comment", TypeName = "text")]
    public string Comment { get; set; } = null!;
    
    ICollection<ProjectTeam> ProjectTeams { get; set; } = null!;
    
}