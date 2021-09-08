using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Application.Extensions;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Lista PaymentMethod command handler
    /// </summary>
    public class ListPaymentMethodCommandHandler : IRequestHandler<ListPaymentMethodCommand, CommandResult<IEnumerable<PaymentMethodDto>>>
    {

        #region Local objects/variables

        private readonly IPaymentMethodDomainService _PaymentMethodDomainService;
        private readonly ILogger<ListPaymentMethodCommandHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new command handler instance
        /// </summary>
        /// <param name="PaymentMethodDomainService">PaymentMethod domain/core service</param>
        /// <param name="logger">Logger object</param>
        public ListPaymentMethodCommandHandler(IPaymentMethodDomainService PaymentMethodDomainService, ILogger<ListPaymentMethodCommandHandler> logger)
        {
            _PaymentMethodDomainService = PaymentMethodDomainService;
            _logger = logger;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command hanlder
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public Task<CommandResult<IEnumerable<PaymentMethodDto>>> Handle(ListPaymentMethodCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().Name} START");
            CommandResult<IEnumerable<PaymentMethodDto>> result = new();
            IEnumerable<PaymentMethod> entities = _PaymentMethodDomainService.GetAllAsync(cancellationToken).Result;
            if (entities != null)
            {
                result.Response = entities.Map();
            }
            _logger.LogInformation($"{GetType().Name} END");
            return Task.FromResult(result);
        }

        #endregion
    }

}
