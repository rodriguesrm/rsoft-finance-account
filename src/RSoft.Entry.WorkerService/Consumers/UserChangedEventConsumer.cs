using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Lib.Messaging.Contracts;
using System.Threading.Tasks;
using MediatR;
using RSoft.Entry.Contracts.Commands;
using RSoft.Lib.Common.Abstractions;
using RSoft.Lib.Contracts.Events;
using System.Text.Json;
using RSoft.Entry.WorkerService.Extensions;
using System;

namespace RSoft.Entry.WorkerService.Consumers
{

    /// <summary>
    /// Consumer event raised when user is changed
    /// </summary>
    public class UserChangedEventConsumer : IConsumerEvent<UserChangedEvent>
    {

        #region Local objects/variables

        private readonly ILogger<UserChangedEventConsumer> _logger;
        private readonly IMediator _mediator;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new consumer event instance
        /// </summary>
        public UserChangedEventConsumer()
        {
            _logger = ServiceActivator.GetScope().ServiceProvider.GetService<ILogger<UserChangedEventConsumer>>();
            _mediator = ServiceActivator.GetScope().ServiceProvider.GetService<IMediator>();
        }

        #endregion

        #region Consumer methods

        /// <summary>
        /// Consume UserChangedEvent handler
        /// </summary>
        /// <param name="context">Consumer context</param>
        public async Task Consume(ConsumeContext<UserChangedEvent> context)
        {

            _logger.LogInformation($"Process {nameof(UserChangedEvent)} MessageId:{context.MessageId} START", JsonSerializer.Serialize(context.Message));

            CreateOrUpdateUserCommand command = new
            (
                false,
                context.Message.Id,
                context.Message.FirstName,
                context.Message.LastName,
                context.Message.IsActive
            );
            _ = await _mediator.Send(command);

            _logger.LogInformation($"Process {nameof(UserChangedEvent)} MesssageId:{context.MessageId} END");

        }

        #endregion

    }

}
