using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AccrualPeriodDomain = RSoft.Entry.Core.Entities.AccrualPeriod;

namespace RSoft.Entry.Tests.Application.Handlers
{

    public class CloseAccrualPeriodCommandHandlerTest : TestFor<CloseAccrualPeriodCommandHandler>
    {

        #region Constructors

        public CloseAccrualPeriodCommandHandlerTest() : base() { }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {

            var domainService = new Mock<IAccrualPeriodDomainService>();
            domainService
                .Setup(m => m.GetByKeyAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int year, int month, CancellationToken CancellationToken) =>
                {
                    AccrualPeriodDomain entity = _fixture.Build<AccrualPeriodDomain>()
                        .With(e => e.Year, year)
                        .With(e => e.Month, month)
                        .Create();
                    return entity;
                });

            domainService
                .Setup(m => m.ClosePeriodAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int year, int month, CancellationToken CancellationToken) => new SimpleOperationResult(true, null));

            _fixture.Inject(domainService.Object);

            DateTime date = DateTime.UtcNow.AddMonths(11);
            CloseAccrualPeriodCommand command = new(date.Year, date.Month);
            var result = await Target.Handle(command, default);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Response);
        }

        [Test]
        public async Task HandleMediatorCommand_WhenCloseOperationFail_ProcessFailtReturnFalse()
        {

            var domainService = new Mock<IAccrualPeriodDomainService>();
            domainService
                .Setup(m => m.GetByKeyAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int year, int month, CancellationToken CancellationToken) =>
                {
                    AccrualPeriodDomain entity = _fixture.Build<AccrualPeriodDomain>()
                        .With(e => e.Year, year)
                        .With(e => e.Month, month)
                        .Create();
                    return entity;
                });

            domainService
                .Setup(m => m.ClosePeriodAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int year, int month, CancellationToken CancellationToken) =>
                {
                    return new SimpleOperationResult(false, new Dictionary<string, string>
                    {
                        { "TEST", "ERROR" }
                    });
                });

            _fixture.Inject(domainService.Object);

            DateTime date = DateTime.UtcNow.AddMonths(11);
            CloseAccrualPeriodCommand command = new(date.Year, date.Month);
            var result = await Target.Handle(command, default);
            Assert.NotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsFalse(result.Response);
        }

        #endregion

    }
}
