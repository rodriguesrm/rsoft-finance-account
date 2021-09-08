using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RSoft.Account.Application.Extensions;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Commands;
using RSoft.Lib.Common.Abstractions;
using RSoft.Lib.Common.Models;
using RSoft.Lib.Design.Infra.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Change status PaymentMethod command handler
    /// </summary>
    public class ChangeStatusPaymentMethodCommandHandler : IRequestHandler<ChangeStatusPaymentMethodCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IPaymentMethodDomainService _PaymentMethodDomainService;
        private readonly ILogger<CreatePaymentMethodCommandHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a handler instance
        /// </summary>
        /// <param name="uow">Unit of work controller instance</param>
        /// <param name="PaymentMethodDomainService">PaymentMethod domain/core service</param>
        /// <param name="logger">Logger object</param>
        public ChangeStatusPaymentMethodCommandHandler(IUnitOfWork uow, IPaymentMethodDomainService PaymentMethodDomainService, ILogger<CreatePaymentMethodCommandHandler> logger)
        {
            _uow = uow;
            _PaymentMethodDomainService = PaymentMethodDomainService;
            _logger = logger;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Command request data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public Task<CommandResult<bool>> Handle(ChangeStatusPaymentMethodCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().Name} START");
            CommandResult<bool> result = new();
            PaymentMethod entity = _PaymentMethodDomainService.GetByKeyAsync(request.Id, cancellationToken).Result;
            if (entity == null)
            {
                IStringLocalizer<ChangeStatusPaymentMethodCommandHandler> localizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<ChangeStatusPaymentMethodCommandHandler>>();
                result.Errors = new List<GenericNotification>() { new GenericNotification("PaymentMethod", localizer["PAYMENTMETHOD_NOTFOUND"]) };
            }
            else
            {
                entity.IsActive = request.IsActive;
                entity.Validate();
                if (entity.Valid)
                {
                    _ = _PaymentMethodDomainService.Update(entity.Id, entity);
                    _ = _uow.SaveChanges();
                    result.Response = true;
                }
                else
                {
                    result.Errors = entity.Notifications.ToGenericNotifications();
                }
            }
            _logger.LogInformation($"{GetType().Name} END");
            return Task.FromResult(result);
        }

        #endregion

    }
}
