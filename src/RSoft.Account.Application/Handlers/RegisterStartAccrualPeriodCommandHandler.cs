using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Infra.Data;
using System.Threading;
using System.Threading.Tasks;
using DomainAccrualPeriod = RSoft.Account.Core.Entities.AccrualPeriod;
using System;
using RSoft.Account.Application.Extensions;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Register start accrual period command handler
    /// </summary>
    public class RegisterStartAccrualPeriodCommandHandler : IRequestHandler<RegisterStartAccrualPeriodCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IAccrualPeriodDomainService _accrualPeriodDomainService;
        private readonly ILogger<RegisterStartAccrualPeriodCommandHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new handler instance
        /// </summary>
        /// <param name="uow">Unit of work controller object</param>
        /// <param name="accrualPeriodDomainService">Accrual period domain service</param>
        /// <param name="logger">Logger object</param>
        public RegisterStartAccrualPeriodCommandHandler
        (
            IUnitOfWork uow, 
            IAccrualPeriodDomainService accrualPeriodDomainService, 
            ILogger<RegisterStartAccrualPeriodCommandHandler> logger
        )
        {
            _uow = uow;
            _accrualPeriodDomainService = accrualPeriodDomainService;
            _logger = logger;
        }

        #endregion

        #region Handlers

        ///<inheritdoc/>
        public async Task<CommandResult<bool>> Handle(RegisterStartAccrualPeriodCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation($"{GetType().Name} START");
            CommandResult<bool> result = new();
            DomainAccrualPeriod entity = await _accrualPeriodDomainService.GetByKeyAsync(request.Year, request.Month);
            if (entity != null)
            {

                float closingBalance = 0.0f;
                DateTime lastPeriodDate = (new DateTime(request.Year, request.Month, 1)).AddMonths(-1);
                DomainAccrualPeriod lastAccrualDate = await _accrualPeriodDomainService.GetByKeyAsync(lastPeriodDate.Year, lastPeriodDate.Month);
                if (lastAccrualDate != null && lastAccrualDate.IsClosed)
                    closingBalance = lastAccrualDate.ClosingBalance;
                entity.OpeningBalance = closingBalance;
                entity.SetServiceAuthor(true);

                entity.Validate();
                _accrualPeriodDomainService.Update(entity.Year, entity.Month, entity);
                _ = await _uow.SaveChangesAsync(cancellationToken);
            }
            _logger.LogInformation($"{GetType().Name} END");
            return result;

        }

        #endregion

    }
}
