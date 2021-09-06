using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Application.Extensions;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Commands;
using RSoft.Lib.Design.Infra.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Create category command handler
    /// </summary>
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CommandResult<Guid?>>
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
        /// <param name="logger">Logger object</param>
        /// <param name="uow">Unit of work controller object</param>
        public CreateCategoryCommandHandler(ICategoryDomainService categoryDomainService, ILogger<CreateCategoryCommandHandler> logger, IUnitOfWork uow)
        {
            _categoryDomainService = categoryDomainService;
            _logger = logger;
            _uow = uow;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<Guid?>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().Name} HANDLER START");
            CommandResult<Guid?> result = new();
            Category entity = new() { Name = request.Name };
            entity = await _categoryDomainService.AddAsync(entity);
            if (entity.Valid)
            {
                _ = await _uow.SaveChangesAsync();
                result.Response = entity.Id;
            }
            else
            {
                result.Errors = entity.Notifications.ToGenericNotifications();
            }
            _logger.LogInformation($"{GetType().Name} HANDLER END");
            return result;
        }

        #endregion

    }
}
