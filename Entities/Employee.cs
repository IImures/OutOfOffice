using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OutOfOffice.Entities;

[Table("employees")]
public class Employee
{
    
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    [Column("full_name")]
    public string FullName { get; set; } = null!;
    
    [Required]
    [Column("out_of_office_balance")]
    public short OutOfOfficeBalance { get; set; }
    
    [MaxLength(200)]
    [Column("password")]
    public string Password { get; set; } = null!;
    
    //Todo: Add Photo property
    
    [Required]
    [Column("Subdivision_id")]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Subdivision Subdivision { get; set; } = null!;
    
    [ForeignKey(nameof(Subdivision))]
    
    public int SubdivisionId { get; set; }
    
    [Required]
    [Column("Position_id")]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Position Position { get; set; } = null!;
    
    [ForeignKey(nameof(Position))]
    public int PositionId { get; set; }
    
    [Required]
    [Column("employee_status_id")]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public EmployeeStatus Status { get; set; } = null!;
    
    [ForeignKey(nameof(Status))]
    public int StatusId { get; set; }
    
    [Column("people_partner_id")]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Employee? PeoplePartner { get; set; } = null!;
    
    [ForeignKey(nameof(PeoplePartner))]
    public int? PeoplePartnerId { get; set; }
    
    public ICollection<EmployeeRole> Roles { get;} = new List<EmployeeRole>();
    
    //public ICollection<Authority> Authorities { get; set; } = null!;
    
    public ICollection<ApprovalRequest> ApprovalRequests { get; set; } = null!;
    
    public ICollection<ProjectTeam> Projects { get; set; } = null!;
    
}