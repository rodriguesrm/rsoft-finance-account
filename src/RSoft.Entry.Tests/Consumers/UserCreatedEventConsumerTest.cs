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

    public class UserCreatedEventConsumerTest : TestFor<UserCreatedEventConsumer>
    {

        #region Tests

        [Test]
        public void ConsumeMessage_ProcessSuccess()
        {
            ConsumeContext<UserCreatedEvent> context = _fixture.Build<ConsumeContextStub<UserCreatedEvent>>()
                .With(c => c.Message, One<UserCreatedEvent>())
                .Create();
            _ = Target.Consume(context);

            LoggerStub<UserCreatedEventConsumer> logger =
                ServiceActivator.GetScope().ServiceProvider.GetService<ILogger<UserCreatedEventConsumer>>() as LoggerStub<UserCreatedEventConsumer>;

            string checkStart = $"Process {nameof(UserCreatedEvent)} MessageId:{context.MessageId} START";
            string checkEnd = $"Process {nameof(UserCreatedEvent)} MesssageId:{context.MessageId} END";
            Assert.IsTrue(logger.Logs.Any(l => l == checkStart));
            Assert.IsTrue(logger.Logs.Any(l => l == checkEnd));
        }

        #endregion

    }
}
