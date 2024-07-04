using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.Services;

namespace OutOfOffice.Controllers;

[Route("list")]
[ApiController]
public class StatusesController(IStatusService statusService) : ControllerBase
{
    //[Authorize]
    [HttpGet("statuses/employees")]
    public async Task<IActionResult> GetEmployeeStatuses()
    {
        return Ok(await statusService.GetEmployeeStatuses());
    }

    //[Authorize]
    [HttpGet("statuses/approval-requests")]
    public async Task<IActionResult> GetApprovalStatuses()
    {
        return Ok(await statusService.GetApprovalStatuses());
    }

    //[Authorize]
    [HttpGet("statuses/project-statuses")]
    public async Task<IActionResult> GetProjectStatuses()
    {
        return Ok(await statusService.GetProjectStatuses());
    }
    
    //[Authorize]
    [HttpGet("statuses/project-types")]
    public async Task<IActionResult> GetProjectTypes()
    {
        return Ok(await statusService.GetProjectTypes());
    }
    
    
    //[Authorize]
    [HttpGet("statuses/leave-requests")]
    public async Task<IActionResult> GetLeaveRequestStatuses()
    {
        return Ok(await statusService.GetLeaveRequestStatuses());
    }
    
    //[Authorize]
    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        return Ok(await statusService.GetRoles());
    }
    
    //[Authorize]
    [HttpGet("subdivisions")]
    public async Task<IActionResult> GetSubdivisions()
    {
        return Ok(await statusService.GetSubdivisions());
    }
    
    //[Authorize]
    [HttpGet("positions")]
    public async Task<IActionResult> GetPositions()
    {
        return Ok(await statusService.GetPositions());
    }
}