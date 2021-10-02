using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Handlers
{

    public class UpdatePaymentMethodCommandHandlerTest : TestFor<UpdatePaymentMethodCommandHandler>
    {

        #region Constructors

        public UpdatePaymentMethodCommandHandlerTest() : base() { }

        #endregion

        #region Overrides

        protected override void Setup(IFixture fixture)
        {

            Mock<IPaymentMethodDomainService> domainService = new();

            domainService
                .Setup(m => m.Update(It.IsAny<Guid>(), It.IsAny<PaymentMethod>()))
                .Returns((Guid id, PaymentMethod entity) => entity);

            domainService
                .Setup(m => m.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken token) =>
                {
                    _fixture.Customize<PaymentMethod>(c => c.FromFactory(() => new PaymentMethod(id)));
                    PaymentMethod entity = One<PaymentMethod>();
                    return entity;
                });

            _fixture.Inject(domainService.Object);

        }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {
            UpdatePaymentMethodCommand command = new(Guid.NewGuid(), "PAYMENT_METHOD_UPDATED", 2);
            CommandResult<bool> result = await Sut.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        #endregion

    }
}
