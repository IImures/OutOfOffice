using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.DTO.Requests;
using OutOfOffice.DTO.Responses;
using OutOfOffice.Services;

namespace OutOfOffice.Controllers;


[Route("list/employees")]
[ApiController]
public class EmployeeController : ControllerBase
{
    
    private readonly IEmployeeService _employeeService;
    
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    
    [HttpGet]
    //[Authorize(Roles = "HR")]
    public async Task<IActionResult> GetEmployees(
        [FromQuery] PageRequest request
        )
    {
        var pagedList = await _employeeService.GetEmployees(request);
        var response = new PageListResponse<EmployeeResponse>()
        {
            CurrentPage = pagedList.CurrentPage,
            TotalPages = pagedList.TotalPages,
            PageSize = pagedList.PageSize,
            TotalCount = pagedList.TotalCount,
            Items = pagedList.ToList()
        };

        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        return Ok(await _employeeService.GetEmployee(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddEmployee(
        [FromBody] RegisterRequest request
    )
    {
        await _employeeService.AddEmployee(request);
        return Created();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(
        int id,
        [FromBody] UpdateEmployeeRequest request
    )
    {
        return Ok(await _employeeService.UpdateEmployee(id, request));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeactivateEmployee(int id)
    {
        await _employeeService.DeactivateEmployee(id);
        return NoContent();
    }
}