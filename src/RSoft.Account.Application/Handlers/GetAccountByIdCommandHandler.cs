using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using DomainAccount = RSoft.Account.Core.Entities.Account;
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
    /// Get Account by id command handler
    /// </summary>
    public class GetAccountByIdCommandHandler : IRequestHandler<GetAccountByIdCommand, CommandResult<AccountDto>>
    {

        #region Local objects/variables

        private readonly IAccountDomainService _accountDomainService;
        private readonly ILogger<GetAccountByIdCommandHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new command handler instance
        /// </summary>
        /// <param name="accountDomainService">Account domain/core service</param>
        /// <param name="logger">Logger object</param>
        public GetAccountByIdCommandHandler(IAccountDomainService accountDomainService, ILogger<GetAccountByIdCommandHandler> logger)
        {
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
        public Task<CommandResult<AccountDto>> Handle(GetAccountByIdCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().Name} START");
            CommandResult<AccountDto> result = new();
            DomainAccount entity = _accountDomainService.GetByKeyAsync(request.Id,cancellationToken).Result;
            if (entity == null)
            {
                IStringLocalizer<GetAccountByIdCommandHandler> localizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<GetAccountByIdCommandHandler>>();
                result.Errors = new List<GenericNotification>() { new GenericNotification("Account", localizer["Account_NOTFOUND"]) };
            }
            else
            {
                result.Response = entity.Map();
            }
            _logger.LogInformation($"{GetType().Name} END");
            return Task.FromResult(result);
        }

        #endregion

    }
}
