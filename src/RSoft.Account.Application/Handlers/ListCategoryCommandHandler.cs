using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Application.Extensions;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Lista category command handler
    /// </summary>
    public class ListCategoryCommandHandler : IRequestHandler<ListCategoryCommand, CommandResult<IEnumerable<CategoryDto>>>
    {

        #region Local objects/variables

        private readonly ICategoryDomainService _categoryDomainService;
        private readonly ILogger<ListCategoryCommandHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new command handler instance
        /// </summary>
        /// <param name="categoryDomainService">Category domain/core service</param>
        /// <param name="logger">Logger object</param>
        public ListCategoryCommandHandler(ICategoryDomainService categoryDomainService, ILogger<ListCategoryCommandHandler> logger)
        {
            _categoryDomainService = categoryDomainService;
            _logger = logger;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command hanlder
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public Task<CommandResult<IEnumerable<CategoryDto>>> Handle(ListCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().Name} HANDLER START");
            CommandResult<IEnumerable<CategoryDto>> result = new();
            IEnumerable<Category> entities = _categoryDomainService.GetAllAsync().Result;
            if (entities != null)
            {
                result.Response = entities.Map();
            }
            _logger.LogInformation($"{GetType().Name} HANDLER END");
            return Task.FromResult(result);
        }

        #endregion
    }

}
