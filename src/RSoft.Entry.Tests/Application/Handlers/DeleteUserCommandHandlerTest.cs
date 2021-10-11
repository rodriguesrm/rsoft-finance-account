using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Handlers
{
    
    public class DeleteUserCommandHandlerTest : TestFor<DeleteUserCommandHandler>
    {

        #region Constructors

        public DeleteUserCommandHandlerTest() : base() { }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {

            User user = One<User>();
            Mock<IUserDomainService> domainService = new();

            domainService
                .Setup(m => m.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken token) => user);

            domainService
                .Setup(m => m.Update(It.IsAny<Guid>(), It.IsAny<User>()))
                .Callback((Guid id, User userArg) =>
                {
                    user.Name = userArg.Name;
                    user.IsActive = userArg.IsActive;
                });

            _fixture.Inject(domainService.Object);


            DeleteUserCommand command = new(user.Id);
            CommandResult<bool> result = await Target.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.AreEqual("USER", user.Name.FirstName);
            Assert.AreEqual("DELETED", user.Name.LastName);
            Assert.IsFalse(user.IsActive);
        }

        #endregion

    }
}
