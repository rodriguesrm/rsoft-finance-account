using AutoFixture;
using AutoFixture.Kernel;
using Microsoft.Extensions.Localization;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Entry.Tests.Stubs;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Handlers
{

    public class RollbackTransactionCommandHandlerTest : TestFor<RollbackTransactionCommandHandler>
    {

        #region Local objects/variables

        private readonly Guid _transactionId = Guid.NewGuid();

        #endregion

        #region Constructors

        public RollbackTransactionCommandHandlerTest() : base() { }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {

            Mock<ITransactionDomainService> domainService = new();
            
            domainService
                .Setup(m => m.GetByKeyAsync(It.Is<Guid>(p => p == _transactionId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    _fixture.Customize<Transaction>(c => c.FromFactory(() => new Transaction(_transactionId)));
                    Transaction entity = One<Transaction>();
                    return entity;
                });

            domainService
                .Setup(m => m.GetByKeyAsync(It.Is<Guid>(p => p != _transactionId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);

            domainService
                .Setup(m => m.AddAsync(It.IsAny<Transaction>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Transaction entity, CancellationToken token) => entity);

            _fixture.Inject(domainService.Object);
            _fixture.Customizations.Add(new TypeRelay(typeof(IStringLocalizer<RollbackTransactionCommandHandler>), typeof(StringLocalizerStub<RollbackTransactionCommandHandler>)));

        }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {
            RollbackTransactionCommand command = new(_transactionId, "ROLLBACK OK");
            CommandResult<Guid?> result = await Target.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Response);
            Assert.True(result.Response.HasValue);
        }

        [Test]
        public async Task HandleMediatorCommand_WhenTransactionNotFound_ProcessFail()
        {
            RollbackTransactionCommand command = new(Guid.NewGuid(), "ROLLBACK FAIL");
            CommandResult<Guid?> result = await Target.Handle(command, default);
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.True(result.Errors.Count > 0);
            Assert.True(result.Errors.Any(e => e.Message == "TRANSACTION_NOT_FOUND"));
        }

        #endregion

    }

}
