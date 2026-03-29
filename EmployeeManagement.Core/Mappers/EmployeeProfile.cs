using AutoMapper;
using EmployeeManagement.Core.DTO;
using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.Core.Mappers
{
    /// <summary>
    /// AutoMapper profile that defines mappings between Employee entity and DTOs.
    /// </summary>
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // Entity → Response DTO
            CreateMap<Employee, EmployeeResponse>();

            // Add Request DTO → Entity
            CreateMap<EmployeeAddRequest, Employee>()
                .ForMember(dest => dest.EmployeeID, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore());

            // Update Request DTO → Entity (merge into existing)
            CreateMap<EmployeeUpdateRequest, Employee>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        }
    }
}
