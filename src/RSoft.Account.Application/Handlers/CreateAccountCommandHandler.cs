using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Application.Extensions;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Commands;
using RSoft.Lib.Design.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Create account command handler
    /// </summary>
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CommandResult<Guid?>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IAccountDomainService _accountDomainService;
        private readonly ILogger<CreateAccountCommandHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a command handler object instance
        /// </summary>
        /// <param name="accountDomainService">Account domain service object</param>
        /// <param name="logger">Logger object</param>
        /// <param name="uow">Unit of work controller object</param>
        public CreateAccountCommandHandler(IUnitOfWork uow, IAccountDomainService accountDomainService, ILogger<CreateAccountCommandHandler> logger)
        {
            _uow = uow;
            _accountDomainService = accountDomainService;
            _logger = logger;
        }

        #endregion

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<Guid?>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().Name} START");
            CommandResult<Guid?> result = new();
            Core.Entities.Account entity = new();
            entity.Name = request.Name;
            if (request.CategoryId.HasValue)
                entity.Category = new(request.CategoryId.Value);
            entity.Validate();
            if (entity.Valid)
            {
                entity = await _accountDomainService.AddAsync(entity, cancellationToken);
                _ = await _uow.SaveChangesAsync();
                result.Response = entity.Id;
            }
            else
            {
                result.Errors = entity.Notifications.ToGenericNotifications();
            }
            _logger.LogInformation($"{GetType().Name} END");
            return result;
        }
    }
}
