using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Application.Extensions;
using RSoft.Lib.Design.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Contracts.Models;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Entry.Application.Arguments;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// List Transaction command handler
    /// </summary>
    public class ListTransactionCommandHandler : ListCommandHandlerBase<ListTransactionCommand, TransactionDto, Transaction>, IRequestHandler<ListTransactionCommand, CommandResult<IEnumerable<TransactionDto>>>
    {

        #region Local objects/variables

        private readonly ITransactionDomainService _transactionDomainService;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new command handler instance
        /// </summary>
        /// <param name="transactionDomainService">Transaction domain/core service</param>
        /// <param name="logger">Logger object</param>
        public ListTransactionCommandHandler(ITransactionDomainService transactionDomainService, ILogger<ListTransactionCommandHandler> logger) : base(logger)
        {
            _transactionDomainService = transactionDomainService;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<IEnumerable<Transaction>> GetAllAsync(ListTransactionCommand request, CancellationToken cancellationToken)
        {
            ListTransactionFilter filter = request.Map();
            IEnumerable<Transaction> result = await _transactionDomainService.GetByFilterAsync(filter, cancellationToken);
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
