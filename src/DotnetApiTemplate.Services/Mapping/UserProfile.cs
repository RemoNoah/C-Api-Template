using AutoMapper;
using DotnetApiTemplate.Domain.DTO.User;
using DotnetApiTemplate.Domain.Models;

namespace DotnetApiTemplate.Services.Mapping;

/// <summary>
/// Initializes a new instance of the <see cref="UserProfile"/> class.
/// </summary>
public class UserProfile : Profile
{
    /// <summary>
    /// Configures the AutoMapper mapping from 
    /// <see cref="User"/> to <see cref="UserLoginDTO"/>,
    /// <see cref="UserRegistrationDTO"/> to <see cref="User"/>.
    /// </summary>
    public UserProfile()
    {
        _ = CreateMap<User, UserLoginDTO>();

        _ = CreateMap<UserRegistrationDTO, User>();
    }
}
