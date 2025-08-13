using FluentAssertions;
using FluentResults;
using FluentValidation;
using Moq;
using myLoan.Application.Features.Auth.Command.Login;
using myLoan.Application.Interface.Auth;
using myLoan.Application.Interface.Common;

namespace myLoan.test.Features.Auth
{
    public class LoginCommandHandlerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IValidationHandler> _validationHandlerMock;
        private readonly LoginCommandHandler _handler;

        public LoginCommandHandlerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _validationHandlerMock = new Mock<IValidationHandler>();

            _handler = new LoginCommandHandler(
                _authServiceMock.Object,
                _validationHandlerMock.Object
            );
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnFail_WhenRequestIsNull()
        {
            // Act
            var result = await _handler.HandleAsync(null, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message.Contains("cannot be null"));
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnFail_WhenValidationFails()
        {
            // Arrange
            var command = new LoginCommand { Email = "test@test.com", Password = "pass123" };

            _validationHandlerMock
                .Setup(v => v.ValidateAsync(It.IsAny<IValidator<LoginCommand>>(), command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Fail("Invalid credentials"));

            // Act
            var result = await _handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message.Contains("Invalid credentials"));
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnFail_WhenLoginAsyncFails()
        {
            // Arrange
            var command = new LoginCommand { Email = "test@test.com", Password = "pass123" };

            _validationHandlerMock
                .Setup(v => v.ValidateAsync(It.IsAny<IValidator<LoginCommand>>(), command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            _authServiceMock
                .Setup(s => s.LoginAsync(command.Email, command.Password))
                .ReturnsAsync(Result.Fail("Login failed"));

            // Act
            var result = await _handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message.Contains("Login failed"));
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnOk_WhenLoginSucceeds()
        {
            // Arrange
            var command = new LoginCommand { Email = "test@test.com", Password = "pass123" };

            _validationHandlerMock
                .Setup(v => v.ValidateAsync(It.IsAny<IValidator<LoginCommand>>(), command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            _authServiceMock
                .Setup(s => s.LoginAsync(command.Email, command.Password))
                .ReturnsAsync(Result.Ok("token-xyz"));

            // Act
            var result = await _handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be("token-xyz");
        }
    }
}
