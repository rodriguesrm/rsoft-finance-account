using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Contracts.Models;
using DomainEntry = RSoft.Entry.Core.Entities.Entry;
using RSoft.Entry.Core.Ports;
using RSoft.Entry.Tests.DependencyInjection;
using RSoft.Lib.Design.Application.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Handlers
{
    public class ListAccountCommandHandlerTest : TestFor<ListEntryCommandHandler>
    {

        #region Constructors

        public ListAccountCommandHandlerTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {
            Mock<IEntryDomainService> domainService = new();
            domainService
                .Setup(m => m.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {

                    IEnumerable<DomainEntry> entities = new List<DomainEntry>() { One<DomainEntry>(), One<DomainEntry>(), One<DomainEntry>()};
                    return entities;
                });
            _fixture.Inject(domainService.Object);
        }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {

            ListEntryCommand command = new();
            CommandResult<IEnumerable<EntryDto>> result = await Sut.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
            IEnumerable<EntryDto> dtos = result.Response;
            Assert.NotNull(dtos);
            Assert.AreEqual(3, dtos.Count());
        }

        #endregion

    }
}
