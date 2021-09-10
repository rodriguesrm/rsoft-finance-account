using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Enum;
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
    /// Create PaymentMethod command handler
    /// </summary>
    public class CreatePaymentMethodCommandHandler : CreateCommandHandlerBase<CreatePaymentMethodCommand, Guid?, PaymentMethod>, IRequestHandler<CreatePaymentMethodCommand, CommandResult<Guid?>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IPaymentMethodDomainService _paymentMethodDomainService;
        private readonly IBusControl _bus;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new handler instance
        /// </summary>
        /// <param name="paymentMethodDomainService">PaymentMethod domain service object</param>
        /// <param name="logger">Logger object</param>
        /// <param name="uow">Unit of work controller object</param>
        /// <param name="bus">Message bus control</param>
        public CreatePaymentMethodCommandHandler(IPaymentMethodDomainService paymentMethodDomainService, ILogger<CreatePaymentMethodCommandHandler> logger, IUnitOfWork uow, IBusControl bus) : base(logger)
        {
            _paymentMethodDomainService = paymentMethodDomainService;
            _uow = uow;
            _bus = bus;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<Guid?>> Handle(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

        #region Overrides
        
        ///<inheritdoc/>
        protected override PaymentMethod PrepareEntity(CreatePaymentMethodCommand request)
        {
            PaymentTypeEnum? paymentType = null;
            if (request.PaymentType.HasValue)
                paymentType = (PaymentTypeEnum)request.PaymentType.Value;
            PaymentMethod entity = new() { Name = request.Name, PaymentType = paymentType };
            return entity;
        }

        ///<inheritdoc/>
        protected override async Task<Guid?> SaveAsync(PaymentMethod entity, CancellationToken cancellationToken)
        {
            entity = await _paymentMethodDomainService.AddAsync(entity, cancellationToken);
            _ = await _uow.SaveChangesAsync(cancellationToken);
            await _bus.Publish(new PaymentMethodCreatedEvent(entity.Id, entity.Name, entity.PaymentType.Value), cancellationToken);
            return entity.Id;
        }

        #endregion

    }
}
