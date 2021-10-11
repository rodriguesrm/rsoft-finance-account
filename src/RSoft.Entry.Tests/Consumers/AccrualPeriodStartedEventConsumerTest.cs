using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Finance.Contracts.Events;
using RSoft.Lib.Common.Abstractions;
using System;
using NUnit.Framework;
using AutoFixture;
using System.Linq;
using RSoft.Entry.Tests.Stubs;
using RSoft.Entry.WorkerService.Consumers;

namespace RSoft.Entry.Tests.Consumers
{
    public class AccrualPeriodStartedEventConsumerTest : TestFor<AccrualPeriodStartedEventConsumer>
    {

        #region Tests

        [Test]
        public void ConsumeMessage_ProcessSuccess()
        {
            ConsumeContext<AccrualPeriodStartedEvent> context = _fixture.Build<ConsumeContextStub<AccrualPeriodStartedEvent>>()
                .With(c => c.Message, new AccrualPeriodStartedEvent(DateTime.UtcNow.Year, DateTime.UtcNow.Month))
                .Create();
            _ = Target.Consume(context);

            LoggerStub<AccrualPeriodStartedEventConsumer> logger = 
                ServiceActivator.GetScope().ServiceProvider.GetService<ILogger<AccrualPeriodStartedEventConsumer>>() as LoggerStub<AccrualPeriodStartedEventConsumer>;

            string checkStart = $"Process {nameof(AccrualPeriodStartedEvent)} MessageId:{context.MessageId} START";
            string checkEnd = $"Process {nameof(AccrualPeriodStartedEvent)} MesssageId:{context.MessageId} END";
            Assert.IsTrue(logger.Logs.Any(l => l == checkStart));
            Assert.IsTrue(logger.Logs.Any(l => l == checkEnd));
        }

        #endregion

    }
}
