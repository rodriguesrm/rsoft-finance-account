using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Lib.Common.Abstractions;
using NUnit.Framework;
using AutoFixture;
using System.Linq;
using RSoft.Entry.Tests.Stubs;
using RSoft.Entry.WorkerService.Consumers;
using RSoft.Lib.Contracts.Events;

namespace RSoft.Entry.Tests.Consumers
{

    public class UserChangedEventConsumerTest : TestFor<UserChangedEventConsumer>
    {

        #region Tests

        [Test]
        public void ConsumeMessage_ProcessSuccess()
        {
            ConsumeContext<UserChangedEvent> context = _fixture.Build<ConsumeContextStub<UserChangedEvent>>()
                .With(c => c.Message, One<UserChangedEvent>())
                .Create();
            _ = Target.Consume(context);

            LoggerStub<UserChangedEventConsumer> logger =
                ServiceActivator.GetScope().ServiceProvider.GetService<ILogger<UserChangedEventConsumer>>() as LoggerStub<UserChangedEventConsumer>;

            string checkStart = $"Process {nameof(UserChangedEvent)} MessageId:{context.MessageId} START";
            string checkEnd = $"Process {nameof(UserChangedEvent)} MesssageId:{context.MessageId} END";
            Assert.IsTrue(logger.Logs.Any(l => l == checkStart));
            Assert.IsTrue(logger.Logs.Any(l => l == checkEnd));
        }

        #endregion

    }
}
