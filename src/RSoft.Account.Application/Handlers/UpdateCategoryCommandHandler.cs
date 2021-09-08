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
    /// Create category command handler
    /// </summary>
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly ICategoryDomainService _categoryDomainService;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new handler instance
        /// </summary>
        /// <param name="categoryDomainService">Category domain service object</param>
        /// <param name="uow">Unit of work controller object</param>
        /// <param name="logger">Logger object</param>
        public UpdateCategoryCommandHandler(ICategoryDomainService categoryDomainService, IUnitOfWork uow, ILogger<CreateCategoryCommandHandler> logger)
        {
            _categoryDomainService = categoryDomainService;
            _uow = uow;
            _logger = logger;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().Name} START");
            CommandResult<bool> result = new();
            Category entity = await _categoryDomainService.GetByKeyAsync(request.Id);
            if (entity == null)
            {
                IStringLocalizer<UpdateCategoryCommandHandler> localizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<UpdateCategoryCommandHandler>>();
                result.Errors = new List<GenericNotification>() { new GenericNotification("Category", localizer["CATEGORY_NOTFOUND"]) };
            }
            else
            {
                entity.Name = request.Name;
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
