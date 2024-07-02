using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOffice.Entities;

[Table("project_types")]
public class ProjectType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    [Column("type")]
    public string Type { get; set; } = null!;
}

public static class ProjectTypeEnum {
    public static readonly string Active = "Active";
    public static readonly string Inactive = "Inactive";
    public static readonly string Canceled = "Canceled";
}
