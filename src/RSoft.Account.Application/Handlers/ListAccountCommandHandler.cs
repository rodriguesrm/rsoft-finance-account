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
using RSoft.Account.Application.Handlers.Abstractions;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Lista Account command handler
    /// </summary>
    public class ListAccountCommandHandler : ListCommandHandlerBase<ListAccountCommand, AccountDto, DomainAccount>, IRequestHandler<ListAccountCommand, CommandResult<IEnumerable<AccountDto>>>
    {

        #region Local objects/variables

        private readonly IAccountDomainService _accountDomainService;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new command handler instance
        /// </summary>
        /// <param name="accountDomainService">Account domain/core service</param>
        /// <param name="logger">Logger object</param>
        public ListAccountCommandHandler(IAccountDomainService accountDomainService, ILogger<ListAccountCommandHandler> logger) : base(logger)
        {
            _accountDomainService = accountDomainService;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<IEnumerable<DomainAccount>> GetAllAsync(ListAccountCommand request, CancellationToken cancellationToken)
            => await _accountDomainService.GetAllAsync(cancellationToken);

        ///<inheritdoc/>
        protected override IEnumerable<AccountDto> MapEntities(IEnumerable<DomainAccount> entities)
            => entities.Map();

        #endregion

        #region Handlers

        /// <summary>
        /// Command hanlder
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<IEnumerable<AccountDto>>> Handle(ListAccountCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion
    }

}
