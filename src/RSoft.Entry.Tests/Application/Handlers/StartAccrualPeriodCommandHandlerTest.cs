using NUnit.Framework;
using RSoft.Entry.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using System;
using System.Threading.Tasks;

namespace RSoft.Entry.Tests.Application.Handlers
{

    public class StartAccrualPeriodCommandHandlerTest : TestFor<StartAccrualPeriodCommandHandler>
    {

        #region Constructors

        public StartAccrualPeriodCommandHandlerTest() : base() { }

        #endregion

        #region Tests

        [Test]
        public async Task HandleMediatorCommand_ProcessSuccess()
        {
            DateTime date = DateTime.UtcNow.AddMonths(10);
            StartAccrualPeriodCommand command = new(date.Year, date.Month);
            var result = await Sut.Handle(command, default);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        #endregion

    }
}
