using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RSoft.Account.Application.Extensions;
using RSoft.Finance.Contracts.Commands;
using RSoft.Lib.Common.Abstractions;
using RSoft.Lib.Common.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Lib.Design.Domain.Entities;
using System;

namespace RSoft.Account.Application.Handlers.Abstractions
{

    /// <summary>
    /// Create category command handler
    /// </summary>
    public abstract class UpdateCommandHandlerBase<TUpdateCommand, TResult, TEntity>
        where TUpdateCommand : IRequest<CommandResult<TResult>>
        where TEntity : EntityBase<TEntity>
    {

        #region Local objects/variables

        private readonly ILogger _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new handler instance
        /// </summary>
        /// <param name="logger">Logger object</param>
        public UpdateCommandHandlerBase(ILogger logger)
        {
            _logger = logger;
        }

        #endregion

        #region Abstracts methods

        /// <summary>
        /// Get entity by key
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        protected abstract Task<TEntity> GetEntityByKeyAsync(TUpdateCommand request, CancellationToken cancellationToken);

        /// <summary>
        /// Prepare entity to create mapping data
        /// </summary>
        /// <param name="request">Rquest command data</param>
        /// <param name="entity">Entity instance</param>
        protected abstract void PrepareEntity(TUpdateCommand request, TEntity entity);

        /// <summary>
        /// Perform save (update) entity in context
        /// </summary>
        /// <param name="entity">Entity to save</param>
        /// <param name="cancellationToken">Cancellation token</param>
        protected abstract Task<TResult> SaveAsync(TEntity entity, CancellationToken cancellationToken);

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="additionalValidationsAction">Additional validations to be applied to the entity</param>
        protected virtual async Task<CommandResult<TResult>> RunHandler
        (
            TUpdateCommand request, 
            CancellationToken cancellationToken,
            Action<TUpdateCommand, TEntity> additionalValidationsAction = null
        )
        {
            _logger.LogInformation($"{GetType().Name} START");
            CommandResult<TResult> result = new();
            TEntity entity = await GetEntityByKeyAsync(request, cancellationToken);
            if (entity == null)
            {
                IStringLocalizer<UpdateCategoryCommandHandler> localizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<UpdateCategoryCommandHandler>>();
                result.Errors = new List<GenericNotification>() { new GenericNotification("Category", localizer["CATEGORY_NOTFOUND"]) };
            }
            else
            {
                PrepareEntity(request, entity);
                entity.Validate();
                additionalValidationsAction?.Invoke(request, entity);
                if (entity.Valid)
                    result.Response = await SaveAsync(entity, cancellationToken);
                else
                    result.Errors = entity.Notifications.ToGenericNotifications();
            }
            _logger.LogInformation($"{GetType().Name} END");
            return result;
        }

        #endregion

    }
}
