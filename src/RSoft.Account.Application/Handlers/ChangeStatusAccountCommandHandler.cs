using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RSoft.Account.Application.Extensions;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Commands;
using RSoft.Lib.Common.Abstractions;
using RSoft.Lib.Common.Models;
using RSoft.Lib.Design.Infra.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DomainAccount = RSoft.Account.Core.Entities.Account;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Change status Account command handler
    /// </summary>
    public class ChangeStatusAccountCommandHandler : IRequestHandler<ChangeStatusAccountCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IAccountDomainService _accountDomainService;
        private readonly ILogger<CreateAccountCommandHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a handler instance
        /// </summary>
        /// <param name="uow">Unit of work controller instance</param>
        /// <param name="accountDomainService">Account domain/core service</param>
        /// <param name="logger">Logger object</param>
        public ChangeStatusAccountCommandHandler(IUnitOfWork uow, IAccountDomainService accountDomainService, ILogger<CreateAccountCommandHandler> logger)
        {
            _uow = uow;
            _accountDomainService = accountDomainService;
            _logger = logger;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Command request data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public Task<CommandResult<bool>> Handle(ChangeStatusAccountCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().Name} START");
            CommandResult<bool> result = new();
            DomainAccount entity = _accountDomainService.GetByKeyAsync(request.Id, cancellationToken).Result;
            if (entity == null)
            {
                IStringLocalizer<ChangeStatusAccountCommandHandler> localizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<ChangeStatusAccountCommandHandler>>();
                result.Errors = new List<GenericNotification>() { new GenericNotification("Account", localizer["ACCOUNT_NOTFOUND"]) };
            }
            else
            {
                entity.IsActive = request.IsActive;
                entity.Validate();
                if (entity.Valid)
                {
                    _ = _accountDomainService.Update(entity.Id, entity);
                    _ = _uow.SaveChanges();
                    result.Response = true;
                }
                else
                {
                    result.Errors = entity.Notifications.ToGenericNotifications();
                }
            }
            _logger.LogInformation($"{GetType().Name} END");
            return Task.FromResult(result);
        }

        #endregion

    }
}
