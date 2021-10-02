using FluentValidator;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Application.Extensions;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Finance.Contracts.Events;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Application.Handlers;
using RSoft.Lib.Design.Infra.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// Create Transaction command handler
    /// </summary>
    public class RollbackTransactionCommandHandler : CreateCommandHandlerBase<RollbackTransactionCommand, Guid?, Transaction>, IRequestHandler<RollbackTransactionCommand, CommandResult<Guid?>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly ITransactionDomainService _transactionDomainService;
        private readonly IBusControl _bus;
        private readonly IStringLocalizer<RollbackTransactionCommandHandler> _localizer;

        #endregion

        #region Constructors

        /// <summary>
        /// Undo command handler instance
        /// </summary>
        /// <param name="uow">Unit of work controller object</param>
        /// <param name="transactionDomainService">Transaction domain service</param>
        /// <param name="bus">Messaging bus control</param>
        /// <param name="localizer">String localizer object</param>
        /// <param name="logger">Logger object</param>
        public RollbackTransactionCommandHandler
        (
            IUnitOfWork uow,
            ITransactionDomainService transactionDomainService,
            IBusControl bus,
            IStringLocalizer<RollbackTransactionCommandHandler> localizer,
            ILogger<RollbackTransactionCommandHandler> logger
        ) : base(logger)
        {
            _uow = uow;
            _transactionDomainService = transactionDomainService;
            _localizer = localizer;
            _bus = bus;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<Guid?>> Handle(RollbackTransactionCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override Transaction PrepareEntity(RollbackTransactionCommand request)
        {
            Transaction originalTransaction = _transactionDomainService.GetByKeyAsync(request.TransactionId).Result;
            if (originalTransaction == null)
            {
                originalTransaction.AddNotification(new Notification(nameof(Transaction), _localizer["TRANSACTION_NOT_FOUND"]));
                return originalTransaction;
            }
            Transaction undoTransaction = originalTransaction.Map(request);
            return undoTransaction;
        }

        ///<inheritdoc/>
        protected override async Task<Guid?> SaveAsync(Transaction entity, CancellationToken cancellationToken)
        {
            TransactionCreatedEvent transactionUndodEvent = entity.MapToEvent();
            entity = await _transactionDomainService.AddAsync(entity, cancellationToken);
            _ = await _uow.SaveChangesAsync(cancellationToken);
            await _bus.Publish(transactionUndodEvent, cancellationToken);
            return entity.Id;
        }

        #endregion

    }
}
