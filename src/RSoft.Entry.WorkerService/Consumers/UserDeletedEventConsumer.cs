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

namespace RSoft.Entry.WorkerService.Consumers
{

    /// <summary>
    /// Consumer event raised when user is deleted
    /// </summary>
    public class UserDeletedEventConsumer : IConsumerEvent<UserDeletedEvent>
    {

        #region Local objects/variables

        private readonly ILogger<UserDeletedEventConsumer> _logger;
        private readonly IMediator _mediator;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new consumer event instance
        /// </summary>
        public UserDeletedEventConsumer()
        {
            _logger = ServiceActivator.GetScope().ServiceProvider.GetService<ILogger<UserDeletedEventConsumer>>();
            _mediator = ServiceActivator.GetScope().ServiceProvider.GetService<IMediator>();
        }

        #endregion

        #region Consumer methods

        /// <summary>
        /// Consume UserDeletedEvent handler
        /// </summary>
        /// <param name="context">Consumer context</param>
        public async Task Consume(ConsumeContext<UserDeletedEvent> context)
        {

            _logger.LogInformation($"Process {nameof(UserChangedEvent)} MessageId:{context.MessageId} START", JsonSerializer.Serialize(context.Message));

            DeleteUserCommand command = new(context.Message.Id);
            _ = await _mediator.Send(command);

            _logger.LogInformation($"Process {nameof(UserChangedEvent)} MesssageId:{context.MessageId} END");

        }

        #endregion

    }

}
