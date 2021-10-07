using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Finance.Contracts.Events;
using RSoft.Lib.Common.Models;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Application.Handlers;
using RSoft.Lib.Design.Infra.Data;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// Close AccrualPeriod command handler
    /// </summary>
    [Authorize]
    public class CloseAccrualPeriodCommandHandler : UpdateCommandHandlerBase<CloseAccrualPeriodCommand, bool, AccrualPeriod>, IRequestHandler<CloseAccrualPeriodCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IAccrualPeriodDomainService _accrualPeriodDomainService;
        private readonly IBusControl _bus;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new handler instance
        /// </summary>
        /// <param name="accrualPeriodDomainService">AccrualPeriod domain service object</param>
        /// <param name="logger">Logger object</param>
        /// <param name="uow">Unit of work controller object</param>
        /// <param name="bus">Message bus control</param>
        public CloseAccrualPeriodCommandHandler(IAccrualPeriodDomainService accrualPeriodDomainService, ILogger<CloseAccrualPeriodCommandHandler> logger, IUnitOfWork uow, IBusControl bus) : base(logger)
        {
            _accrualPeriodDomainService = accrualPeriodDomainService;
            _uow = uow;
            _bus = bus;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<AccrualPeriod> GetEntityByKeyAsync(CloseAccrualPeriodCommand request, CancellationToken cancellationToken)
            => await _accrualPeriodDomainService.GetByKeyAsync(request.Year, request.Month, cancellationToken);

        ///<inheritdoc/>
        protected override void PrepareEntity(CloseAccrualPeriodCommand request, AccrualPeriod entity) { /* Nothing to do */ }

        ///<inheritdoc/>
        protected override async Task<bool> SaveAsync(AccrualPeriod entity, CancellationToken cancellationToken)
        {

            SimpleOperationResult result = await _accrualPeriodDomainService.ClosePeriodAsync(entity.Year, entity.Month, cancellationToken);
            if (result.Success)
            {
                _ = await _uow.SaveChangesAsync(cancellationToken);
                await _bus.Publish(new AccrualPeriodClosedEvent(entity.Year, entity.Month), cancellationToken);
                return true;
            }
            return false;

        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<bool>> Handle(CloseAccrualPeriodCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

    }
}
