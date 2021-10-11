using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Handlers
{
    
    public class CreateOrUpdateUserCommandHandlerTest : TestFor<CreateOrUpdateUserCommandHandler>
    {

        #region Local objects/variables

        private readonly IList<User> _users;

        #endregion

        #region Constructors

        public CreateOrUpdateUserCommandHandlerTest() 
        {
            _users = new List<User>();
        }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {

            Mock<IUserDomainService> domainService = new();

            domainService
                .Setup(m => m.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken token) => _users.FirstOrDefault(u => u.Id == id));

            domainService
                .Setup(m => m.Update(It.IsAny<Guid>(), It.IsAny<User>()))
                .Callback((Guid id, User userArg) =>
                {
                    User existingUser = _users.FirstOrDefault(u => u.Id == id);
                    if (existingUser != null)
                    {
                        existingUser.Name = userArg.Name;
                        existingUser.IsActive = userArg.IsActive;
                    }
                });

            domainService
                .Setup(m => m.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User user, CancellationToken cancellation) =>
                {
                    _users.Add(user);
                    return user;
                });

            _fixture.Inject(domainService.Object);

            base.Setup(fixture);
        }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCreateCommand_WhenEntityNotExists_CreateUserAndReturnId()
        {
            CreateOrUpdateUserCommand command = new(true, Guid.NewGuid(), "WANDA", "MAXIMOFF", true);
            CommandResult<Guid?> result = await Target.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        [Test]
        public async Task HandleMediatorCreateCommand_WhenEntityAlreadyExists_NotUpdateUserAndReturnId()
        {
            User user = new()
            {
                Name = new("CAROL", "DANVERS"),
                IsActive = true
            };
            _users.Add(user);
            CreateOrUpdateUserCommand command = new(true, user.Id, "WANDA", "MAXIMOFF", !user.IsActive);
            CommandResult<Guid?> result = await Target.Handle(command, default);
            Assert.NotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Response.HasValue);
            Assert.AreEqual(user.Id, result.Response.Value);
            User userCheck = _users.FirstOrDefault(u => u.Id == user.Id);
            Assert.NotNull(userCheck);
            Assert.AreEqual(user.Name.FirstName, userCheck.Name.FirstName);
            Assert.AreEqual(user.Name.LastName, userCheck.Name.LastName);
            Assert.AreEqual(user.IsActive, userCheck.IsActive);
        }

        [Test]
        public async Task HandleMediatorUpdateCommand_WhenEntityNotExists_CreateUserAndReturndId()
        {
            CreateOrUpdateUserCommand command = new(false, Guid.NewGuid(), "NATASHA", "ROMANOFF", true);
            CommandResult<Guid?> result = await Target.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.IsTrue(result.Response.HasValue);
            User userCheck = _users.FirstOrDefault(u => u.Id == result.Response.Value);
            Assert.NotNull(userCheck);
            Assert.AreEqual(command.FirstName, userCheck.Name.FirstName);
            Assert.AreEqual(command.LastName, userCheck.Name.LastName);
        }

        [Test]
        public async Task HandleMediatorUpdateCommand_WhenEntityAlreadyExists_UpdateUserAndReturndId()
        {
            User user = new()
            {
                Name = new("BLACK", "WIDOW"),
                IsActive = false
            };
            _users.Add(user);
            CreateOrUpdateUserCommand command = new(false, user.Id, "NATASHA", "ROMANOFF", true);
            CommandResult<Guid?> result = await Target.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.IsTrue(result.Response.HasValue);
            User userCheck = _users.FirstOrDefault(u => u.Id == result.Response.Value);
            Assert.NotNull(userCheck);
            Assert.AreEqual(command.FirstName, userCheck.Name.FirstName);
            Assert.AreEqual(command.LastName, userCheck.Name.LastName);
            Assert.AreEqual(command.IsActive, userCheck.IsActive);
        }

        [Test]
        public async Task HandleMediatorUpdateCommand_WhenEntityIsInvalid_ReturnSuccessFalse()
        {
            Guid userId = Guid.NewGuid();
            User user = new(userId)
            {
                Name = new("BLACK", "WIDOW"),
                IsActive = false
            };
            _users.Add(user);
            CreateOrUpdateUserCommand command = new(false, user.Id, "N", "R", true);
            CommandResult<Guid?> result = await Target.Handle(command, default);
            Assert.NotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsFalse(result.Response.HasValue);
            Assert.IsTrue(result.HasError);
            Assert.AreEqual(2, result.Errors.Count);
            Assert.IsTrue(result.Errors.Any(e => e.Message == "FIRST_NAME_MIN_SIZE"));
            Assert.IsTrue(result.Errors.Any(e => e.Message == "LAST_NAME_MIN_SIZE"));
        }

        #endregion

    }
}
