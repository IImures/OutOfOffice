using AutoMapper;
using OutOfOffice.DTO.Requests;
using OutOfOffice.DTO.Responses;
using OutOfOffice.Entities;

namespace OutOfOffice.Utils;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<Employee, EmployeeResponse>()
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position.Name))
            .ForMember(dest => dest.Subdivision, opt => opt.MapFrom(src => src.Subdivision.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Status))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(er => er.Role.RoleName).ToList()));
        
        CreateMap<ApprovalRequest, ApprovalRequestResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ApprovalStatus.Status))
            .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee));
        
        CreateMap<LeaveRequest, LeaveRequestResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Status))
            .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee));

        CreateMap<Project, ProjectResponse>()
            .ForMember(dest => dest.ProjectManager, opt => opt.MapFrom(src => src.ProjectManager.FullName))
            .ForMember(dest => dest.ProjectStatus, opt => opt.MapFrom(src => src.Status.Status))
            .ForMember(dest => dest.ProjectType, opt => opt.MapFrom(src => src.Type.Type))
            .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.ProjectTeams.Select(pt => pt.Employee.FullName).ToList()));
        
    }
}