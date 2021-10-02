using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Contracts.Models;
using DomainEntry = RSoft.Entry.Core.Entities.Entry;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Handlers
{
    public class GetEntryByIdCommandHandlerTest : TestFor<GetEntryByIdCommandHandler>
    {

        #region Constructors

        public GetEntryByIdCommandHandlerTest() : base() { }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {

            DomainEntry entity = null;

            Mock<IEntryDomainService> domainService = new();
            domainService
                .Setup(m => m.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken token) =>
                {
                    _fixture.Customize<DomainEntry>(c => c.FromFactory(() => new DomainEntry(id)));
                    entity = One<DomainEntry>();
                    return entity;
                });
            _fixture.Inject(domainService.Object);

            GetEntryByIdCommand command = new(Guid.NewGuid());
            CommandResult<EntryDto> result = await Sut.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
            EntryDto dto = result.Response;
            Assert.NotNull(dto);
            Assert.AreEqual(entity.Id, dto.Id);
            Assert.AreEqual(entity.Name, dto.Name);
            Assert.AreEqual(entity.Category.Id, dto.Category.Id);
            Assert.AreEqual(entity.Category.Name, dto.Category.Name);
        }

        #endregion

    }
}
