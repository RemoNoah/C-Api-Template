using DotnetApiTemplate.Domain.DTO.User;
using DotnetApiTemplate.Domain.Models;
using DotnetApiTemplate.Domain.Services;
using DotnetApiTemplate.Domain.UnitOfWork;
using DotnetApiTemplate.Services.Services;
using Moq;
using System.Linq.Expressions;

namespace DotnetApiTemplate.Tests.Services;

[TestClass]
public class AuthServiceTest
{
    private Mock<IUnitOfWork> _uowMock = null!;
    private AuthService _authService = null!;


    [TestInitialize]
    public void Setup()
    {
        _uowMock = new Mock<IUnitOfWork>();
        _authService = new(_uowMock.Object);
    }

    [TestMethod]
    public void RegisterAsync_UserDTOIsNUll_ReturnsNull()
    {
        // Arrange
        UserRegistrationDTO newUser = null!;

        // Act
        var result = _authService.RegisterAsync(newUser);

        // Assert
        Assert.IsNull(result.Result);
    }

    [TestMethod]
    public void LoginAsync_UserDTOIsNUll_ReturnsNull()
    {
        // Arrange
        UserLoginDTO newUser = null!;

        // Act
        var result = _authService.LoginAsync(newUser);

        // Assert
        Assert.IsNull(result.Result);
    }

    [TestMethod]
    public void LoginAsync_UserNotFound_ReturnsNull()
    {
        // Arrange
        UserLoginDTO newUser = new();
        _uowMock.Setup(r => r.Users.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), CancellationToken.None)).ReturnsAsync(null as User);

        // Act
        var result = _authService.LoginAsync(newUser);

        // Assert
        Assert.IsNull(result.Result);
    }
}
