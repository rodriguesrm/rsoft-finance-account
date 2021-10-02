using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Contracts.Models;
using EntryAccount = RSoft.Entry.Core.Entities.Entry;
using RSoft.Entry.Core.Ports;
using RSoft.Entry.Tests.DependencyInjection;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Handlers
{
    public class GetAccountByIdCommandHandlerTest : TestFor<GetAccountByIdCommandHandler>
    {

        #region Constructors

        public GetAccountByIdCommandHandlerTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {

            EntryAccount entity = null;

            Mock<IAccountDomainService> domainService = new();
            domainService
                .Setup(m => m.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken token) =>
                {
                    _fixture.Customize<EntryAccount>(c => c.FromFactory(() => new EntryAccount(id)));
                    entity = One<EntryAccount>();
                    return entity;
                });
            _fixture.Inject(domainService.Object);

            GetAccountByIdCommand command = new(Guid.NewGuid());
            CommandResult<AccountDto> result = await Sut.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
            AccountDto dto = result.Response;
            Assert.NotNull(dto);
            Assert.AreEqual(entity.Id, dto.Id);
            Assert.AreEqual(entity.Name, dto.Name);
            Assert.AreEqual(entity.Category.Id, dto.Category.Id);
            Assert.AreEqual(entity.Category.Name, dto.Category.Name);
        }

        #endregion

    }
}
