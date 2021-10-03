using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Contracts.Models;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Handlers
{
    public class GetPaymentMethodByIdCommandHandlerTest : TestFor<GetPaymentMethodByIdCommandHandler>
    {

        #region Constructors

        public GetPaymentMethodByIdCommandHandlerTest() : base() { }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {

            PaymentMethod entity = null;

            Mock<IPaymentMethodDomainService> domainService = new();
            domainService
                .Setup(m => m.GetByKeyAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken token) =>
                {
                    _fixture.Customize<PaymentMethod>(c => c.FromFactory(() => new PaymentMethod(id)));
                    entity = One<PaymentMethod>();
                    return entity;
                });
            _fixture.Inject(domainService.Object);

            GetPaymentMethodByIdCommand command = new(Guid.NewGuid());
            CommandResult<PaymentMethodDto> result = await Target.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
            PaymentMethodDto dto = result.Response;
            Assert.NotNull(dto);
            Assert.AreEqual(entity.Id, dto.Id);
            Assert.AreEqual(entity.Name, dto.Name);
            Assert.AreEqual(entity.PaymentType, dto.PaymentType);
        }

        #endregion

    }
}
