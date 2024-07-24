namespace DotnetApiTemplate.Backend.Domain.Services.Abstract;

/// <summary>
/// Interface for <see cref="IRoleService"/>.
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// Gets all roles.
    /// </summary>
    /// <returns>All roles.</returns>
    Task<IEnumerable<RoleDTO>> GetAll();

    /// <summary>
    /// Gets the roles by user id.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns>All roles from a user.</returns>
    Task<IEnumerable<RoleDTO>> GetByUserId(Guid userId);

    /// <summary>
    /// Adds the specified role.
    /// </summary>
    /// <param name="role">The role.</param>
    /// <returns>Task</returns>
    Task Add(RoleDTO role);
}
