using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Commands;
using RSoft.Lib.Common.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Lib.Common.Models;
using RSoft.Account.Application.Extensions;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Get PaymentMethod by id command handler
    /// </summary>
    public class GetPaymentMethodByIdCommandHandler : IRequestHandler<GetPaymentMethodByIdCommand, CommandResult<PaymentMethodDto>>
    {

        #region Local objects/variables

        private readonly IPaymentMethodDomainService _PaymentMethodDomainService;
        private readonly ILogger<GetPaymentMethodByIdCommandHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new command handler instance
        /// </summary>
        /// <param name="PaymentMethodDomainService">PaymentMethod domain/core service</param>
        /// <param name="logger">Logger object</param>
        public GetPaymentMethodByIdCommandHandler(IPaymentMethodDomainService PaymentMethodDomainService, ILogger<GetPaymentMethodByIdCommandHandler> logger)
        {
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
        public Task<CommandResult<PaymentMethodDto>> Handle(GetPaymentMethodByIdCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().Name} START");
            CommandResult<PaymentMethodDto> result = new();
            PaymentMethod entity = _PaymentMethodDomainService.GetByKeyAsync(request.Id).Result;
            if (entity == null)
            {
                IStringLocalizer<GetPaymentMethodByIdCommandHandler> localizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<GetPaymentMethodByIdCommandHandler>>();
                result.Errors = new List<GenericNotification>() { new GenericNotification("PaymentMethod", localizer["PAYMENTMETHOD_NOTFOUND"]) };
            }
            else
            {
                result.Response = entity.Map();
            }
            _logger.LogInformation($"{GetType().Name} END");
            return Task.FromResult(result);
        }

        #endregion

    }
}
