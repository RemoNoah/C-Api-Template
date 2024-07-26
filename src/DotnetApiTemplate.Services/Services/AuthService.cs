﻿using DotnetApiTemplate.Domain.DTO.User;
using DotnetApiTemplate.Domain.Models;
using DotnetApiTemplate.Domain.Services;
using DotnetApiTemplate.Domain.UnitOfWork;

namespace DotnetApiTemplate.Services.Services;

/// <summary>
/// Service class for managing user-related operations.
/// This class implements the <see cref="IAuthService"/> interface.
/// </summary>
/// <param name="unitOfWork"></param>
/// <param name="authService"></param>
public class AuthService(IUnitOfWork unitOfWork) : IAuthService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<User?> LoginAsync(UserLoginDTO userLoginDto)
    {
        if (userLoginDto == null)
            return null;

        User? user = await _unitOfWork.Users.GetFirstOrDefaultAsync(um => um.Email == userLoginDto.Email);
        if (user != null && userLoginDto.Email != string.Empty && !string.IsNullOrEmpty(user.Salt)
            && user.VerifyPassword(userLoginDto.Password))
            return user;

        return null;
    }

    /// <inheritdoc/>
    public async Task<User?> RegisterAsync(UserRegistrationDTO user)
    {
        if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            return null;

        User? userWithEmail= (await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Email == user.Email));

        if (userWithEmail != null)
            return null;

        User userModel = new(user.Password)
        {
            Id = Guid.NewGuid(),
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
        };

        if ((await _unitOfWork.Users.GetAllAsync()).Count() < 1)
        {
            userModel.Roles.Add((await _unitOfWork.Roles.GetFirstOrDefaultAsync(r => r.Name == "Admin"))!);
        }
        else
        {
            userModel.Roles.Add((await _unitOfWork.Roles.GetFirstOrDefaultAsync(r => r.Name == "Client"))!);
        }

        _unitOfWork.Users.Create(userModel);

        _ = await _unitOfWork.CompleteAsync();
        return userModel;
    }
}
