using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Application.Extensions;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Commands;
using RSoft.Lib.Design.Infra.Data;
using System.Threading;
using System.Threading.Tasks;

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
        public Task<CommandResult<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().Name} HANDLER START");
            CommandResult<bool> result = new();
            Category entity = new(request.Id) { Name = request.Name };
            entity = _categoryDomainService.Update(entity.Id, entity);
            if (entity.Valid)
            {
                _ = _uow.SaveChangesAsync();
                result.Response = true;
            }
            else
            {
                result.Errors = entity.Notifications.ToGenericNotifications();
            }
            _logger.LogInformation($"{GetType().Name} HANDLER END");
            return Task.FromResult(result);
        }

        #endregion

    }
}
