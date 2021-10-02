using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Infra.Data;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Lib.Design.Application.Handlers;
using MassTransit;
using RSoft.Finance.Contracts.Events;
using RSoft.Finance.Contracts.Enum;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// Create PaymentMethod command handler
    /// </summary>
    public class UpdatePaymentMethodCommandHandler : UpdateCommandHandlerBase<UpdatePaymentMethodCommand, bool, PaymentMethod>, IRequestHandler<UpdatePaymentMethodCommand, CommandResult<bool>>
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
        /// <param name="PaymentMethodDomainService">PaymentMethod domain service object</param>
        /// <param name="uow">Unit of work controller object</param>
        /// <param name="logger">Logger object</param>
        /// <param name="bus">Messaging bus control</param>
        public UpdatePaymentMethodCommandHandler(IPaymentMethodDomainService PaymentMethodDomainService, IUnitOfWork uow, ILogger<CreatePaymentMethodCommandHandler> logger, IBusControl bus) : base(logger)
        {
            _paymentMethodDomainService = PaymentMethodDomainService;
            _uow = uow;
            _bus = bus;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<PaymentMethod> GetEntityByKeyAsync(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
            => await _paymentMethodDomainService.GetByKeyAsync(request.Id, cancellationToken);

        ///<inheritdoc/>
        protected override void PrepareEntity(UpdatePaymentMethodCommand request, PaymentMethod entity)
        {
            entity.Name = request.Name;
            if (request.PaymentType.HasValue)
                entity.PaymentType = (PaymentTypeEnum)request.PaymentType.Value;
        }

        ///<inheritdoc/>
        protected override async Task<bool> SaveAsync(PaymentMethod entity, CancellationToken cancellationToken)
        {
            _ = _paymentMethodDomainService.Update(entity.Id, entity);
            _ = await _uow.SaveChangesAsync(cancellationToken);
            await _bus.Publish(new PaymentMethodChangedEvent(entity.Id, entity.Name, (int)entity.PaymentType), cancellationToken);
            return true;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<bool>> Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

    }
}
