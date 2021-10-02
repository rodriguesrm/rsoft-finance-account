using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using EntryAccount = RSoft.Entry.Core.Entities.Entry;
using RSoft.Entry.Core.Ports;
using RSoft.Entry.Tests.DependencyInjection;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Handlers
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
                .Setup(m => m.Update(It.IsAny<Guid>(), It.IsAny<EntryAccount>()))
                .Returns((Guid id, EntryAccount entity) => entity);

            domainService
                .Setup(m => m.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken token) =>
                {
                    _fixture.Customize<EntryAccount>(c => c.FromFactory(() => new EntryAccount(id)));
                    EntryAccount entity = One<EntryAccount>();
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
