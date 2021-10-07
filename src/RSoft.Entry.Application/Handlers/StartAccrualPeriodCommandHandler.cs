using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Finance.Contracts.Events;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Application.Handlers;
using RSoft.Lib.Design.Infra.Data;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// Start AccrualPeriod command handler
    /// </summary>
    [Authorize]
    public class StartAccrualPeriodCommandHandler : CreateCommandHandlerBase<StartAccrualPeriodCommand, bool, AccrualPeriod>, IRequestHandler<StartAccrualPeriodCommand, CommandResult<bool>>
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
        public StartAccrualPeriodCommandHandler(IAccrualPeriodDomainService accrualPeriodDomainService, ILogger<StartAccrualPeriodCommandHandler> logger, IUnitOfWork uow, IBusControl bus) : base(logger)
        {
            _accrualPeriodDomainService = accrualPeriodDomainService;
            _uow = uow;
            _bus = bus;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<bool>> Handle(StartAccrualPeriodCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override AccrualPeriod PrepareEntity(StartAccrualPeriodCommand request)
            => new(request.Year, request.Month);

        ///<inheritdoc/>
        protected override async Task<bool> SaveAsync(AccrualPeriod entity, CancellationToken cancellationToken)
        {
            entity = await _accrualPeriodDomainService.AddAsync(entity, cancellationToken);
            _ = await _uow.SaveChangesAsync(cancellationToken);
            await _bus.Publish(new AccrualPeriodStartedEvent(entity.Year, entity.Month), cancellationToken);
            return true;
        }

        #endregion

    }
}
