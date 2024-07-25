using DotnetApiTemplate.Api.Controllers;
using DotnetApiTemplate.Domain.DTO.Role;
using DotnetApiTemplate.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DotnetApiTemplate.Tests.Controllers;

[TestClass]
public class RoleControllerTest
{
    private RoleController _roleController = null!;
    private readonly Mock<IRoleService> _roleServiceMock = new();

    [TestInitialize]
    public void Setup()
    {
        _roleController = new RoleController(_roleServiceMock.Object);
    }

    [TestMethod]
    public async Task GetAllWithoutId_ReturnsOK()
    {
        // Arrange
        IEnumerable<RoleWithoutIdDTO> roleDTOs = [new() { Name="test"}, new() {Name ="test1" }];

        _ = _roleServiceMock.Setup(r => r.GetAllWithoutId()).ReturnsAsync(roleDTOs);

        // Act
        var actual = await _roleController.GetAllWithoutId();

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(OkObjectResult));
        if (actual.Result is not OkObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<RoleWithoutIdDTO>));
        if (result.Value is not IEnumerable<RoleWithoutIdDTO>)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public async Task GetAllWithId_ReturnsOK()
    {
        // Arrange
        IEnumerable<RoleWithIdDTO> roleDTOs = [new() { Name = "test" }, new() { Name = "test1" }];

        _ = _roleServiceMock.Setup(r => r.GetAllWithId()).ReturnsAsync(roleDTOs);

        // Act
        var actual = await _roleController.GetAllWithId();

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(OkObjectResult));
        if (actual.Result is not OkObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<RoleWithIdDTO>));
        if (result.Value is not IEnumerable<RoleWithIdDTO>)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public async Task GetIdByName_NameIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        string name = "";

        // Act
        var actual = await _roleController.GetIdByName(name);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(BadRequestObjectResult));
        if (actual.Result is not BadRequestObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(string));
        if (result.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public async Task GetIdByName_ResultIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        string name = "test";

        _ = _roleServiceMock.Setup(r => r.GetIdByName(It.IsAny<string>())).ReturnsAsync(Guid.Empty);

        // Act
        var actual = await _roleController.GetIdByName(name);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(BadRequestObjectResult));
        if (actual.Result is not BadRequestObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(string));
        if (result.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public async Task GetIdByName_ReturnsOk()
    {
        // Arrange
        string name = "test";

        _ = _roleServiceMock.Setup(r => r.GetIdByName(It.IsAny<string>())).ReturnsAsync(Guid.NewGuid());

        // Act
        var actual = await _roleController.GetIdByName(name);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(OkObjectResult));
        if (actual.Result is not OkObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(Guid));
        if (result.Value is not Guid)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public async Task GetNameById_NameIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        Guid id = Guid.Empty;

        // Act
        var actual = await _roleController.GetNameById(id);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(BadRequestObjectResult));
        if (actual.Result is not BadRequestObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(string));
        if (result.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public async Task GetNameById_ResultIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        _ = _roleServiceMock.Setup(r => r.GetNameById(It.IsAny<Guid>())).ReturnsAsync("");

        // Act
        var actual = await _roleController.GetNameById(id);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(BadRequestObjectResult));
        if (actual.Result is not BadRequestObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(string));
        if (result.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public async Task GetNameById_ReturnsOk()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        _ = _roleServiceMock.Setup(r => r.GetNameById(It.IsAny<Guid>())).ReturnsAsync("Test");

        // Act
        var actual = await _roleController.GetNameById(id);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(OkObjectResult));
        if (actual.Result is not OkObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(string));
        if (result.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public async Task Create_NameIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        RoleWithoutIdDTO role = new() { Name = "" };

        // Act
        var actual = await _roleController.Create(role);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(BadRequestObjectResult));
        if (actual.Result is not BadRequestObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(string));
        if (result.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public async Task Create_DTOIsNull_ReturnsBadRequest()
    {
        // Arrange
        RoleWithoutIdDTO role =null!;

        // Act
        var actual = await _roleController.Create(role);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(BadRequestObjectResult));
        if (actual.Result is not BadRequestObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(string));
        if (result.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public async Task Create_ResultIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        RoleWithoutIdDTO role = new() { Name = "aa" };

        _ = _roleServiceMock.Setup(r => r.Create(It.IsAny<RoleWithoutIdDTO>()))!.ReturnsAsync(new RoleWithoutIdDTO());

        // Act
        var actual = await _roleController.Create(role);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(BadRequestObjectResult));
        if (actual.Result is not BadRequestObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(string));
        if (result.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public async Task Create_ReturnsOk()
    {
        // Arrange
        RoleWithoutIdDTO role = new() { Name = "Test"};

        _ = _roleServiceMock.Setup(r => r.Create(It.IsAny<RoleWithoutIdDTO>())).ReturnsAsync(role);

        // Act
        var actual = await _roleController.Create(role);

        // Assert
        Assert.IsInstanceOfType(actual.Result, typeof(OkObjectResult));
        if (actual.Result is not OkObjectResult result)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(result.Value, typeof(RoleWithoutIdDTO));
        if (result.Value is not RoleWithoutIdDTO)
        {
            throw new ArgumentNullException();
        }
    }
}