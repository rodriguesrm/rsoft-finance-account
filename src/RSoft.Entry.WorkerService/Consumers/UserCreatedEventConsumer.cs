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
    /// Consumer event raised when user is created
    /// </summary>
    public class UserCreatedEventConsumer : IConsumerEvent<UserCreatedEvent>
    {

        #region Local objects/variables

        private readonly ILogger<UserCreatedEventConsumer> _logger;
        private readonly IMediator _mediator;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new consumer event instance
        /// </summary>
        public UserCreatedEventConsumer()
        {
            _logger = ServiceActivator.GetScope().ServiceProvider.GetService<ILogger<UserCreatedEventConsumer>>();
            _mediator = ServiceActivator.GetScope().ServiceProvider.GetService<IMediator>();
        }

        #endregion

        #region Consumer methods

        /// <summary>
        /// Consume UserCreatedEvent handler
        /// </summary>
        /// <param name="context">Consumer context</param>
        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {

            _logger.LogInformation($"Process {nameof(UserCreatedEvent)} MessageId:{context.MessageId} START", JsonSerializer.Serialize(context.Message));

            CreateOrUpdateUserCommand command = new
            (
                true,
                context.Message.Id, 
                context.Message.FirstName, 
                context.Message.LastName, 
                context.Message.IsActive
            );
            _ = await _mediator.Send(command);

            _logger.LogInformation($"Process {nameof(UserCreatedEvent)} MesssageId:{context.MessageId} END");

        }

        #endregion

    }

}
