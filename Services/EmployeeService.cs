using Microsoft.EntityFrameworkCore;
using OutOfOffice.Context;
using OutOfOffice.DTO.Requests;
using OutOfOffice.Entities;
using OutOfOffice.Utils;
using System.Linq.Dynamic.Core;
using OutOfOffice.DTO.Responses;
using OutOfOffice.Exceptions;

namespace OutOfOffice.Services;

public class EmployeeService(ApplicationContext context, IAuthService authService) : IEmployeeService
{
    
    private static readonly string[] AllowedSortColumns = { "id", "fullName", "outOffOfficeBalance", "subdivision", "position", "status" };
    private static readonly string[] AllowedSortDirections = { "asc", "desc" };

    private readonly ApplicationContext _context = context;

    public async Task<PagedList<EmployeeResponse>> GetEmployees(PageRequest request)
    {
        
            IQueryable<Employee> query = _context.Employees
            .Include(e => e.Subdivision)
            .Include(e => e.Position)
            .Include(e => e.Status)
            .Where(e => e.Status.Status != StatusType.Inactive)
            .Include(e => e.Roles)
                .ThenInclude(er=> er.Role)
            .AsQueryable();
        if (!AllowedSortDirections.Contains(request.SortDirection) ||
            !AllowedSortColumns.Contains(request.SortBy))
        {
            throw new BadRequestParameters("Bad sorting parameters");
        }

        string sorting = $"{request.SortBy} {request.SortDirection}";
        query = query.OrderBy(sorting);
        
        var pagedEmployees = await PagedList<Employee>.ToPagedListAsync(query, request.Page, request.PageSize);
        var employeeResponses = pagedEmployees.Select(e => new EmployeeResponse
        {
            Id = e.Id,
            FullName = e.FullName,
            OutOfOfficeBalance = e.OutOfOfficeBalance,
            Subdivision = e.Subdivision.Name,
            Position = e.Position.Name,
            Status = e.Status.Status,
            Roles = e.Roles.Select(empRole => empRole.Role.RoleName).ToList(),
            PartnerId = e.PeoplePartnerId
        }).ToList();

        return new PagedList<EmployeeResponse>(employeeResponses, pagedEmployees.TotalCount, pagedEmployees.CurrentPage, pagedEmployees.PageSize);
    }

    public async Task<EmployeeResponse> GetEmployee(int id)
    {
        var emp = await GetEmployeeById(id);
        return new EmployeeResponse()
        {
            FullName = emp.FullName,
            OutOfOfficeBalance = emp.OutOfOfficeBalance,
            Subdivision = emp.Subdivision.Name,
            Position = emp.Position.Name,
            Status = emp.Status.Status,
            Roles = emp.Roles.Select(empRole => empRole.Role.RoleName).ToList(),
            PartnerId = emp.PeoplePartnerId
        };
    }

    public async Task<EmployeeResponse> AddEmployee(RegisterRequest request)
    {
        return await authService.RegisterUser(request);
    }

    public async Task<EmployeeResponse> UpdateEmployee(int id, UpdateEmployeeRequest request)
    {
        var emp = await GetEmployeeById(id);

        if (request.FullName != null) emp.FullName = request.FullName;
        if (request.OutOfOfficeBalance != null) emp.OutOfOfficeBalance = request.OutOfOfficeBalance.Value;
        if (request.SubdivisionId != null)
        {
            var subdivision = await _context.Subdivisions
                .FirstOrDefaultAsync(s => s.Id == request.SubdivisionId)
                            ?? throw new NotFoundException($"Subdivision not found with id {request.SubdivisionId}", 404);
            emp.Subdivision = subdivision;
        }
        if (request.PositionId != null)
        {
            var position = await _context.Positions
                .FirstOrDefaultAsync(p => p.Id == request.PositionId)
                            ?? throw new NotFoundException($"Position not found with id {request.PositionId}", 404);
            emp.Position = position;
        }
        if (request.StatusId != null)
        {
            var status = await _context.EmployeeStatuses
                .FirstOrDefaultAsync(s => s.Id == request.StatusId)
                            ?? throw new NotFoundException($"Status not found with id {request.StatusId}", 404);
            emp.Status = status;
        }
        if (request.PeoplePartnerId != null)
        {
            var peoplePartner = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == request.PeoplePartnerId)
                            ?? throw new NotFoundException($"People partner not found with id {request.PeoplePartnerId}", 404);
            emp.PeoplePartner = peoplePartner;
        }
        if (request.RolesId != null)
        {
            var roles = await _context.Roles
                .Where(r => request.RolesId.Contains(r.Id))
                .ToListAsync();
            if (roles.Count != request.RolesId.Length)
            {
                var ids = "[" + String.Join(",", request.RolesId.Where(r => !roles.Select(r1 => r1.Id).Contains(r)).ToArray()) + "]";
                throw new NotFoundException($"Role not found with id {ids}", 404);
            }
            emp.Roles = roles.Select(role => new EmployeeRole
            {
                Employee = emp,
                Role = role
            }).ToList();
        }

        await _context.SaveChangesAsync();

        return new EmployeeResponse()
        {
            Id = emp.Id,
            FullName = emp.FullName,
            OutOfOfficeBalance = emp.OutOfOfficeBalance,
            Subdivision = emp.Subdivision.Name,
            Position = emp.Position.Name,
            Status = emp.Status.Status,
            Roles = emp.Roles.Select(empRole => empRole.Role.RoleName).ToList(),
            PartnerId = emp.PeoplePartnerId
        };
    }

    public async Task DeactivateEmployee(int id)
    {
        var emp = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id)
                  ?? throw new NotFoundException($"Employee not found with id {id}", 404);
        
        emp.Status = await _context.EmployeeStatuses.FirstOrDefaultAsync(s => s.Status == StatusType.Inactive)
                             ?? throw new NotFoundException("Status not found", 404);
        
        await _context.SaveChangesAsync();
    }

    private async Task<Employee> GetEmployeeById(int id)
    {
        return await _context.Employees
                   .Include(e => e.Subdivision)
                   .Include(e => e.Position)
                   .Include(e => e.Status)
                   .Where(e=> e.Status.Status != StatusType.Inactive)
                   .Include(e => e.Roles)
                   .ThenInclude(er => er.Role)
                   .FirstOrDefaultAsync(e => e.Id == id)
               ?? throw new NotFoundException($"Employee not found with id {id}", 404);
    }
}

public interface IEmployeeService
{
    Task<PagedList<EmployeeResponse>> GetEmployees(PageRequest request);
    
    Task<EmployeeResponse> GetEmployee(int id);
    
    Task<EmployeeResponse> AddEmployee(RegisterRequest request);
    
    Task<EmployeeResponse> UpdateEmployee(int id, UpdateEmployeeRequest request);
    
    Task DeactivateEmployee(int id);
}
