using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Contracts.Models;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Entry.Application.Extensions;
using RSoft.Lib.Design.Application.Handlers;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// Get PaymentMethod by id command handler
    /// </summary>
    public class GetPaymentMethodByIdCommandHandler : GetByKeyCommandHandlerBase<GetPaymentMethodByIdCommand, PaymentMethodDto, PaymentMethod>, IRequestHandler<GetPaymentMethodByIdCommand, CommandResult<PaymentMethodDto>>
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
        public GetPaymentMethodByIdCommandHandler(IPaymentMethodDomainService PaymentMethodDomainService, ILogger<GetPaymentMethodByIdCommandHandler> logger) : base(logger)
        {
            _paymentMethodDomainService = PaymentMethodDomainService;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<PaymentMethod> GetEntityByKeyAsync(GetPaymentMethodByIdCommand request, CancellationToken cancellationToken)
            => await _paymentMethodDomainService.GetByKeyAsync(request.Id);

        ///<inheritdoc/>
        protected override PaymentMethodDto MapEntity(PaymentMethod entity)
            => entity.Map();

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Command request data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<PaymentMethodDto>> Handle(GetPaymentMethodByIdCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

    }
}
