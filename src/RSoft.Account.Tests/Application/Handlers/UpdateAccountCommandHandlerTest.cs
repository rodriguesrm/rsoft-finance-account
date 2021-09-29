using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Account.Application.Handlers;
using RSoft.Account.Contracts.Commands;
using DomainAccount = RSoft.Account.Core.Entities.Account;
using RSoft.Account.Core.Ports;
using RSoft.Account.Tests.DependencyInjection;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Tests.Application.Handlers
{

    public class UpdateAccountCommandHandlerTest : TestFor<UpdateAccountCommandHandler>
    {

        #region Constructors

        public UpdateAccountCommandHandlerTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {

            Mock<IAccountDomainService> domainService = new();

            domainService
                .Setup(m => m.Update(It.IsAny<Guid>(), It.IsAny<DomainAccount>()))
                .Returns((Guid id, DomainAccount entity) => entity);

            domainService
                .Setup(m => m.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken token) =>
                {
                    _fixture.Customize<DomainAccount>(c => c.FromFactory(() => new DomainAccount(id)));
                    DomainAccount entity = One<DomainAccount>();
                    return entity;
                });

            _fixture.Inject(domainService.Object);

        }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {
            UpdateAccountCommand command = new(Guid.NewGuid(), "ACCOUNT_NAME_UPDTED", Guid.NewGuid());
            CommandResult<bool> result = await Sut.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        #endregion

    }
}
