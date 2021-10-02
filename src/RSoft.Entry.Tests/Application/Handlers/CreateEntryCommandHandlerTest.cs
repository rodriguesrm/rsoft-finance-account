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
    
    public class CreateEntryCommandHandlerTest : TestFor<CreateEntryCommandHandler>
    {

        #region Constructors

        public CreateEntryCommandHandlerTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {
            Mock<IEntryDomainService> domainService = new();
            domainService
                .Setup(m => m.AddAsync(It.IsAny<DomainEntry>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((DomainEntry entity, CancellationToken token) => entity);
            _fixture.Inject(domainService.Object);
        }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {
            CreateEntryCommand command = new("ENTRY_NAME", Guid.NewGuid());
            CommandResult<Guid?> result = await Sut.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        #endregion

    }
}
