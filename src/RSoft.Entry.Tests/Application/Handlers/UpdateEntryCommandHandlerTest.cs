using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using DomainEntry = RSoft.Entry.Core.Entities.Entry;
using RSoft.Entry.Core.Ports;
using RSoft.Entry.Tests.DependencyInjection;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Handlers
{

    public class UpdateEntryCommandHandlerTest : TestFor<UpdateEntryCommandHandler>
    {

        #region Constructors

        public UpdateEntryCommandHandlerTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {

            Mock<IEntryDomainService> domainService = new();

            domainService
                .Setup(m => m.Update(It.IsAny<Guid>(), It.IsAny<DomainEntry>()))
                .Returns((Guid id, DomainEntry entity) => entity);

            domainService
                .Setup(m => m.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken token) =>
                {
                    _fixture.Customize<DomainEntry>(c => c.FromFactory(() => new DomainEntry(id)));
                    DomainEntry entity = One<DomainEntry>();
                    return entity;
                });

            _fixture.Inject(domainService.Object);

        }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {
            UpdateEntryCommand command = new(Guid.NewGuid(), "ENTRY_NAME_UPDTED", Guid.NewGuid());
            CommandResult<bool> result = await Sut.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        #endregion

    }
}
