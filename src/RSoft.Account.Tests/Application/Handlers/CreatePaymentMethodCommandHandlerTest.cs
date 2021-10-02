using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Entry.Tests.DependencyInjection;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Handlers
{
    
    public class CreatePaymentMethodCommandHandlerTest : TestFor<CreatePaymentMethodCommandHandler>
    {

        #region Constructors

        public CreatePaymentMethodCommandHandlerTest()
        {
            ServiceInjection.BuildProvider();
        }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {
            Mock<IPaymentMethodDomainService> domainService = new();
            domainService
                .Setup(m => m.AddAsync(It.IsAny<PaymentMethod>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((PaymentMethod entity, CancellationToken token) => entity);
            _fixture.Inject(domainService.Object);
        }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {
            CreatePaymentMethodCommand command = new("PAYMENT_METHOD", 1);
            CommandResult<Guid?> result = await Sut.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        #endregion

    }
}
