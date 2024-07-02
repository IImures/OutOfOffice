using Microsoft.EntityFrameworkCore;
using OutOfOffice.Context;
using OutOfOffice.DTO.Requests;
using OutOfOffice.DTO.Responses;
using OutOfOffice.Entities;
using OutOfOffice.Utils;
using System.Linq.Dynamic.Core;
using AutoMapper;
using OutOfOffice.Exceptions;

namespace OutOfOffice.Services;

public class ApprovalRequestService(ApplicationContext _context, IMapper _mapper) : IAprovalRequestService
{
    
    private static readonly string[] AllowedSortColumns = { "id", "comment", "status", "employeeId" };
    private static readonly string[] AllowedSortDirections = { "asc", "desc" };
    
    public async Task<PagedList<ApprovalRequestResponse>> GetApprovalRequests(PageRequest request)
    {
        var query = GetApprovalRequestsQuery();
        if (!AllowedSortDirections.Contains(request.SortDirection) ||
            !AllowedSortColumns.Contains(request.SortBy))
        {
            throw new Exception("Bad sorting parameters");
        }
        
        string sorting = $"{request.SortBy} {request.SortDirection}";
        query = query.OrderBy(sorting);
        
        var pagedApprovalRequests = await PagedList<ApprovalRequest>.ToPagedListAsync(query, request.Page, request.PageSize);
        var approvalRequestResponses = pagedApprovalRequests.Select(a => new ApprovalRequestResponse
        {
            Id = a.Id,
            LeaveRequestId = a.LeaveRequest.Id,
            Comment = a.Comment,
            Status = a.ApprovalStatus.Status,
            Employee = _mapper.Map<EmployeeResponse>(a.Employee)
        }).ToList();
        return new PagedList<ApprovalRequestResponse>(approvalRequestResponses, request.Page, request.PageSize, pagedApprovalRequests.TotalCount);
    }

    public async Task<ApprovalRequestResponse> GetApprovalRequest(int id)
    {
        var approvalRequest = await GetApprovalRequestById(id);
        if (approvalRequest == null)
        {
            throw new NotFoundException($"Approval request with id {id} not found", 404);
        }
        return _mapper.Map<ApprovalRequestResponse>(approvalRequest);
    }

   

    public async Task AddApprovalRequest(AddApprovalRequestRequest request)
    {
       var approvalRequest = new ApprovalRequest
       {
           Comment = request.Comment,
       };
       
       var emp = await _context.Employees
           .FirstOrDefaultAsync(e => e.Id == request.EmployeeId)
           ?? throw new NotFoundException($"Employee with id {request.EmployeeId} not found", 404);
       var status = await _context.ApprovalStatuses
           .FirstOrDefaultAsync(s => s.Id == request.StatusId)
           ?? throw new NotFoundException($"Approval status with id {request.StatusId} not found", 404);
       var leaveRequest = await _context.LeaveRequests
           .FirstOrDefaultAsync(l => l.Id == request.LeaveRequestId)
              ?? throw new NotFoundException($"Leave request with id {request.LeaveRequestId} not found", 404);
       
       approvalRequest.ApprovalStatus = status;
       approvalRequest.LeaveRequest = leaveRequest;
       approvalRequest.Employee = emp;
       
       await _context.ApprovalRequests.AddAsync(approvalRequest);
       await _context.SaveChangesAsync();
    }

    public async Task<ApprovalRequestResponse> UpdateApprovalRequest(int id, UpdateApprovalRequestRequest request)
    {
        var approvalRequest = await GetApprovalRequestById(id)
            ?? throw new NotFoundException($"Approval request with id {id} not found", 404);
        if (request.Comment != null) approvalRequest.Comment = request.Comment;

        if (request.EmployeeId != null)
        {
            var emp = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == request.EmployeeId)
                ?? throw new NotFoundException($"Employee with id {request.EmployeeId} not found", 404);
            approvalRequest.Employee = emp;
        }
        if (request.StatusId != null)
        {
            var status = await _context.ApprovalStatuses
                .FirstOrDefaultAsync(s => s.Id == request.StatusId)
                ?? throw new NotFoundException($"Approval status with id {request.StatusId} not found", 404);
            approvalRequest.ApprovalStatus = status;
        }
        if (request.LeaveRequestId != null)
        {
            var leaveRequest = await _context.LeaveRequests
                .FirstOrDefaultAsync(l => l.Id == request.LeaveRequestId)
                ?? throw new NotFoundException($"Leave request with id {request.LeaveRequestId} not found", 404);
            approvalRequest.LeaveRequest = leaveRequest;
        }

        await _context.SaveChangesAsync();
        return _mapper.Map<ApprovalRequestResponse>(approvalRequest);
    }

    public async Task DeleteApprovalRequest(int id)
    {
        var approvalRequest = await GetApprovalRequestById(id)
            ?? throw new NotFoundException($"Approval request with id {id} not found", 404);
        
        approvalRequest.ApprovalStatus = await _context.ApprovalStatuses
            .FirstOrDefaultAsync(s => s.Status == ApprovalStatusType.Deleted)
            ?? throw new NotFoundException("Approval status not found", 404);
        
        await _context.SaveChangesAsync();
    }
    
    private IQueryable<ApprovalRequest> GetApprovalRequestsQuery()
    {
        IQueryable<ApprovalRequest> query = _context.ApprovalRequests
            .Include(a => a.Employee)
            .ThenInclude(e => e.Position)
            .Include(a => a.Employee)
            .ThenInclude(e => e.Subdivision)
            .Include(a => a.Employee)
            .ThenInclude(e => e.Status)
            .Include(a => a.Employee)
            .ThenInclude(e => e.Roles)
            .ThenInclude(er => er.Role)
            .Include(a => a.LeaveRequest)
            .Include(a => a.ApprovalStatus);
        return query;
    }
    
    private async Task<ApprovalRequest?> GetApprovalRequestById(int id)
    {
        return await _context.ApprovalRequests
            .Include(a => a.Employee)
            .ThenInclude(e => e.Position)
            .Include(a => a.Employee)
            .ThenInclude(e => e.Subdivision)
            .Include(a => a.Employee)
            .ThenInclude(e => e.Status)
            .Include(a => a.Employee)
            .ThenInclude(e => e.Roles)
            .ThenInclude(er => er.Role)
            .Include(a => a.ApprovalStatus)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}

public interface IAprovalRequestService
{
    Task<PagedList<ApprovalRequestResponse>> GetApprovalRequests(PageRequest request);
    Task<ApprovalRequestResponse> GetApprovalRequest(int id);
    Task AddApprovalRequest(AddApprovalRequestRequest request);
    Task<ApprovalRequestResponse> UpdateApprovalRequest(int id, UpdateApprovalRequestRequest request);
    Task DeleteApprovalRequest(int id);
}