using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OutOfOffice.Entities;

[Table("employee_roles")]
[PrimaryKey("EmployeeId", "RoleId")]
public class EmployeeRole

{
    
    [Column("fk_employee_id")]
    [ForeignKey("fk_employee")]
    public int EmployeeId { get; set; }

    public Employee Employee { get; set; } = null!;

    [Column("fk_role_id")]
    [ForeignKey("fk_role")]
    public int RoleId { get; set; }

    public Role Role { get; set; } = null!;
}