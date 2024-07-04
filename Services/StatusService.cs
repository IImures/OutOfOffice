using Microsoft.EntityFrameworkCore;
using OutOfOffice.Context;

namespace OutOfOffice.Services;

public class StatusService(ApplicationContext context) : IStatusService
{
    public async Task<object> GetEmployeeStatuses()
    {
        var empStatuses = await context.EmployeeStatuses.ToListAsync();
        return empStatuses.Select(status => new
        {
            Id = status.Id,
            Status = status.Status
        }).ToList();
    }

    public async Task<object> GetApprovalStatuses()
    {
        var approvalStatus = await context.ApprovalStatuses.ToListAsync();
        return approvalStatus.Select(status => new
        {
            Id = status.Id,
            Status = status.Status
        }).ToList();
    }

    public async Task<object> GetProjectStatuses()
    {
        var projectStatuses = await context.ProjectStatuses.ToListAsync();
        return projectStatuses.Select(status => new
        {
            Id = status.Id,
            Status = status.Status
        }).ToList();
    }

    public async Task<object> GetRoles()
    {
        var roles = await context.Roles.ToListAsync();
        return roles.Select(role => new
        {
            Id = role.Id,
            Role = role.RoleName
        }).ToList();
    }
    
    public async Task<object> GetSubdivisions()
    {
        var subdivisions = await context.Subdivisions.ToListAsync();
        return subdivisions.Select(subdivision => new
        {
            Id = subdivision.Id,
            Subdivision = subdivision.Name
        }).ToList();
    }
    
    public async Task<object> GetPositions()
    {
        var positions = await context.Positions.ToListAsync();
        return positions.Select(position => new
        {
            Id = position.Id,
            Position = position.Name
        }).ToList();
    }
    
    public async Task<object> GetLeaveRequestStatuses()
    {
        var leaveRequestStatuses = await context.LeaveRequestStatuses.ToListAsync();
        return leaveRequestStatuses.Select(status => new
        {
            Id = status.Id,
            Status = status.Status
        }).ToList();
    }
    
    public async Task<object> GetProjectTypes()
    {
        var projectTypes = await context.ProjectTypes.ToListAsync();
        return projectTypes.Select(type => new
        {
            Id = type.Id,
            Type = type.Type
        }).ToList();
    }
}

public interface IStatusService
{
    Task<object> GetEmployeeStatuses();
    Task<object> GetApprovalStatuses();
    Task<object> GetProjectStatuses();
    Task<object> GetRoles();
    Task<object> GetSubdivisions();
    Task<object> GetPositions();
    Task<object> GetLeaveRequestStatuses();
    Task<object> GetProjectTypes();
}