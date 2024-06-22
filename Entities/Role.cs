using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOffice.Entities;

[Table("roles")]
public class Role
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string RoleName { get; set; } = null!;
    
}