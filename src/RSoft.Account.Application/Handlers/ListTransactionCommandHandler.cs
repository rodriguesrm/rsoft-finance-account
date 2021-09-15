using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Application.Extensions;
using RSoft.Lib.Design.Application.Handlers;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Account.Application.Arguments;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// List Transaction command handler
    /// </summary>
    public class ListTransactionCommandHandler : ListCommandHandlerBase<ListTransactionCommand, TransactionDto, Transaction>, IRequestHandler<ListTransactionCommand, CommandResult<IEnumerable<TransactionDto>>>
    {

        #region Local objects/variables

        private readonly ITransactionDomainService _TransactionDomainService;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new command handler instance
        /// </summary>
        /// <param name="TransactionDomainService">Transaction domain/core service</param>
        /// <param name="logger">Logger object</param>
        public ListTransactionCommandHandler(ITransactionDomainService TransactionDomainService, ILogger<ListTransactionCommandHandler> logger) : base(logger)
        {
            _TransactionDomainService = TransactionDomainService;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<IEnumerable<Transaction>> GetAllAsync(ListTransactionCommand request, CancellationToken cancellationToken)
        {
            ListTransactionFilter filter = request.Map();
            IEnumerable<Transaction> result = await _TransactionDomainService.GetByFilterAsync(filter, cancellationToken);
            return result;
        }

        ///<inheritdoc/>
        protected override IEnumerable<TransactionDto> MapEntities(IEnumerable<Transaction> entities)
            => entities.Map();

        #endregion

        #region Handlers

        /// <summary>
        /// Command hanlder
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<IEnumerable<TransactionDto>>> Handle(ListTransactionCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion
    }

}
