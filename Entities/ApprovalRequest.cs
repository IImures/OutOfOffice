using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOffice.Entities;

[Table("approval_requests")]
public class ApprovalRequest
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [Column("comment", TypeName = "text")]
    public string Comment { get; set; } = null!;
    
    [Required]
    [Column("approval_status_id")]
    public ApprovalStatus ApprovalStatus { get; set; } = null!;
    
    [ForeignKey(nameof(ApprovalStatus))]
    public int ApprovalStatusId { get; set; }
    
    [Required]
    [Column("leave_request_id")]
    public LeaveRequest LeaveRequest { get; set; } = null!;
    
    [ForeignKey(nameof(LeaveRequest))]
    public int LeaveRequestId { get; set; }
    
    [Required]
    [Column("employee_id")]
    public Employee Employee { get; set; } = null!;
    
    [ForeignKey(nameof(Employee))]
    public int EmployeeId { get; set; }
    
}