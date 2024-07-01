using Microsoft.EntityFrameworkCore;
using OutOfOffice.Context;
using OutOfOffice.DTO.Requests;
using OutOfOffice.Entities;
using OutOfOffice.Utils;
using System.Linq.Dynamic.Core;
using OutOfOffice.DTO.Responses;

namespace OutOfOffice.Services;

public class EmployeeService(ApplicationContext context) : IEmployeeService
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
            .Include(e => e.Roles)
                .ThenInclude(er=> er.Role)
            .AsQueryable();
        if (!AllowedSortDirections.Contains(request.SortDirection) ||
            !AllowedSortColumns.Contains(request.SortBy))
        {
            throw new Exception("Bad request");
        }

        string sorting = $"{request.SortBy} {request.SortDirection}";
        query = query.OrderBy(sorting);
        
        var pagedEmployees = await PagedList<Employee>.ToPagedListAsync(query, request.Page, request.PageSize);
        var employeeResponses = pagedEmployees.Select(e => new EmployeeResponse
        {
            Id = e.Id,
            FullName = e.FullName,
            OutOffOfficeBalance = e.OutOfOfficeBalance,
            Subdivision = e.Subdivision.Name,
            Position = e.Position.Name,
            Status = e.Status.Status,
            Roles = e.Roles.Select(empRole => empRole.Role.RoleName).ToList(),
            PartnerId = e.PeoplePartnerId
        }).ToList();

        return new PagedList<EmployeeResponse>(employeeResponses, pagedEmployees.TotalCount, pagedEmployees.CurrentPage, pagedEmployees.PageSize);
       
    }
    
}

public interface IEmployeeService
{
    Task<PagedList<EmployeeResponse>> GetEmployees(PageRequest request);
}
