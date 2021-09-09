using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Domain.Enum;
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
        private readonly IPaymentMethodDomainService _PaymentMethodDomainService;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new handler instance
        /// </summary>
        /// <param name="PaymentMethodDomainService">PaymentMethod domain service object</param>
        /// <param name="logger">Logger object</param>
        /// <param name="uow">Unit of work controller object</param>
        public CreatePaymentMethodCommandHandler(IPaymentMethodDomainService PaymentMethodDomainService, ILogger<CreatePaymentMethodCommandHandler> logger, IUnitOfWork uow) : base(logger)
        {
            _PaymentMethodDomainService = PaymentMethodDomainService;
            _uow = uow;
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
            entity = await _PaymentMethodDomainService.AddAsync(entity, cancellationToken);
            _ = await _uow.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        #endregion

    }
}
