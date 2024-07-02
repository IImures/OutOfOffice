using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOffice.Entities;

[Table("leave_request_statuses")]
public class LeaveRequestStatus
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    [Column("status")]
    public string Status { get; set; } = null!;
}

public static class LeaveStatusType
{
    public static readonly string Created = "Created";
    public static readonly string Approved = "Approved";
    public static readonly string Canceled = "Canceled";
    public static readonly string Submitted = "Submitted";
}