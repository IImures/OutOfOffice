using Microsoft.EntityFrameworkCore;
using OutOfOffice.Context;
using OutOfOffice.DTO.Requests;
using OutOfOffice.DTO.Responses;
using OutOfOffice.Entities;
using OutOfOffice.Exceptions;
using System.Linq.Dynamic.Core;
using AutoMapper;
using OutOfOffice.Utils;


namespace OutOfOffice.Services;

public class ProjectService(ApplicationContext context, IMapper mapper) : IProjectService
{
    private static readonly string[] AllowedSortColumns = { "id", "start_date", "end_date", "ProjectStatusId", "ProjectTypeId", "ProjectManagerId", "comment" };
    private static readonly string[] AllowedSortDirections = { "asc", "desc" };
    
    public async Task<PagedList<ProjectResponse>> GetProjects(PageRequest request)
    {
        var query = GetProjectsQuery();
        if (!AllowedSortDirections.Contains(request.SortDirection) ||
            !AllowedSortColumns.Contains(request.SortBy))
        {
            throw new BadRequestParameters("Bad sorting parameters", 400);
        }
        string sorting = $"{request.SortBy} {request.SortDirection}";
        query = query.OrderBy(sorting);
        
        var pagedProjects = await PagedList<Project>.ToPagedListAsync(query, request.Page, request.PageSize);
        var projectResponses = pagedProjects.Select(mapper.Map<ProjectResponse>).ToList();
        return new PagedList<ProjectResponse>(projectResponses, pagedProjects.TotalCount, pagedProjects.CurrentPage, pagedProjects.PageSize);
        
        
    }

    public async Task<ProjectResponse> GetProject(int id)
    {
        return mapper.Map<ProjectResponse>(await GetProjectById(id));
    }

    public async Task AddProject(AddProjectRequest request)
    {
        var project = new Project()
        {
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Comment = request.Comment,
        };
        
        var status = await context.ProjectStatuses.FirstOrDefaultAsync(s => s.Id == request.ProjectStatusId)
            ?? throw new NotFoundException($"Project status with {request.ProjectStatusId} not found", 404);
        
        var type = await context.ProjectTypes.FirstOrDefaultAsync(t => t.Id == request.ProjectTypeId)
            ?? throw new NotFoundException($"Project type with {request.ProjectTypeId} not found", 404);
        
        var projectManager = await context.Employees.FirstOrDefaultAsync(e => e.Id == request.ProjectManagerId)
            ?? throw new NotFoundException($"Project manager with {request.ProjectManagerId} not found", 404);
        
        var employees = await context.Employees.Where(e => request.EmployeeIds.Contains(e.Id)).ToListAsync();
        if(employees.Count != request.EmployeeIds.Length)
        {
            var ids = "[" + String.Join(",", request.EmployeeIds.Where(r => !employees.Select(r1 => r1.Id).Contains(r)).ToArray()) + "]";
            throw new NotFoundException($"Employees not found with id {ids}", 404);
        }
        project.Status = status;
        project.Type = type;
        project.ProjectManager = projectManager;
        project.ProjectTeams = employees.Select(e => new ProjectTeam()
        {
            Employee = e,
            Project = project
        }).ToList();

        await context.Projects.AddAsync(project);
        await context.SaveChangesAsync();
    }

    public async Task<ProjectResponse> UpdateProject(int id, UpdateProjectRequest request)
    {
        var project = await GetProjectById(id);
        
        if(request.StartDate != null) project.StartDate = request.StartDate.Value;
        if(request.EndDate != null) project.EndDate = request.EndDate.Value;
        if(request.Comment != null) project.Comment = request.Comment;
        if(request.ProjectStatusId != null)
        {
            var status = await context.ProjectStatuses.FirstOrDefaultAsync(s => s.Id == request.ProjectStatusId)
                ?? throw new NotFoundException($"Project status with {request.ProjectStatusId} not found", 404);
            project.Status = status;
        }
        if(request.ProjectTypeId != null)
        {
            var type = await context.ProjectTypes.FirstOrDefaultAsync(t => t.Id == request.ProjectTypeId)
                ?? throw new NotFoundException($"Project type with {request.ProjectTypeId} not found", 404);
            project.Type = type;
        }
        if(request.ProjectManagerId != null)
        {
            var projectManager = await context.Employees.FirstOrDefaultAsync(e => e.Id == request.ProjectManagerId)
                ?? throw new NotFoundException($"Project manager with {request.ProjectManagerId} not found", 404);
            project.ProjectManager = projectManager;
        }
        if(request.EmployeeIds != null)
        {
            var employees = await context.Employees.Where(e => request.EmployeeIds.Contains(e.Id)).ToListAsync();
            if(employees.Count != request.EmployeeIds.Length)
            {
                var ids = "[" + String.Join(",", request.EmployeeIds.Where(r => !employees.Select(r1 => r1.Id).Contains(r)).ToArray()) + "]";
                throw new NotFoundException($"Employees not found with id {ids}", 404);
            }
            project.ProjectTeams = employees.Select(e => new ProjectTeam()
            {
                Employee = e,
                Project = project
            }).ToList();
        }

        await context.SaveChangesAsync();
        return mapper.Map<ProjectResponse>(project);
    }

    public async Task DeleteProject(int id)
    {
        var project = await GetProjectById(id);
        var status = await context.ProjectStatuses.FirstOrDefaultAsync(s => s.Status == ProjectStatusType.Canceled)
            ?? throw new NotFoundException("Project status not found", 404);
        project.Status = status;
        await context.SaveChangesAsync();
    }
    
    private async Task<Project> GetProjectById(int id)
    {
        return await context.Projects
                   .Where(p => p.Id == id)
                   .Include(p => p.Status)
                   .Include(p => p.Type)
                   .Include(p => p.ProjectManager)
                   .Include(p=> p.ProjectTeams)
                   .ThenInclude(pt => pt.Employee)
                   .FirstOrDefaultAsync() ??
               throw new NotFoundException("Project not found", 404);
    }
    
    private IQueryable<Project> GetProjectsQuery()
    {
        return context.Projects
            .Include(p => p.Status)
            .Include(p => p.Type)
            .Include(p => p.ProjectManager)
            .Include(p => p.ProjectTeams)
            .ThenInclude(pt => pt.Employee)
            .AsQueryable();
    }
}

public interface IProjectService
{
    Task<PagedList<ProjectResponse>> GetProjects(PageRequest request);
    Task<ProjectResponse> GetProject(int id);
    Task AddProject(AddProjectRequest request);
    Task<ProjectResponse> UpdateProject(int id, UpdateProjectRequest request);
    Task DeleteProject(int id);
}