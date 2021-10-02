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

namespace RSoft.Entry.Application.Consumers
{

    /// <summary>
    /// Consumer event raised when accrual period is started
    /// </summary>
    public class AccrualPeriodStartedEventConsumer : IConsumerEvent<AccrualPeriodStartedEvent>
    {

        #region Local objects/variables

        private readonly ILogger<AccrualPeriodStartedEventConsumer> _logger;
        private readonly IMediator _mediator;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new consumer event instance
        /// </summary>
        public AccrualPeriodStartedEventConsumer()
        {
            _logger = ServiceActivator.GetScope().ServiceProvider.GetService<ILogger<AccrualPeriodStartedEventConsumer>>();
            _mediator = ServiceActivator.GetScope().ServiceProvider.GetService<IMediator>();
        }

        #endregion

        #region Static properties


        /// <summary>
        /// Delayed control flag
        /// </summary>
        public static bool IsLoaded { get; set; } = false;

        #endregion

        #region Consumer methods

        /// <summary>
        /// Consume event handler
        /// </summary>
        /// <param name="context">Consumer context</param>
        public async Task Consume(ConsumeContext<AccrualPeriodStartedEvent> context)
        {

            if (!IsLoaded)
            {
                System.Threading.Thread.Sleep(4000);
                IsLoaded = true;
            }

            try
            {

                _logger.LogInformation($"{nameof(AccrualPeriodStartedEventConsumer)} START");

                RegisterStartAccrualPeriodCommand command = new()
                {
                    Year = context.Message.Year,
                    Month = context.Message.Month
                };
                _ = await _mediator.Send(command);

                //BACKLOG: Handle exceptions

                _logger.LogInformation($"{nameof(AccrualPeriodStartedEventConsumer)} END");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fail on process message {context.MessageId}");
                await context.Publish(context.MessageId);
            }

        }

        #endregion

    }
}
