using AutoFixture;
using Moq;
using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Entry.Tests.Stubs;
using RSoft.Lib.Design.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Handlers
{

    public class RegisterStartAccrualPeriodCommandHandlerTest : TestFor<RegisterStartAccrualPeriodCommandHandler>
    {

        #region Constructors

        public RegisterStartAccrualPeriodCommandHandlerTest() : base() { }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_WhenLastPeriodNotExisting_ProcessSuccess()
        {

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            var domainService = new Mock<IAccrualPeriodDomainService>();
            domainService
                .Setup(m => m.GetByKeyAsync(It.Is<int>(g => g == year), It.Is<int>(g => g == month), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int year, int month, CancellationToken token) =>
                {
                    AccrualPeriod entity = _fixture
                        .Build<AccrualPeriod>()
                        .With(a => a.Year, year)
                        .With(a => a.Month, month)
                        .With(a => a.OpeningBalance, 0)
                        .With(a => a.TotalCredits, 0)
                        .With(a => a.TotalDebts, 0)
                        .Create();
                    return entity;
                });
            _fixture.Inject(domainService.Object);

            RegisterStartAccrualPeriodCommand command = new() { Year = year, Month = month };
            CommandResult<bool> result = await Target.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        [Test]
        public async Task HandleMediatorCommand_WhenHasLastPeriodAndItIsClosed_ProcessSuccess()
        {

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            DateTime lastPeriodDate = (new DateTime(year, month, 1)).AddMonths(-1);

            var domainService = new Mock<IAccrualPeriodDomainService>();
            domainService
                .Setup(m => m.GetByKeyAsync(It.Is<int>(g => g == year), It.Is<int>(g => g == month), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int year, int month, CancellationToken token) =>
                {
                    AccrualPeriod entity = _fixture
                        .Build<AccrualPeriod>()
                        .With(a => a.Year, year)
                        .With(a => a.Month, month)
                        .With(a => a.OpeningBalance, 0)
                        .With(a => a.TotalCredits, 0)
                        .With(a => a.TotalDebts, 0)
                        .Create();
                    return entity;
                });
            domainService
                .Setup(m => m.GetByKeyAsync(It.Is<int>(g => g == lastPeriodDate.Year), It.Is<int>(g => g == lastPeriodDate.Month), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int year, int month, CancellationToken token) =>
                {
                    AccrualPeriod entity = _fixture
                        .Build<AccrualPeriod>()
                        .With(a => a.Year, year)
                        .With(a => a.Month, month)
                        .With(a => a.OpeningBalance, 15000)
                        .With(a => a.TotalCredits, 0)
                        .With(a => a.TotalDebts, 0)
                        .Create();
                    entity.CloseAccrualPeriod(AuthenticatedUserStub.UserAdminId, 7900f, 1330f);
                    return entity;
                });
            _fixture.Inject(domainService.Object);

            RegisterStartAccrualPeriodCommand command = new() { Year = year, Month = month };
            CommandResult<bool> result = await Target.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        #endregion

    }
}
