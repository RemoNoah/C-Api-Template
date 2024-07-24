using AutoMapper;
using DotnetApiTemplate.Domain.DTO.User;
using DotnetApiTemplate.Domain.Models;

namespace DotnetApiTemplate.Backend.Services.Mapping;

/// <summary>
/// Initializes a new instance of the <see cref="UserProfile"/> class.
/// </summary>
public class UserProfile : Profile
{
    /// <summary>
    /// Configures the AutoMapper mapping from <see cref="User"/> to <see cref="UserDTO"/>,
    /// <see cref="User"/> to <see cref="VocationalTrainerDTO"/>.
    /// </summary>
    public UserProfile()
    {
        _ = CreateMap<User, UserLoginDTO>();

        _ = CreateMap<UserRegistrationDTO, User>();
    }
}
