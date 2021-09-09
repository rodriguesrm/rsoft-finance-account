using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Application.Extensions;
using RSoft.Lib.Design.Application.Handlers;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Lista PaymentMethod command handler
    /// </summary>
    public class ListPaymentMethodCommandHandler : ListCommandHandlerBase<ListPaymentMethodCommand, PaymentMethodDto, PaymentMethod>, IRequestHandler<ListPaymentMethodCommand, CommandResult<IEnumerable<PaymentMethodDto>>>
    {

        #region Local objects/variables

        private readonly IPaymentMethodDomainService _paymentMethodDomainService;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new command handler instance
        /// </summary>
        /// <param name="PaymentMethodDomainService">PaymentMethod domain/core service</param>
        /// <param name="logger">Logger object</param>
        public ListPaymentMethodCommandHandler(IPaymentMethodDomainService PaymentMethodDomainService, ILogger<ListPaymentMethodCommandHandler> logger) : base(logger)
        {
            _paymentMethodDomainService = PaymentMethodDomainService;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<IEnumerable<PaymentMethod>> GetAllAsync(ListPaymentMethodCommand request, CancellationToken cancellationToken)
            => await _paymentMethodDomainService.GetAllAsync(cancellationToken);

        ///<inheritdoc/>
        protected override IEnumerable<PaymentMethodDto> MapEntities(IEnumerable<PaymentMethod> entities)
            => entities.Map();

        #endregion

        #region Handlers

        /// <summary>
        /// Command hanlder
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<IEnumerable<PaymentMethodDto>>> Handle(ListPaymentMethodCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion
    }

}
