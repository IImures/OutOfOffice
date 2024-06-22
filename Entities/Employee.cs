﻿using System.ComponentModel.DataAnnotations;
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
    
    //Todo: Add Photo property
    
    [Required]
    [Column("Subdivision_id")]
    public Subdivision Subdivision { get; set; } = null!;
    
    [ForeignKey(nameof(Subdivision))]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public int SubdivisionId { get; set; }
    
    [Required]
    [Column("Position_id")]
    public Position Position { get; set; } = null!;
    
    [ForeignKey(nameof(Position))]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public int PositionId { get; set; }
    
    [Required]
    [Column("employee_status_id")]
    public EmployeeStatus Status { get; set; } = null!;
    
    [ForeignKey(nameof(Status))]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public int StatusId { get; set; }
    
    [Column("people_partner_id")]
    public Employee PeoplePartner { get; set; } = null!;
    
    [ForeignKey(nameof(PeoplePartner))]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public int PeoplePartnerId { get; set; }
    
    public ICollection<Role> Roles { get; set; } = null!;
    
    public ICollection<Authority> Authorities { get; set; } = null!;
    
}