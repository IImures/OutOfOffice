using System.Linq.Dynamic.Core;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.Context;
using OutOfOffice.DTO.Requests;
using OutOfOffice.DTO.Responses;
using OutOfOffice.Entities;
using OutOfOffice.Exceptions;
using OutOfOffice.Utils;

namespace OutOfOffice.Services;

public class LeaveRequestService(ApplicationContext _context, IMapper _mapper) : ILeaveRequestService
{
    
    private static readonly string[] AllowedSortColumns = { "id", "comment", "status", "startDate", "endDate"};
    private static readonly string[] AllowedSortDirections = { "asc", "desc" };
    
    public async Task<PagedList<LeaveRequestResponse>> GetLeaveRequests(PageRequest request)
    {
        var query = GetLeaveRequestsQuery();
        if (!AllowedSortDirections.Contains(request.SortDirection) ||
            !AllowedSortColumns.Contains(request.SortBy))
        {
            throw new Exception("Bad sorting parameters");
        }
        
        string sorting = $"{request.SortBy} {request.SortDirection}";
        query = query.OrderBy(sorting);
        
        var pagedApprovalRequests = await PagedList<LeaveRequest>.ToPagedListAsync(query, request.Page, request.PageSize);
        var approvalRequestResponses = pagedApprovalRequests.Select( lr => _mapper.Map<LeaveRequestResponse>(lr)).ToList();
        return new PagedList<LeaveRequestResponse>(approvalRequestResponses, pagedApprovalRequests.TotalCount, pagedApprovalRequests.CurrentPage, pagedApprovalRequests.PageSize);
    }

    public async Task<LeaveRequestResponse> GetLeaveRequest(int id)
    {
        return _mapper.Map<LeaveRequestResponse>(await _context.LeaveRequests
            .Include(lr => lr.Status)
            .Include(lr => lr.Employee)
                .ThenInclude(e => e.Subdivision)
            .Include(e => e.Employee)
                .ThenInclude(e => e.Position)
            .Include(lr => lr.Employee)
                .ThenInclude(e => e.Status)
            .Include(lr => lr.Employee)
                .ThenInclude(e => e.Roles)
                    .ThenInclude(er => er.Role)
            .FirstOrDefaultAsync(lr => lr.Id == id)
            ?? throw new NotFoundException($"Leave request with id {id} not found", 404)
        );
    }

    public async Task AddLeaveRequest(AddLeaveRequestRequest request)
    {
        var leaveRequest = new LeaveRequest()
        {
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            AbsenceReason = request.Reason,
            Comment = request.Comment,
        };
        
        var status = _context.LeaveRequestStatuses.FirstOrDefault(s => s.Status == LeaveStatusType.Created)
            ?? throw new NotFoundException("Leave request status not found", 404);
        
        var employee = _context.Employees.FirstOrDefault(e => e.Id == request.EmployeeId)
            ?? throw new NotFoundException("Employee not found", 404);
        
        leaveRequest.Employee = employee;
        leaveRequest.Status = status;
        
        await _context.LeaveRequests.AddAsync(leaveRequest);
        await _context.SaveChangesAsync();
    }

    public async Task<LeaveRequestResponse> UpdateLeaveRequest(int id, UpdateLeaveRequestRequest request)
    {
        var leaveRequest = _context.LeaveRequests.FirstOrDefault(lr => lr.Id == id)
            ?? throw new NotFoundException($"Leave request with id {id} not found", 404);
        
        if(request.Comment != null) leaveRequest.Comment = request.Comment;
        if(request.StartDate != null) leaveRequest.StartDate = request.StartDate.Value;
        if(request.EndDate != null) leaveRequest.EndDate = request.EndDate.Value;
        if(request.Reason != null) leaveRequest.AbsenceReason = request.Reason;
        
        await _context.SaveChangesAsync();
        return _mapper.Map<LeaveRequestResponse>(leaveRequest);
    }

    public async Task DeleteLeaveRequest(int id)
    {
        var leaveRequest =await _context.LeaveRequests.FirstOrDefaultAsync(lr => lr.Id == id)
            ?? throw new NotFoundException($"Leave request with id {id} not found", 404);
        
        var leaveRequestStatus = await _context.LeaveRequestStatuses.FirstOrDefaultAsync(s => s.Status == LeaveStatusType.Canceled)
            ?? throw new NotFoundException("Leave request status not found", 404);
        
        leaveRequest.Status = leaveRequestStatus;
        var approvalRequest = await _context.ApprovalRequests.FirstOrDefaultAsync(ar => ar.LeaveRequestId == id);
        if(approvalRequest != null)
        {
            var approvalRequestStatus = await _context.ApprovalStatuses.FirstOrDefaultAsync(s => s.Status == ApprovalStatusType.Canceled)
                ?? throw new NotFoundException("Approval request status not found", 404);
            approvalRequest.ApprovalStatus = approvalRequestStatus;
        }
        
        await _context.SaveChangesAsync();
    }

    public async Task ApproveLeaveRequest(int id)
    {
        var approvalRequest = new ApprovalRequest();
        
        var leaveRequest = await _context.LeaveRequests
                               .Include(lr => lr.Employee)
                               .FirstOrDefaultAsync(lr => lr.Id == id)
            ?? throw new NotFoundException($"Leave request with id {id} not found", 404);
        
        leaveRequest.Status = await _context.LeaveRequestStatuses.FirstOrDefaultAsync(s => s.Status == LeaveStatusType.Submitted)
            ?? throw new NotFoundException("Leave request status not found", 404);
        approvalRequest.LeaveRequest = leaveRequest;

        var approvalRequestStatus =
            await _context.ApprovalStatuses.FirstOrDefaultAsync(s => s.Status == ApprovalStatusType.Pending);
        approvalRequest.ApprovalStatus = approvalRequestStatus!;
        approvalRequest.Employee = leaveRequest.Employee;

        await _context.ApprovalRequests.AddAsync(approvalRequest);
        await _context.SaveChangesAsync();
    }

    private IQueryable<LeaveRequest> GetLeaveRequestsQuery()
    {
        return _context.LeaveRequests
            .Include(lr => lr.Employee)
                .ThenInclude(e => e.Subdivision)
            .Include(e => e.Employee)
                .ThenInclude(e => e.Position)
            .Include(lr => lr.Employee)
                .ThenInclude(e => e.Status)
            .Include(lr => lr.Employee)
                .ThenInclude(e => e.Roles)
                    .ThenInclude(er => er.Role)
            .Include(lr => lr.Status);

    }
}

public interface ILeaveRequestService
{
    Task<PagedList<LeaveRequestResponse>> GetLeaveRequests(PageRequest request);
    Task<LeaveRequestResponse> GetLeaveRequest(int id);
    
    Task AddLeaveRequest(AddLeaveRequestRequest request);
    Task<LeaveRequestResponse> UpdateLeaveRequest(int id, UpdateLeaveRequestRequest request);
    Task DeleteLeaveRequest(int id);
    Task ApproveLeaveRequest(int id);
}