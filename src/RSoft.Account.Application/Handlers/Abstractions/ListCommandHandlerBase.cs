using Microsoft.Extensions.Logging;
using RSoft.Finance.Contracts.Commands;
using RSoft.Lib.Design.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Application.Handlers.Abstractions
{

    /// <summary>
    /// Lista category command handler
    /// </summary>
    public abstract class ListCommandHandlerBase<TDto, TEntity>
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
        public ListCommandHandlerBase(ILogger logger)
        {
            _logger = logger;
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Get entity by key
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        protected abstract Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Map entity list to result list
        /// </summary>
        /// <param name="entities">Entity list</param>
        protected abstract IEnumerable<TDto> MapEntities(IEnumerable<TEntity> entities);

        #endregion

        #region Handlers

        /// <summary>
        /// Command hanlder
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<IEnumerable<TDto>>> RunHandler(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().Name} START");
            CommandResult<IEnumerable<TDto>> result = new();
            IEnumerable<TEntity> entities = await GetAllAsync(cancellationToken);
            if (entities != null)
            {
                result.Response = MapEntities(entities);
            }
            _logger.LogInformation($"{GetType().Name} END");
            return result;
        }

        #endregion
    }

}
