using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RSoft.Account.Application.Extensions;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Common.Abstractions;
using RSoft.Lib.Common.Models;
using RSoft.Lib.Design.Infra.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Finance.Domain.Enum;
using RSoft.Lib.Design.Application.Handlers;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Create PaymentMethod command handler
    /// </summary>
    public class UpdatePaymentMethodCommandHandler : UpdateCommandHandlerBase<UpdatePaymentMethodCommand, bool, PaymentMethod>, IRequestHandler<UpdatePaymentMethodCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IPaymentMethodDomainService _paymentMethodDomainService;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new handler instance
        /// </summary>
        /// <param name="PaymentMethodDomainService">PaymentMethod domain service object</param>
        /// <param name="uow">Unit of work controller object</param>
        /// <param name="logger">Logger object</param>
        public UpdatePaymentMethodCommandHandler(IPaymentMethodDomainService PaymentMethodDomainService, IUnitOfWork uow, ILogger<CreatePaymentMethodCommandHandler> logger) : base(logger)
        {
            _paymentMethodDomainService = PaymentMethodDomainService;
            _uow = uow;
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
        protected override Task<bool> SaveAsync(PaymentMethod entity, CancellationToken cancellationToken)
        {
            _ = _paymentMethodDomainService.Update(entity.Id, entity);
            _ = _uow.SaveChanges();
            return Task.FromResult(true);
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
