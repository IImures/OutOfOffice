﻿using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.DTO.Requests;
using OutOfOffice.Services;

namespace OutOfOffice.Controllers;


[Route("api/employees")]
[ApiController]
public class EmployeeController : ControllerBase
{
    
    private readonly IEmployeeService _employeeService;
    
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetEmployees(
        [FromQuery] PageRequest request
        )
    {
        return Ok(await _employeeService.GetEmployees(request));
    }
    //
    // [HttpGet("api/employees/{id}")]
    // public async Task<IActionResult> GetEmployee(int id)
    // {
    //     return Ok(await _employeeService.GetEmployee(id));
    // }
    //
    // [HttpPost("api/employees")]
    // public async Task<IActionResult> AddEmployee(
    //     [FromBody] AddEmployeeRequest request
    // )
    // {
    //     await _employeeService.AddEmployee(request);
    //     return Created();
    // }
    //
    // [HttpPut("api/employees/{id}")]
    // public async Task<IActionResult> UpdateEmployee(
    //     int id,
    //     [FromBody] UpdateEmployeeRequest request
    // )
    // {
    //     await _employeeService.UpdateEmployee(id, request);
    //     return Ok();
    // }
    //
    // [HttpDelete("api/employees/{id}")]
    // public async Task<IActionResult> DeleteEmployee(int id)
    // {
    //     await _employeeService.DeleteEmployee(id);
    //     return Ok();
    // }
}