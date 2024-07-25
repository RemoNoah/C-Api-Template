using AutoMapper;
using DotnetApiTemplate.Domain.DTO.Role;
using DotnetApiTemplate.Domain.DTO.User;
using DotnetApiTemplate.Domain.Models;
using DotnetApiTemplate.Domain.Services;
using DotnetApiTemplate.Domain.UnitOfWork;
using DotnetApiTemplate.Services.Mapping;
using DotnetApiTemplate.Services.Services;
using Moq;
using System.Linq.Expressions;

namespace DotnetApiTemplate.Tests.Services;

[TestClass]
public class RoleServiceTest
{
    private Mock<IUnitOfWork> _uowMock = null!;
    private RoleService _roleService = null!;
    private IMapper _mapper = null!;


    [TestInitialize]
    public void Setup()
    {
        MapperConfiguration mappingConfig = new(mc =>
        {
            mc.AddProfile(new RoleProfile());
        });
        IMapper mapper = mappingConfig.CreateMapper();

        _mapper = mapper;

        _uowMock = new Mock<IUnitOfWork>();
        _roleService = new(_uowMock.Object, _mapper);
    }

    [TestMethod]
    public async Task GetAllWithoutId_ReturnsRoles()
    {
        // Arrange
        List<Role> roleDTOs = [new() {Id = Guid.NewGuid(), Name = "Test" }, new() { Id = Guid.NewGuid(), Name = "Test" }, new() { Id = Guid.NewGuid(), Name = "Test" }];

        _uowMock.Setup(u => u.Roles.GetAllAsync()).ReturnsAsync(roleDTOs);

        // Act
        var result = await _roleService.GetAllWithoutId();

        // Assert
        Assert.AreEqual(roleDTOs.Count, result.Count());
        Assert.AreEqual(roleDTOs[0].Name, result.First().Name);
    }

    [TestMethod]
    public async Task GetAllWithId_ReturnsRoles()
    {
        // Arrange
        List<Role> roleDTOs = [new() { Id = Guid.NewGuid(), Name = "Test" }, new() { Id = Guid.NewGuid(), Name = "Test" }, new() { Id = Guid.NewGuid(), Name = "Test" }];

        _uowMock.Setup(u => u.Roles.GetAllAsync()).ReturnsAsync(roleDTOs);

        // Act
        var result = await _roleService.GetAllWithId();

        // Assert
        Assert.AreEqual(roleDTOs.Count, result.Count());
        Assert.AreEqual(roleDTOs[0].Id, result.First().Id);
        Assert.AreEqual(roleDTOs[0].Name, result.First().Name);
    }

    [TestMethod]
    public async Task GetIdByName_ReturnsRole()
    {
        // Arrange
        Role role = new() { Id = Guid.NewGuid(), Name = "Test" };

        _uowMock.Setup(u => u.Roles.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Role, bool>>>(), CancellationToken.None)).ReturnsAsync(role);

        // Act
        var result = await _roleService.GetIdByName(role.Name);

        // Assert
        Assert.AreEqual(role.Id.ToString(), result.ToString());
    }

    [TestMethod]
    public async Task GetNameById_ReturnsRole()
    {
        // Arrange
        Role role = new() { Id = Guid.NewGuid(), Name = "Test" };

        _uowMock.Setup(u => u.Roles.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Role, bool>>>(), CancellationToken.None)).ReturnsAsync(role);

        // Act
        var result = await _roleService.GetNameById(role.Id);

        // Assert
        Assert.AreEqual(role.Name, result);
    }

    [TestMethod]
    public async Task Create_RoleNameFound_ReturnsNewRole()
    {
        // Arrange
        Role role = new() { Id = Guid.NewGuid(), Name = "Test" };
        RoleWithoutIdDTO roleWithoutDTO = new() {  Name = role.Name };
        string expectedName = null!;

        _uowMock.Setup(u => u.Roles.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Role, bool>>>(), CancellationToken.None)).ReturnsAsync(role);

        // Act
        var result = await _roleService.Create(roleWithoutDTO);

        // Assert
        Assert.AreEqual(expectedName, result.Name);
    }

    [TestMethod]
    public async Task Create_RoleNameNotFound_ReturnsCreatedRole()
    {
        // Arrange
        Role role = new() { Id = Guid.NewGuid(), Name = "Test" };
        RoleWithoutIdDTO roleWithoutDTO = new() { Name = role.Name };

        _uowMock.Setup(u => u.Roles.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Role, bool>>>(), CancellationToken.None)).ReturnsAsync(null as Role);

        // Act
        var result = await _roleService.Create(roleWithoutDTO);

        // Assert
        Assert.AreEqual(roleWithoutDTO.Name, result.Name);
    }

    [TestMethod]
    public async Task DeleteById_RoleNotFound_ReturnsFalse()
    {
        // Arrange
        Role role = new() { Id = Guid.NewGuid(), Name = "Test" };
        RoleWithoutIdDTO roleWithoutDTO = new() { Name = role.Name };

        _uowMock.Setup(u => u.Roles.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as Role);

        // Act
        var result = await _roleService.DeleteById(role.Id);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task DeleteById_RoleFound_ReturnsTrue()
    {
        // Arrange
        Role role = new() { Id = Guid.NewGuid(), Name = "Test" };
        RoleWithoutIdDTO roleWithoutDTO = new() { Name = role.Name };

        _uowMock.Setup(u => u.Roles.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(role);
        _uowMock.Setup(u => u.Roles.Delete(It.IsAny<Role>()));

        // Act
        var result = await _roleService.DeleteById(role.Id);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task DeleteByName_RoleFound_ReturnsTrue()
    {
        // Arrange
        Role role = new() { Id = Guid.NewGuid(), Name = "Test" };
        RoleWithoutIdDTO roleWithoutDTO = new() { Name = role.Name };

        _uowMock.Setup(u => u.Roles.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Role, bool>>>(), CancellationToken.None)).ReturnsAsync(role);
        _uowMock.Setup(u => u.Roles.Delete(It.IsAny<Role>()));

        // Act
        var result = await _roleService.DeleteByName(role.Name);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task DeleteByName_RoleNotFound_ReturnsFalse()
    {
        // Arrange
        Role role = new() { Id = Guid.NewGuid(), Name = "Test" };
        RoleWithoutIdDTO roleWithoutDTO = new() { Name = role.Name };

        _uowMock.Setup(u => u.Roles.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Role, bool>>>(), CancellationToken.None)).ReturnsAsync(null as Role);
        _uowMock.Setup(u => u.Roles.Delete(It.IsAny<Role>()));

        // Act
        var result = await _roleService.DeleteByName(role.Name);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task UpdateById_RoleNotFound_ReturnsNewRole()
    {
        // Arrange
        Role role = new() { Id = Guid.NewGuid(), Name = "Test" };
        RoleWithIdDTO roleWithDTO = new() { Name = role.Name, Id = role.Id};
        string expectedName = null!;

        _uowMock.Setup(u => u.Roles.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as Role);
        _uowMock.Setup(u => u.Roles.Update(It.IsAny<Role>()));

        // Act
        var result = await _roleService.UpdateById(roleWithDTO);

        // Assert
        Assert.AreEqual(expectedName, result.Name);
    }

    [TestMethod]
    public async Task UpdateById_RoleFound_ReturnsUpdatedRole()
    {
        // Arrange
        Role role = new() { Id = Guid.NewGuid(), Name = "Test" };
        RoleWithIdDTO roleWithDTO = new() { Name = role.Name, Id = role.Id };

        _uowMock.Setup(u => u.Roles.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(role);
        _uowMock.Setup(u => u.Roles.Update(It.IsAny<Role>()));

        // Act
        var result = await _roleService.UpdateById(roleWithDTO);

        // Assert
        Assert.AreEqual(role.Name, result.Name);
    }

    [TestMethod]
    public async Task UpdateByOldName_RoleNotFound_ReturnsNewRole()
    {
        // Arrange
        Role role = new() { Id = Guid.NewGuid(), Name = "Test" };
        RoleUpdateByOldNameDTO roleUpdateByOldNameDTO = new() { OldName = role.Name, NewName = "test" };
        string expectedName = null!;

        _uowMock.Setup(u => u.Roles.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Role, bool>>>(), CancellationToken.None)).ReturnsAsync(null as Role);
        _uowMock.Setup(u => u.Roles.Update(It.IsAny<Role>()));

        // Act
        var result = await _roleService.UpdateByOldName(roleUpdateByOldNameDTO);

        // Assert
        Assert.AreEqual(expectedName, result.Name);
    }

    [TestMethod]
    public async Task UpdateByOldName_RoleFound_ReturnsUpdatedRole()
    {
        // Arrange
        Role role = new() { Id = Guid.NewGuid(), Name = "Test" };
        RoleUpdateByOldNameDTO roleUpdateByOldNameDTO = new() { OldName = role.Name, NewName = "test" };

        _uowMock.Setup(u => u.Roles.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Role, bool>>>(), CancellationToken.None)).ReturnsAsync(role);
        _uowMock.Setup(u => u.Roles.Update(It.IsAny<Role>()));

        // Act
        var result = await _roleService.UpdateByOldName(roleUpdateByOldNameDTO);

        // Assert
        Assert.AreEqual(roleUpdateByOldNameDTO.NewName, result.Name);
    }
}
