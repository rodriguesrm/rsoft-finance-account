using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Infra.Data;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Lib.Design.Application.Handlers;
using RSoft.Finance.Contracts.Events;
using MassTransit;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Change status PaymentMethod command handler
    /// </summary>
    public class ChangeStatusPaymentMethodCommandHandler : UpdateCommandHandlerBase<ChangeStatusPaymentMethodCommand, bool, PaymentMethod>, IRequestHandler<ChangeStatusPaymentMethodCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IPaymentMethodDomainService _paymentMethodDomainService;
        private readonly IBusControl _bus;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a handler instance
        /// </summary>
        /// <param name="uow">Unit of work controller instance</param>
        /// <param name="paymentMethodDomainService">PaymentMethod domain/core service</param>
        /// <param name="logger">Logger object</param>
        /// <param name="bus">Messaging bus control</param>
        public ChangeStatusPaymentMethodCommandHandler(IUnitOfWork uow, IPaymentMethodDomainService paymentMethodDomainService, ILogger<CreatePaymentMethodCommandHandler> logger, IBusControl bus) : base(logger)
        {
            _uow = uow;
            _paymentMethodDomainService = paymentMethodDomainService;
            _bus = bus;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<PaymentMethod> GetEntityByKeyAsync(ChangeStatusPaymentMethodCommand request, CancellationToken cancellationToken)
            => await _paymentMethodDomainService.GetByKeyAsync(request.Id, cancellationToken);

        ///<inheritdoc/>
        protected override void PrepareEntity(ChangeStatusPaymentMethodCommand request, PaymentMethod entity)
        {
            entity.IsActive = request.IsActive;
        }

        ///<inheritdoc/>
        protected override async Task<bool> SaveAsync(PaymentMethod entity, CancellationToken cancellationToken)
        {
            _ = _paymentMethodDomainService.Update(entity.Id, entity);
            _ = await _uow.SaveChangesAsync();
            await _bus.Publish(new PaymentMethodStatusChangedEvent(entity.Id, entity.IsActive));
            return true;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Command request data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<bool>> Handle(ChangeStatusPaymentMethodCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

    }
}
