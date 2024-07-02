using AutoMapper;
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
    }
}