using Microsoft.AspNetCore.Mvc;
using OutOfOffice.DTO.Requests;
using OutOfOffice.DTO.Responses;
using OutOfOffice.Services;

namespace OutOfOffice.Controllers;


[Route("list/approval-requests")]
[ApiController]
public class ApprovalRequestController(IAprovalRequestService _aprovalRequestService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetApprovalRequests(
        [FromQuery] PageRequest request
        )
    {
        var pageList = await _aprovalRequestService.GetApprovalRequests(request);
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
    public async Task<IActionResult> GetApprovalRequest(int id)
    {
        return Ok(await _aprovalRequestService.GetApprovalRequest(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddApprovalRequest(
        [FromBody] AddApprovalRequestRequest request
    )
    {
        await _aprovalRequestService.AddApprovalRequest(request);
        return Created();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateApprovalRequest(
        int id,
        [FromBody] UpdateApprovalRequestRequest request
    )
    {
        return Ok( await _aprovalRequestService.UpdateApprovalRequest(id, request));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApprovalRequest(int id)
    {
        await _aprovalRequestService.DeleteApprovalRequest(id);
        return NoContent();
    }
}