using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Commands;
using RSoft.Lib.Common.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Lib.Common.Models;
using RSoft.Account.Application.Extensions;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Get Category by id command handler
    /// </summary>
    public class GetCategoryByIdCommandHandler : IRequestHandler<GetCategoryByIdCommand, CommandResult<CategoryDto>>
    {

        #region Local objects/variables

        private readonly ICategoryDomainService _categoryDomainService;
        private readonly ILogger<GetCategoryByIdCommandHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new command handler instance
        /// </summary>
        /// <param name="categoryDomainService">Category domain/core service</param>
        /// <param name="logger">Logger object</param>
        public GetCategoryByIdCommandHandler(ICategoryDomainService categoryDomainService, ILogger<GetCategoryByIdCommandHandler> logger)
        {
            _categoryDomainService = categoryDomainService;
            _logger = logger;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Command request data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public Task<CommandResult<CategoryDto>> Handle(GetCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().Name} HANDLER START");
            CommandResult<CategoryDto> result = new();
            Category entity = _categoryDomainService.GetByKeyAsync(request.Id).Result;
            if (entity == null)
            {
                IStringLocalizer<GetCategoryByIdCommandHandler> localizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<GetCategoryByIdCommandHandler>>();
                result.Errors = new List<GenericNotification>() { new GenericNotification("Category", localizer["CATEGORY_NOTFOUND"]) };
            }
            else
            {
                result.Response = entity.Map();
            }
            _logger.LogInformation($"{GetType().Name} HANDLER END");
            return Task.FromResult(result);
        }

        #endregion

    }
}
