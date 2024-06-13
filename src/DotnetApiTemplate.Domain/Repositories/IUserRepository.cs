using DotnetApiTemplate.Domain.Models;

namespace DotnetApiTemplate.Domain.Repositories;

/// <summary>
/// Interface for <see cref="IUserRepository"/> specific queries and methods.
/// </summary>
public interface IUserRepository : IGenericRepository<User>
{
}
