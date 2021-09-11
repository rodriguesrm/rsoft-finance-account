using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Application.Extensions;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Events;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Application.Handlers;
using RSoft.Lib.Design.Infra.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Create Transaction command handler
    /// </summary>
    public class CreateTransactionCommandHandler : CreateCommandHandlerBase<CreateTransactionCommand, Guid?, Transaction>, IRequestHandler<CreateTransactionCommand, CommandResult<Guid?>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly ITransactionDomainService _transactionDomainService;
        private readonly IBusControl _bus;

        #endregion

        #region Constructors

        /// <summary>
        /// Create command handler instance
        /// </summary>
        /// <param name="uow">Unit of work controller object</param>
        /// <param name="transactionDomainService">Transaction domain service</param>
        /// <param name="bus">Messaging bus control</param>
        /// <param name="logger">Logger object</param>
        public CreateTransactionCommandHandler
        (
            IUnitOfWork uow, 
            ITransactionDomainService transactionDomainService, 
            IBusControl bus, 
            ILogger<CreateTransactionCommandHandler> logger
        ) : base(logger)
        {
            _uow = uow;
            _transactionDomainService = transactionDomainService;
            _bus = bus;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<Guid?>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override Transaction PrepareEntity(CreateTransactionCommand request)
            => request.Map();

        ///<inheritdoc/>
        protected override async Task<Guid?> SaveAsync(Transaction entity, CancellationToken cancellationToken)
        {
            TransactionCreatedEvent transactionCreatedEvent = entity.MapToEvent();
            entity = await _transactionDomainService.AddAsync(entity, cancellationToken);
            _ = await _uow.SaveChangesAsync(cancellationToken);
            await _bus.Publish(transactionCreatedEvent, cancellationToken);
            return entity.Id;
        }

        #endregion

    }
}
