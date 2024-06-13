using DotnetApiTemplate.Domain.DTO;
using DotnetApiTemplate.Api.Controllers;
using DotnetApiTemplate.Domain.Models;
using DotnetApiTemplate.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace DotnetApiTemplate.Tests.Controllers;

[TestClass]
public class UserControllerTest
{
    private UserController _userController = null!;
    private readonly Mock<IUserService> _userServiceMock = new();

    [TestInitialize]
    public void Setup()
    {
        List<KeyValuePair<string, string?>> inMemorySettings = new()
        {
            new KeyValuePair<string, string?>("Jwt:Key", "JWT_KEYqeipruhgyxcknpoaerjtowperituldkcbnklsdnövmxlckvj0arituq'riewüpfoasvlkmcxyoijv0'w8üaerpjadskvmcxlkmvölkjapofijuwpoiuerj")
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _userController = new UserController(configuration, _userServiceMock.Object);
    }

    [TestMethod]
    public void Register_UserDTOIsNUll_ReturnsBadRequest()
    {
        // Arrange
        UserRegistrationDTO newUser = null!;

        // Act
        var result = _userController.Register(newUser);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        if (result.Result is not BadRequestObjectResult badResult)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(badResult.Value, typeof(string));
        if (badResult.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public void Register_UserDTOIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        UserRegistrationDTO newUser = new();

        // Act
        var result = _userController.Register(newUser);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        if (result.Result is not BadRequestObjectResult badResult)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(badResult.Value, typeof(string));
        if (badResult.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public void Register_RegisteredUserIsNUll_ReturnsBadRequest()
    {
        // Arrange
        UserRegistrationDTO newUser = new() { Email = "Test", FirstName = "Test", Password = "Test", LastName = "test", Username = "test" };

        _ = _userServiceMock.Setup(r => r.RegisterAsync(It.IsAny<UserRegistrationDTO>())).ReturnsAsync(null as User);

        // Act
        var result = _userController.Register(newUser);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        if (result.Result is not BadRequestObjectResult badResult)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(badResult.Value, typeof(string));
        if (badResult.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public void Register_ReturnsOk()
    {
        // Arrange
        UserRegistrationDTO newUser = new() { Email = "Test", FirstName = "Test", Password = "Test", LastName = "test", Username = "test" };
        User user = new() { Email = "Test", FirstName = "Test", LastName = "test", Username = "test", Salt = "aPvpoOXmAQ5trcofo5vfDA==", Hash = "FhA5rX7AAODqo5qLd8s8a03pTRWxh2C7KheuFC3KbQ8=" };

        _ = _userServiceMock.Setup(r => r.RegisterAsync(It.IsAny<UserRegistrationDTO>())).ReturnsAsync(user);

        // Act
        var result = _userController.Register(newUser);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        if (result.Result is not OkObjectResult okResult)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(okResult.Value, typeof(string));
        if (okResult.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }


    [TestMethod]
    public void Login_UserDTOIsNUll_ReturnsBadRequest()
    {
        // Arrange
        UserLoginDTO user = null!;

        // Act
        var result = _userController.Login(user);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        if (result.Result is not BadRequestObjectResult badResult)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(badResult.Value, typeof(string));
        if (badResult.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public void Login_UserDTOIsEmpty_ReturnsBadRequest()
    {
        // Arrange
        UserLoginDTO user = new();

        // Act
        var result = _userController.Login(user);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        if (result.Result is not BadRequestObjectResult badResult)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(badResult.Value, typeof(string));
        if (badResult.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public void Login_LogedInUserIsNUll_ReturnsBadRequest()
    {
        // Arrange
        UserLoginDTO user = new() { Email = "Test", Password = "Test" };

        _ = _userServiceMock.Setup(r => r.RegisterAsync(It.IsAny<UserRegistrationDTO>())).ReturnsAsync(null as User);

        // Act
        var result = _userController.Login(user);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        if (result.Result is not BadRequestObjectResult badResult)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(badResult.Value, typeof(string));
        if (badResult.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }

    [TestMethod]
    public void Login_ReturnsOk()
    {
        // Arrange
        UserLoginDTO user = new() { Email = "Test", Password = "Test" };
        User logedInUser = new() { Email = "Test", FirstName = "Test", LastName = "test", Username = "test", Salt = "aPvpoOXmAQ5trcofo5vfDA==", Hash = "FhA5rX7AAODqo5qLd8s8a03pTRWxh2C7KheuFC3KbQ8=" };

        _ = _userServiceMock.Setup(r => r.LoginAsync(It.IsAny<UserLoginDTO>())).ReturnsAsync(logedInUser);

        // Act
        var result = _userController.Login(user);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        if (result.Result is not OkObjectResult okResult)
        {
            throw new ArgumentNullException();
        }

        Assert.IsInstanceOfType(okResult.Value, typeof(string));
        if (okResult.Value is not string)
        {
            throw new ArgumentNullException();
        }
    }
}
