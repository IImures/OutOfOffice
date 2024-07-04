using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOffice.Entities;

[Table("project_statuses")]
public class ProjectStatus
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    [Column("status")]
    public string Status { get; set; } = null!;
}

public static class ProjectStatusType {
    public static readonly string New = "New";
    public static readonly string Active = "Active";
    public static readonly string Inactive = "Inactive";
    public static readonly string Canceled = "Canceled";
}