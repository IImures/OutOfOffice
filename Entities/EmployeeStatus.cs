using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOffice.Entities;

[Table("employee_statuses")]
public class EmployeeStatus
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("status")]
    public string Status { get; set; } = null!;
    
}

public static class EmployeeStatusType {
    public static readonly string Active = "Active";
    public static readonly string Inactive = "Inactive";
    public static readonly string OnVacation = "On Vacation";
}
