using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Contracts.Models;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Entry.Tests.DependencyInjection;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Handlers
{
    public class GetTransactionByIdCommandHandlerTest : TestFor<GetTransactionByIdCommandHandler>
    {

        #region Constructors

        public GetTransactionByIdCommandHandlerTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {

            Transaction entity = null;

            Mock<ITransactionDomainService> domainService = new();
            domainService
                .Setup(m => m.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken token) =>
                {
                    _fixture.Customize<Transaction>(c => c.FromFactory(() => new Transaction(id)));
                    entity = One<Transaction>();
                    return entity;
                });
            _fixture.Inject(domainService.Object);

            GetTransactionByIdCommand command = new(Guid.NewGuid());
            CommandResult<TransactionDto> result = await Sut.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
            TransactionDto dto = result.Response;
            Assert.NotNull(dto);
            Assert.AreEqual(entity.Id, dto.Id);
            Assert.AreEqual(entity.Date, dto.Date);
            Assert.AreEqual(entity.PaymentMethod.Id, dto.PaymentMethod.Id);
            Assert.AreEqual(entity.PaymentMethod.Name, dto.PaymentMethod.Name);
            Assert.AreEqual(entity.Amount, dto.Amount);
            Assert.AreEqual(entity.Comment, dto.Comment);
        }

        #endregion

    }
}
