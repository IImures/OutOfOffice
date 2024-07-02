using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.DTO.Requests;
using OutOfOffice.DTO.Responses;
using OutOfOffice.Services;

namespace OutOfOffice.Controllers;

[Route("list/projects")]
[ApiController]
public class ProjectController(IProjectService projectService) : ControllerBase
{

    [HttpGet]
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
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        return Ok(await projectService.GetProject(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddProject
    (
        [FromBody] AddProjectRequest request
    )
    {
        await projectService.AddProject(request);
        return Created();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject
    (
        int id,
        [FromBody] UpdateProjectRequest request
    )
    {
        return Ok( await projectService.UpdateProject(id, request));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        await projectService.DeleteProject(id);
        return NoContent();
    }
    
    
}