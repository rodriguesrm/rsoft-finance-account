using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Contracts.Models;
using EntryAccount = RSoft.Entry.Core.Entities.Entry;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Entry.Application.Extensions;
using RSoft.Lib.Design.Application.Handlers;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// Get Account by id command handler
    /// </summary>
    public class GetAccountByIdCommandHandler : GetByKeyCommandHandlerBase<GetAccountByIdCommand, AccountDto, EntryAccount>, IRequestHandler<GetAccountByIdCommand, CommandResult<AccountDto>>
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
        public GetAccountByIdCommandHandler(IAccountDomainService accountDomainService, ILogger<GetAccountByIdCommandHandler> logger) : base(logger)
        {
            _accountDomainService = accountDomainService;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<EntryAccount> GetEntityByKeyAsync(GetAccountByIdCommand request, CancellationToken cancellationToken)
            => await _accountDomainService.GetByKeyAsync(request.Id, cancellationToken);

        ///<inheritdoc/>
        protected override AccountDto MapEntity(EntryAccount entity)
            => entity.Map();

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Command request data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<AccountDto>> Handle(GetAccountByIdCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

    }
}
