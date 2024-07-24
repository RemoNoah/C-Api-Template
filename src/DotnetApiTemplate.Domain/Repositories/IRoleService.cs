using DotnetApiTemplate.Domain.DTO.Role;

namespace DotnetApiTemplate.Backend.Domain.Services.Abstract;

/// <summary>
/// Interface for <see cref="IRoleService"/>.
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// Gets all roles without Id.
    /// </summary>
    /// <returns>All roles in a IEnumerable</returns>
    Task<IEnumerable<RoleWithoutIdDTO>> GetAllWithoutId();

    /// <summary>
    /// Gets all roles with Id.
    /// </summary>
    /// <returns>All roles in a IEnumerable</returns>
    Task<IEnumerable<RoleWithIdDTO>> GetAllWithId();

    /// <summary>
    /// Updates the specified role.
    /// </summary>
    /// <param name="role">The role.</param>
    /// <returns>The updated Role</returns>
    Task<RoleWithoutIdDTO> Update(RoleWithIdDTO role);

    /// <summary>
    /// Creates the specified role.
    /// </summary>
    /// <param name="role">The role.</param>
    /// <returns>The created Role</returns>
    Task<RoleWithoutIdDTO> Create(RoleWithoutIdDTO role);
}
