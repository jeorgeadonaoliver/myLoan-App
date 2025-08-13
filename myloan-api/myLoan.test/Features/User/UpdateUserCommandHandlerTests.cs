using FluentAssertions;
using FluentResults;
using FluentValidation;
using Moq;
using myLoan.Application.Features.Users.Command;
using myLoan.Application.Interface.Common;
using myLoan.Application.Interface.MyLoanRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myLoan.test.Features.User
{
    public class UpdateUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly Mock<IValidationHandler> _validationHandlerMock;
        private readonly UpdateUserCommandHandler _handler;

        public UpdateUserCommandHandlerTests()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _validationHandlerMock = new Mock<IValidationHandler>();

            _handler = new UpdateUserCommandHandler(
                _repositoryMock.Object,
                _validationHandlerMock.Object
            );
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnFail_WhenValidationFails()
        {
            // Arrange
            var command = new UpdateUserCommand { UserId = 1, LastName = "Test" };

            _validationHandlerMock
                .Setup(v => v.ValidateAsync(It.IsAny<IValidator<UpdateUserCommand>>(), command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Fail("Invalid user data"));

            // Act
            var result = await _handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message.Contains("Invalid user data"));
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<myLoan.Domain.myLoanDbEntities.User>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnFail_WhenRepositoryFails()
        {
            // Arrange
            var command = new UpdateUserCommand { UserId = 1, LastName = "Test" };

            _validationHandlerMock
                .Setup(v => v.ValidateAsync(It.IsAny<IValidator<UpdateUserCommand>>(), command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            _repositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<myLoan.Domain.myLoanDbEntities.User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Fail("Update failed"));

            // Act
            var result = await _handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message.Contains("Update failed"));
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnFail_WhenRepositoryReturnsZero()
        {
            // Arrange
            var command = new UpdateUserCommand { UserId = 1, LastName = "Test" };

            _validationHandlerMock
                .Setup(v => v.ValidateAsync(It.IsAny<IValidator<UpdateUserCommand>>(), command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            _repositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<myLoan.Domain.myLoanDbEntities.User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok(0)); // No rows affected

            // Act
            var result = await _handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(); // no rows affected is treated as fail
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnOk_WhenUpdateSucceeds()
        {
            // Arrange
            var command = new UpdateUserCommand { UserId = 1, LastName = "Test" };

            _validationHandlerMock
                .Setup(v => v.ValidateAsync(It.IsAny<IValidator<UpdateUserCommand>>(), command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            _repositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<myLoan.Domain.myLoanDbEntities.User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok(1)); // One row updated

            // Act
            var result = await _handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(1);
        }
    }
}
