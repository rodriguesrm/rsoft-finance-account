using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RSoft.Finance.Contracts.Commands;
using RSoft.Lib.Common.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Lib.Common.Models;
using RSoft.Lib.Design.Domain.Entities;

namespace RSoft.Account.Application.Handlers.Abstractions
{

    /// <summary>
    /// Get entity by id command handler abstract base
    /// </summary>
    public abstract class GetByKeyCommandHandlerBase<TGetCommand, TResult, TEntity>
        where TGetCommand : IRequest<CommandResult<TResult>>
        where TEntity : EntityBase<TEntity>
    {

        #region Local objects/variables

        private readonly ILogger _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new command handler instance
        /// </summary>
        /// <param name="logger">Logger object</param>
        public GetByKeyCommandHandlerBase(ILogger logger)
        {
            _logger = logger;
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Get entity by key
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        protected abstract Task<TEntity> GetEntityByKeyAsync(TGetCommand request, CancellationToken cancellationToken);

        /// <summary>
        /// Map entity to result
        /// </summary>
        /// <param name="entity"></param>
        protected abstract TResult MapEntity(TEntity entity);

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Command request data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<TResult>> RunHandler(TGetCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().Name} START");
            CommandResult<TResult> result = new();
            TEntity entity = await GetEntityByKeyAsync(request, cancellationToken);
            if (entity == null)
            {
                IStringLocalizer<SharedResource> localizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<SharedResource>>();
                result.Errors = new List<GenericNotification>() { new GenericNotification(nameof(TEntity), localizer["ENTITY_NOTFOUND"]) };
            }
            else
            {
                result.Response = MapEntity(entity);
            }
            _logger.LogInformation($"{GetType().Name} END");
            return result;
        }

        #endregion

    }
}
