using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Ports;
using RSoft.Entry.Tests.DependencyInjection;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using DomainEntry = RSoft.Entry.Core.Entities.Entry;

namespace RSoft.Entry.Tests.Application.Handlers
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
            var domainService = new Mock<IEntryDomainService>();

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
            ChangeStatusAccountCommand command = new(Guid.NewGuid(), true);
            CommandResult<bool> result = await Sut.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        #endregion

    }
}
