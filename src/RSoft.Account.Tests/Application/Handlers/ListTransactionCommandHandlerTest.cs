using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Account.Application.Handlers;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Account.Tests.DependencyInjection;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Tests.Application.Handlers
{
    public class ListTransactionCommandHandlerTest : TestFor<ListTransactionCommandHandler>
    {

        #region Constructors

        public ListTransactionCommandHandlerTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {
            _fixture.Customize<Transaction>(c => c.FromFactory(() => new Transaction() { Date = DateTime.UtcNow }));
            Mock<ITransactionDomainService> domainService = new();
            domainService
                .Setup(m => m.GetByFilterAsync(It.IsAny<IListTransactionFilter>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    IEnumerable<Transaction> entities = new List<Transaction>() { One<Transaction>(), One<Transaction>(), One<Transaction>()};
                    return entities;
                });
            _fixture.Inject(domainService.Object);
        }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {

            ListTransactionCommand command = new();
            CommandResult<IEnumerable<TransactionDto>> result = await Sut.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
            IEnumerable<TransactionDto> dtos = result.Response;
            Assert.NotNull(dtos);
            Assert.AreEqual(3, dtos.Count());
        }

        #endregion

    }
}
