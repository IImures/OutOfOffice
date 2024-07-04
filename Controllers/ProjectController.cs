using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OutOfOffice.DTO.Requests;
using OutOfOffice.DTO.Responses;
using OutOfOffice.Services;

namespace OutOfOffice.Controllers;

[Route("list/projects")]
[ApiController]
public class ProjectController(IProjectService projectService) : ControllerBase
{

    [HttpGet]
    [Authorize(Roles = "HR, PM")]
    public async Task<IActionResult> GetProjects
    (
        [FromQuery] PageRequest request
    )
    {
        var pageList = await projectService.GetProjects(request);
        var response = new PageListResponse<ProjectResponse>()
        {
            CurrentPage = pageList.CurrentPage,
            TotalPages = pageList.TotalPages,
            PageSize = pageList.PageSize,
            TotalCount = pageList.TotalCount,
            Items = pageList.ToList()
        };
        return Ok(response);
    }

    [HttpGet("employees")]
    [Authorize(Roles = "EMP")]
    public async Task<IActionResult> GetProjectsForEmp
    (
        [FromQuery] PageRequest request
    )
    {
        var user = User.Claims.FirstOrDefault(c => c.Type == "id");
        if (user == null)
        {
            return Unauthorized();
        }

        var pageList = await projectService.GetProjects(request, int.Parse(user.Value));
        var response = new PageListResponse<ProjectResponse>()
        {
            CurrentPage = pageList.CurrentPage,
            TotalPages = pageList.TotalPages,
            PageSize = pageList.PageSize,
            TotalCount = pageList.TotalCount,
            Items = pageList.ToList()
        };
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "PM, HR, EMP")]
    public async Task<IActionResult> GetProject(int id)
    {
        return Ok(await projectService.GetProject(id));
    }
    
    [HttpPost]
    [Authorize(Roles = "PM")]
    public async Task<IActionResult> AddProject
    (
        [FromBody] AddProjectRequest request
    )
    {
        await projectService.AddProject(request);
        return Created();
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "PM")]
    public async Task<IActionResult> UpdateProject
    (
        int id,
        [FromBody] UpdateProjectRequest request
    )
    {
        return Ok( await projectService.UpdateProject(id, request));
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "PM")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        await projectService.DeleteProject(id);
        return NoContent();
    }
    
    
}