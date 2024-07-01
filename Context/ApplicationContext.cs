using Microsoft.EntityFrameworkCore;
using OutOfOffice.Entities;

namespace OutOfOffice.Context;

public class ApplicationContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Subdivision> Subdivisions { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<EmployeeRole> EmployeeRoles { get; set; }
    //public DbSet<Authority> Authorities { get; set; }
    public DbSet<EmployeeStatus> EmployeeStatuses { get; set; }
    
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTeam> ProjectTeams { get; set; }
    public DbSet<ProjectStatus> ProjectStatuses { get; set; }
    public DbSet<ProjectType> ProjectTypes { get; set; }

    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<LeaveRequestStatus> LeaveRequestStatuses { get; set; }
    public DbSet<ApprovalRequest> ApprovalRequests { get; set; }
    public DbSet<ApprovalStatus> ApprovalStatuses { get; set; }
    
    public ApplicationContext()
    {
    }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
    
}