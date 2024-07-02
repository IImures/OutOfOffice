using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOffice.Entities;

[Table("leave_requests")]
public class LeaveRequest
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [Column("absence_reason")]
    [MaxLength(255)]
    public string AbsenceReason { get; set; } = null!;
    
    [Required]
    [Column("start_date")]
    public DateTime StartDate { get; set; }
    
    [Required]
    [Column("end_date")]
    public DateTime EndDate { get; set; }
    
    [Column("comment", TypeName= "text")]
    public string? Comment { get; set; }
    
    [Required]
    [Column("leave_request_status_id")]
    public LeaveRequestStatus Status { get; set; } = null!;
    
    [ForeignKey(nameof(Status))]
    public int LeaveRequestStatusId { get; set; }
    
    [Required]
    [Column("employee_id")]
    public Employee Employee { get; set; } = null!;
    
    [ForeignKey(nameof(Employee))]
    public int EmployeeId { get; set; }
}