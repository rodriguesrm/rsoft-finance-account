using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Application.Extensions;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Commands;
using RSoft.Finance.Domain.Enum;
using RSoft.Lib.Design.Infra.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Create PaymentMethod command handler
    /// </summary>
    public class CreatePaymentMethodCommandHandler : IRequestHandler<CreatePaymentMethodCommand, CommandResult<Guid?>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IPaymentMethodDomainService _PaymentMethodDomainService;
        private readonly ILogger<CreatePaymentMethodCommandHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new handler instance
        /// </summary>
        /// <param name="PaymentMethodDomainService">PaymentMethod domain service object</param>
        /// <param name="logger">Logger object</param>
        /// <param name="uow">Unit of work controller object</param>
        public CreatePaymentMethodCommandHandler(IPaymentMethodDomainService PaymentMethodDomainService, ILogger<CreatePaymentMethodCommandHandler> logger, IUnitOfWork uow)
        {
            _PaymentMethodDomainService = PaymentMethodDomainService;
            _logger = logger;
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
        {
            _logger.LogInformation($"{GetType().Name} START");
            CommandResult<Guid?> result = new();
            PaymentTypeEnum? paymentType = null;
            if (request.PaymentType.HasValue)
                paymentType = (PaymentTypeEnum)request.PaymentType.Value;
            PaymentMethod entity = new() { Name = request.Name, PaymentType = paymentType };
            entity.Validate();
            if (entity.Valid)
            {
                entity = await _PaymentMethodDomainService.AddAsync(entity, cancellationToken);
                _ = await _uow.SaveChangesAsync();
                result.Response = entity.Id;
            }
            else
            {
                result.Errors = entity.Notifications.ToGenericNotifications();
            }
            _logger.LogInformation($"{GetType().Name} END");
            return result;
        }

        #endregion

    }
}
