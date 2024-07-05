using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.DTO.Requests;
using OutOfOffice.DTO.Responses;
using OutOfOffice.Services;

namespace OutOfOffice.Controllers;


[Route("list/approval-requests")]
[ApiController]
public class ApprovalRequestController(IAprovalRequestService _approvalRequestService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "HR, PM")]
    public async Task<IActionResult> GetApprovalRequests(
        [FromQuery] PageRequest request
        )
    {
        var pageList = await _approvalRequestService.GetApprovalRequests(request);
        var response = new PageListResponse<ApprovalRequestResponse>()
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
    [Authorize(Roles = "HR, PM")]
    public async Task<IActionResult> GetApprovalRequest(int id)
    {
        return Ok(await _approvalRequestService.GetApprovalRequest(id));
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "HR, PM")]
    public async Task<IActionResult> UpdateApprovalRequest(
        int id,
        [FromBody] UpdateApprovalRequestRequest request
    )
    {
        return Ok( await _approvalRequestService.UpdateApprovalRequest(id, request));
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "HR, PM")]
    public async Task<IActionResult> DeleteApprovalRequest(int id)
    {
        await _approvalRequestService.DeleteApprovalRequest(id);
        return NoContent();
    }
}