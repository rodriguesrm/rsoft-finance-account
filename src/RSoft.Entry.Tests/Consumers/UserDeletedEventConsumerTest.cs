using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Lib.Common.Abstractions;
using System;
using NUnit.Framework;
using AutoFixture;
using System.Linq;
using RSoft.Entry.Tests.Stubs;
using RSoft.Entry.WorkerService.Consumers;
using RSoft.Lib.Contracts.Events;

namespace RSoft.Entry.Tests.Consumers
{

    public class UserDeletedEventConsumerTest : TestFor<UserDeletedEventConsumer>
    {

        #region Tests

        [Test]
        public void ConsumeMessage_ProcessSuccess()
        {
            ConsumeContext<UserDeletedEvent> context = _fixture.Build<ConsumeContextStub<UserDeletedEvent>>()
                .With(c => c.Message, new UserDeletedEvent(Guid.NewGuid()))
                .Create();
            _ = Target.Consume(context);

            LoggerStub<UserDeletedEventConsumer> logger =
                ServiceActivator.GetScope().ServiceProvider.GetService<ILogger<UserDeletedEventConsumer>>() as LoggerStub<UserDeletedEventConsumer>;

            string checkStart = $"Process {nameof(UserDeletedEvent)} MessageId:{context.MessageId} START";
            string checkEnd = $"Process {nameof(UserDeletedEvent)} MesssageId:{context.MessageId} END";
            Assert.IsTrue(logger.Logs.Any(l => l == checkStart));
            Assert.IsTrue(logger.Logs.Any(l => l == checkEnd));
        }

        #endregion

    }
}
