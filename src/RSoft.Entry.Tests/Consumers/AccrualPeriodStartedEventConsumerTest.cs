using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Finance.Contracts.Events;
using RSoft.Lib.Messaging.Contracts;
using System.Threading.Tasks;
using MediatR;
using RSoft.Entry.Contracts.Commands;
using RSoft.Lib.Common.Abstractions;
using System;
using RSoft.Entry.Application.Consumers;
using NUnit.Framework;
using AutoFixture;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace RSoft.Entry.Tests.Consumers
{
    public class AccrualPeriodStartedEventConsumerTest : TestFor<AccrualPeriodStartedEventConsumer>
    {

        #region Tests

        [Test]
        public void ConsumeMessage_ProcessSuccess()
        {
            AccrualPeriodStartedEventConsumer.IsLoaded = true;
            ConsumeContext<AccrualPeriodStartedEvent> context = _fixture.Build<ConsumeContext<AccrualPeriodStartedEvent>>()
                .With(c => c.Message, new AccrualPeriodStartedEvent(DateTime.UtcNow.Year, DateTime.UtcNow.Month))
                .Create();
            _ = Sut.Consume(context);
            //string checkStart = $"{nameof(AccrualPeriodStartedEventConsumer)} START";
            //string checkEnd = $"{nameof(AccrualPeriodStartedEventConsumer)} END";
            //Assert.IsTrue(_logRegister.Any(l => l == checkStart));
            //Assert.IsTrue(_logRegister.Any(l => l == checkEnd));
        }

        #endregion

    }
}
