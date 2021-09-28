using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Account.Application.Handlers;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Ports;
using RSoft.Account.Tests.DependencyInjection;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using DomainAccount = RSoft.Account.Core.Entities.Account;

namespace RSoft.Account.Tests.Application.Handlers
{

    public class ChangeStatusAccountCommandHandlerTest : TestFor<ChangeStatusAccountCommandHandler>
    {

        #region Constructors

        public ChangeStatusAccountCommandHandlerTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {
            var domainService = new Mock<IAccountDomainService>();

            domainService
                .Setup(m => m.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken token) =>
                {
                    _fixture.Customize<DomainAccount>(c => c.FromFactory(() => new DomainAccount(id)));
                    DomainAccount account = One<DomainAccount>();
                    return account;
                });


            _fixture.Inject(domainService.Object);
        }

        #endregion

        #region Tests

        [Test]
        public async Task HandleEvent_ProcessSuccess()
        {
            ChangeStatusAccountCommand command = new(Guid.NewGuid(), true);
            CommandResult<bool> result = await Sut.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        #endregion

    }
}
