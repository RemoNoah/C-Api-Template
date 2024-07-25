using AutoMapper;
using DotnetApiTemplate.Domain.DTO.Role;
using DotnetApiTemplate.Domain.Models;
using DotnetApiTemplate.Domain.Services;
using DotnetApiTemplate.Domain.UnitOfWork;
using System.Data;

namespace DotnetApiTemplate.Services.Services;

/// <summary>
/// Service class for managing user-related operations.
/// This class implements the <see cref="IAuthService"/> interface.
/// </summary>
/// <param name="unitOfWork"> <see cref="UnitOfWork"/> </param>
public class RoleService(IUnitOfWork unitOfWork, IMapper mapper) : IRoleService 
{ 
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    /// <inheritdoc/>
    public async Task<IEnumerable<RoleWithoutIdDTO>> GetAllWithoutId()
    {
        IEnumerable<RoleWithoutIdDTO> roles = (await _unitOfWork.Roles.GetAllAsync())
            .Select(r => _mapper.Map<RoleWithoutIdDTO>(r));
        return roles;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<RoleWithIdDTO>> GetAllWithId()
    {
        IEnumerable<RoleWithIdDTO> roles = (await _unitOfWork.Roles.GetAllAsync())
            .Select(r => _mapper.Map<RoleWithIdDTO>(r));
        return roles;
    }

    /// <inheritdoc/>
    public async Task<Guid> GetIdByName(string name)
    {
        Role? role = await _unitOfWork.Roles.GetFirstOrDefaultAsync(r => r.Name == name);
        return role!.Id;
    }

    /// <inheritdoc/>
    public async Task<string> GetNameById(Guid id)
    {
        Role? role = await _unitOfWork.Roles.GetFirstOrDefaultAsync(r => r.Id == id);
        return role!.Name;
    }

    /// <inheritdoc/>
    public async Task<RoleWithoutIdDTO> Create(RoleWithoutIdDTO role)
    {
        Role? existingRole = await _unitOfWork.Roles.GetFirstOrDefaultAsync(r => r.Name == role.Name);

        if (existingRole != null)
            return new();

        Role newRole = _mapper.Map<Role>(role);
        newRole.Id = Guid.NewGuid();

        _unitOfWork.Roles.Create(newRole);
        _ = await _unitOfWork.CompleteAsync();
        return _mapper.Map<RoleWithoutIdDTO>(newRole);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(Guid roleId)
    {
        Role? existingRole = await _unitOfWork.Roles.GetByIdAsync(roleId);
        if (existingRole != null)
        {
            _unitOfWork.Roles.Delete(existingRole);
            _ = await _unitOfWork.CompleteAsync();
            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<RoleWithoutIdDTO> Update(RoleWithIdDTO role)
    {
        Role? existingRole = await _unitOfWork.Roles.GetByIdAsync(role.Id);

        if (existingRole == null)
            return new();

        Role updatedRole = _mapper.Map<Role>(role);

        _unitOfWork.Roles.Update(updatedRole);
        _ = await _unitOfWork.CompleteAsync();

        return _mapper.Map<RoleWithoutIdDTO>(updatedRole);
    }
}
