using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RSoft.Account.Application.Extensions;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Commands;
using RSoft.Lib.Common.Abstractions;
using RSoft.Lib.Common.Models;
using RSoft.Lib.Design.Infra.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Change status category command handler
    /// </summary>
    public class ChangeStatusCategoryCommandHandler : IRequestHandler<ChangeStatusCategoryCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly ICategoryDomainService _categoryDomainService;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a handler instance
        /// </summary>
        /// <param name="uow">Unit of work controller instance</param>
        /// <param name="categoryDomainService">Category domain/core service</param>
        /// <param name="logger">Logger object</param>
        public ChangeStatusCategoryCommandHandler(IUnitOfWork uow, ICategoryDomainService categoryDomainService, ILogger<CreateCategoryCommandHandler> logger)
        {
            _uow = uow;
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
        public async Task<CommandResult<bool>> Handle(ChangeStatusCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().Name} START");
            CommandResult<bool> result = new();
            Category entity = await _categoryDomainService.GetByKeyAsync(request.Id);
            if (entity == null)
            {
                IStringLocalizer<ChangeStatusCategoryCommandHandler> localizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<ChangeStatusCategoryCommandHandler>>();
                result.Errors = new List<GenericNotification>() { new GenericNotification("Category", localizer["CATEGORY_NOTFOUND"]) };
            }
            else
            {
                entity.IsActive = request.IsActive;
                entity.Validate();
                if (entity.Valid)
                {
                    _ = _categoryDomainService.Update(entity.Id, entity);
                    _ = await _uow.SaveChangesAsync();
                    result.Response = true;
                }
                else
                {
                    result.Errors = entity.Notifications.ToGenericNotifications();
                }
            }
            _logger.LogInformation($"{GetType().Name} END");
            return result;
        }

        #endregion

    }
}
