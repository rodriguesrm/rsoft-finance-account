using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Application.Extensions;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using DomainAccount = RSoft.Account.Core.Entities.Account;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Lista Account command handler
    /// </summary>
    public class ListAccountCommandHandler : IRequestHandler<ListAccountCommand, CommandResult<IEnumerable<AccountDto>>>
    {

        #region Local objects/variables

        private readonly IAccountDomainService _accountDomainService;
        private readonly ILogger<ListAccountCommandHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new command handler instance
        /// </summary>
        /// <param name="accountDomainService">Account domain/core service</param>
        /// <param name="logger">Logger object</param>
        public ListAccountCommandHandler(IAccountDomainService accountDomainService, ILogger<ListAccountCommandHandler> logger)
        {
            _accountDomainService = accountDomainService;
            _logger = logger;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command hanlder
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public Task<CommandResult<IEnumerable<AccountDto>>> Handle(ListAccountCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().Name} START");
            CommandResult<IEnumerable<AccountDto>> result = new();
            IEnumerable<DomainAccount> entities = _accountDomainService.GetAllAsync(cancellationToken).Result;
            if (entities != null)
            {
                result.Response = entities.Map();
            }
            _logger.LogInformation($"{GetType().Name} END");
            return Task.FromResult(result);
        }

        #endregion
    }

}
