using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Events;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Application.Handlers;
using RSoft.Lib.Design.Infra.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Create AccrualPeriod command handler
    /// </summary>
    [Authorize]
    public class StartAccrualPeriodCommandHandler : CreateCommandHandlerBase<StartAccrualPeriodCommand, bool, AccrualPeriod>, IRequestHandler<StartAccrualPeriodCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IAccrualPeriodDomainService _AccrualPeriodDomainService;
        private readonly IBusControl _bus;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new handler instance
        /// </summary>
        /// <param name="AccrualPeriodDomainService">AccrualPeriod domain service object</param>
        /// <param name="logger">Logger object</param>
        /// <param name="uow">Unit of work controller object</param>
        /// <param name="bus">Message bus control</param>
        public StartAccrualPeriodCommandHandler(IAccrualPeriodDomainService AccrualPeriodDomainService, ILogger<StartAccrualPeriodCommandHandler> logger, IUnitOfWork uow, IBusControl bus) : base(logger)
        {
            _AccrualPeriodDomainService = AccrualPeriodDomainService;
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
        {
            float closingBalance = 0.0f;
            DateTime lastPeriodDate = (new DateTime(request.Year, request.Month, 1)).AddMonths(-1);
            AccrualPeriod lastAccrualDate = _AccrualPeriodDomainService.GetByKeyAsync(lastPeriodDate.Year, lastPeriodDate.Month).Result ;
            if (lastAccrualDate != null & lastAccrualDate.IsClosed)
                closingBalance = lastAccrualDate.ClosingBalance;

            AccrualPeriod result = new()
            {
                Year = request.Year,
                Month = request.Month,
                OpeningBalance = closingBalance
            };

            return result;

        }

        ///<inheritdoc/>
        protected override async Task<bool> SaveAsync(AccrualPeriod entity, CancellationToken cancellationToken)
        {
            entity = await _AccrualPeriodDomainService.AddAsync(entity, cancellationToken);
            _ = await _uow.SaveChangesAsync(cancellationToken);
            //TODO: Publish Event
            //await _bus.Publish(new AccrualPeriodCreatedEvent(entity.Id, entity.Name), cancellationToken);
            return true;
        }

        #endregion

    }
}
