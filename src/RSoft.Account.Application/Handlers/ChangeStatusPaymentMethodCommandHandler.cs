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
        private readonly ILogger<CreatePaymentMethodCommandHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a handler instance
        /// </summary>
        /// <param name="uow">Unit of work controller instance</param>
        /// <param name="paymentMethodDomainService">PaymentMethod domain/core service</param>
        /// <param name="logger">Logger object</param>
        public ChangeStatusPaymentMethodCommandHandler(IUnitOfWork uow, IPaymentMethodDomainService paymentMethodDomainService, ILogger<CreatePaymentMethodCommandHandler> logger) : base(logger)
        {
            _uow = uow;
            _paymentMethodDomainService = paymentMethodDomainService;
            _logger = logger;
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
