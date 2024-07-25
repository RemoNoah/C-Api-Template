namespace DotnetApiTemplate.Domain.DTO.Role;

/// <summary>
/// Represents a Data Transfer Object (DTO) for an RoleUpdateByOldNameDTO.
/// </summary>
public class RoleUpdateByOldNameDTO
{
    /// <summary>
    /// Gets or sets the old name.
    /// </summary>
    public string OldName { get; set; } = null!;

    /// <summary>
    /// Creates new name.
    /// </summary>
    public string NewName { get; set; } = null!;
}
