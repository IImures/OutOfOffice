using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOffice.Entities;

[Table("approval_statuses")]
public class ApprovalStatus
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    [Column("status")]
    public string Status { get; set; } = null!;
}

public static class ApprovalStatusType
{
    public static readonly string Pending = "Pending";
    public static readonly string Approved = "Approved";
    public static readonly string Rejected = "Rejected";
    public static readonly string Deleted = "Deleted";
}