using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.DTO.Requests;
using OutOfOffice.DTO.Responses;
using OutOfOffice.Services;

namespace OutOfOffice.Controllers;

[Route("list/leave-requests")]
[ApiController]
public class LeaveRequestController(ILeaveRequestService leaveRequestService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "HR, PM")]
    public async Task<IActionResult> GetLeaveRequests([FromQuery] PageRequest request)
    {
        var pageList = await leaveRequestService.GetLeaveRequests(request);
        var response = new PageListResponse<LeaveRequestResponse>()
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
    public async Task<IActionResult> GetLeaveRequestsForEmployee
    (
        [FromQuery] PageRequest request
    )
    {
        var user = User.Claims.FirstOrDefault(c => c.Type == "id");
        if (user == null)
        {
            return Unauthorized();
        }

        var pageList = await leaveRequestService.GetLeaveRequests(request, int.Parse(user.Value));
        var response = new PageListResponse<LeaveRequestResponse>()
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
    [Authorize(Roles = "HR, PM, EMP")]
    public async Task<IActionResult> GetLeaveRequest(int id)
    {
        return Ok(await leaveRequestService.GetLeaveRequest(id));
    }

    [HttpPost]
    [Authorize(Roles = "EMP")]
    public async Task<IActionResult> AddLeaveRequest([FromBody] AddLeaveRequestRequest request)
    {
        await leaveRequestService.AddLeaveRequest(request);
        return Created();
    }
    
    [HttpPost("{id}/approve-requests")]
    [Authorize(Roles = "EMP")]
    public async Task<IActionResult> SubmitLeaveRequest(int id)
    {
        await leaveRequestService.SubmitLeaveRequest(id);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "EMP")]
    public async Task<IActionResult> UpdateLeaveRequest(int id, [FromBody] UpdateLeaveRequestRequest request)
    {
        return Ok(await leaveRequestService.UpdateLeaveRequest(id, request));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "EMP")]
    public async Task<IActionResult> DeleteLeaveRequest(int id)
    {
        await leaveRequestService.DeleteLeaveRequest(id);
        return NoContent();
    }
} 
