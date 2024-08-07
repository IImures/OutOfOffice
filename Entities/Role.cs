﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOffice.Entities;

[Table("roles")]
public class Role
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    [Column("role_name")]
    public string RoleName { get; set; } = null!;
    
    ICollection<EmployeeRole> Employees { get;} = new List<EmployeeRole>();
    
}