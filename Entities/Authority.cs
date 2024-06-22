using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.Entities;

public class Authority
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string AuthorityName { get; set; } = null!;
}