using FluentAssertions;
using FluentResults;
using FluentValidation;
using Moq;
using myLoan.Application.Features.Auth.Command.Register;
using myLoan.Application.Interface.Auth;
using myLoan.Application.Interface.Common;
using myLoan.Application.Interface.MyLoanRepository;
using myLoan.Domain.myLoanDbEntities;

namespace myLoan.test.Features.Auth
{
    public class RegisterCommandHandlerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly Mock<IValidationHandler> _validationHandlerMock;
        private readonly RegisterCommandHandler _handler;

        public RegisterCommandHandlerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _repositoryMock = new Mock<IUserRepository>();
            _validationHandlerMock = new Mock<IValidationHandler>();

            _handler = new RegisterCommandHandler(
                _authServiceMock.Object,
                _repositoryMock.Object,
                _validationHandlerMock.Object
            );
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnFail_WhenValidationFails()
        {
            // Arrange
            var command = new RegisterCommand();
            _validationHandlerMock
                .Setup(v => v.ValidateAsync(It.IsAny<IValidator<RegisterCommand>>(), command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Fail("Invalid data"));

            // Act
            var result = await _handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message.Contains("Invalid data"));
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnFail_WhenRegisterAsyncFails()
        {
            // Arrange
            var command = new RegisterCommand();
            _validationHandlerMock
                .Setup(v => v.ValidateAsync(It.IsAny<IValidator<RegisterCommand>>(), command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            _authServiceMock
                .Setup(s => s.RegisterAsync(It.IsAny<myLoan.Domain.Entities.ApplicationUser>()))
                .ReturnsAsync(Result.Fail("Auth error"));

            // Act
            var result = await _handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message.Contains("Auth error"));
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnOk_WhenAllStepsSucceed()
        {
            // Arrange
            var command = new RegisterCommand();
            _validationHandlerMock
                .Setup(v => v.ValidateAsync(It.IsAny<IValidator<RegisterCommand>>(), command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            _authServiceMock
                .Setup(s => s.RegisterAsync(It.IsAny<myLoan.Domain.Entities.ApplicationUser>()))
                .ReturnsAsync(Result.Ok("token-123"));

            _repositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<myLoan.Domain.myLoanDbEntities.User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            // Act
            var result = await _handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be("token-123");
        }
    }
}