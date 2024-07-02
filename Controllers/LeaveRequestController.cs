﻿using Microsoft.AspNetCore.Mvc;
using OutOfOffice.DTO.Requests;
using OutOfOffice.DTO.Responses;
using OutOfOffice.Services;

namespace OutOfOffice.Controllers;

[Route("list/leave-requests")]
[ApiController]
public class LeaveRequestController(ILeaveRequestService leaveRequestService) : ControllerBase
{
    [HttpGet]
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLeaveRequest(int id)
    {
        return Ok(await leaveRequestService.GetLeaveRequest(id));
    }

    [HttpPost]
    public async Task<IActionResult> AddLeaveRequest([FromBody] AddLeaveRequestRequest request)
    {
        await leaveRequestService.AddLeaveRequest(request);
        return Created();
    }
    
    [HttpPost("{id}/approve-requests")]
    public async Task<IActionResult> ApproveLeaveRequest(int id)
    {
        await leaveRequestService.ApproveLeaveRequest(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLeaveRequest(int id, [FromBody] UpdateLeaveRequestRequest request)
    {
        return Ok(await leaveRequestService.UpdateLeaveRequest(id, request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLeaveRequest(int id)
    {
        await leaveRequestService.DeleteLeaveRequest(id);
        return NoContent();
    }
} 
