using AutoMapper;
using DotnetApiTemplate.Domain.DTO.Role;
using DotnetApiTemplate.Domain.Models;

namespace DotnetApiTemplate.Services.Mapping;

/// <summary>
/// Initializes a new instance of the <see cref="RoleProfile"/> class.
/// </summary>
public class RoleProfile : Profile
{
    /// <summary>
    /// Configures the AutoMapper mapping from <see cref="User"/> to <see cref="UserDTO"/>,
    /// <see cref="User"/> to <see cref="VocationalTrainerDTO"/>.
    /// </summary>
    public RoleProfile()
    {
        _ = CreateMap<Role, RoleWithIdDTO>();

        _ = CreateMap<Role, RoleWithoutIdDTO>();

        _ = CreateMap<RoleWithoutIdDTO, Role>();

        _ = CreateMap<RoleWithIdDTO, Role>();

        _ = CreateMap<RoleUpdateByOldNameDTO, Role>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NewName));
    }
}
