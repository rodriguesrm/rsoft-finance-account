using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Contracts.Models;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Entry.Application.Extensions;
using RSoft.Lib.Design.Application.Handlers;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// Get Transaction by id command handler
    /// </summary>
    public class GetTransactionByIdCommandHandler : GetByKeyCommandHandlerBase<GetTransactionByIdCommand, TransactionDto, Transaction>, IRequestHandler<GetTransactionByIdCommand, CommandResult<TransactionDto>>
    {

        #region Local objects/variables

        private readonly ITransactionDomainService _transactionDomainService;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new command handler instance
        /// </summary>
        /// <param name="TransactionDomainService">Transaction domain/core service</param>
        /// <param name="logger">Logger object</param>
        public GetTransactionByIdCommandHandler(ITransactionDomainService TransactionDomainService, ILogger<GetTransactionByIdCommandHandler> logger) : base(logger)
        {
            _transactionDomainService = TransactionDomainService;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<Transaction> GetEntityByKeyAsync(GetTransactionByIdCommand request, CancellationToken cancellationToken)
            => await _transactionDomainService.GetByKeyAsync(request.Id, cancellationToken);

        ///<inheritdoc/>
        protected override TransactionDto MapEntity(Transaction entity)
            => entity.Map();

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Command request data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<TransactionDto>> Handle(GetTransactionByIdCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

    }
}
