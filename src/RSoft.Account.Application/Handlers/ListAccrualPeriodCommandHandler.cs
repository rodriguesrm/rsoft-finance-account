using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Application.Extensions;
using RSoft.Lib.Design.Application.Handlers;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Contracts.Models;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// List AccrualPeriod command handler
    /// </summary>
    public class ListAccrualPeriodCommandHandler : ListCommandHandlerBase<ListAccrualPeriodCommand, AccrualPeriodDto, AccrualPeriod>, IRequestHandler<ListAccrualPeriodCommand, CommandResult<IEnumerable<AccrualPeriodDto>>>
    {

        #region Local objects/variables

        private readonly IAccrualPeriodDomainService _accrualPeriodDomainService;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new command handler instance
        /// </summary>
        /// <param name="accrualPeriodDomainService">AccrualPeriod domain/core service</param>
        /// <param name="logger">Logger object</param>
        public ListAccrualPeriodCommandHandler(IAccrualPeriodDomainService accrualPeriodDomainService, ILogger<ListAccrualPeriodCommandHandler> logger) : base(logger)
        {
            _accrualPeriodDomainService = accrualPeriodDomainService;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<IEnumerable<AccrualPeriod>> GetAllAsync(ListAccrualPeriodCommand request, CancellationToken cancellationToken)
            => await _accrualPeriodDomainService.GetAllAsync(cancellationToken);

        ///<inheritdoc/>
        protected override IEnumerable<AccrualPeriodDto> MapEntities(IEnumerable<AccrualPeriod> entities)
            => entities.Map();

        #endregion

        #region Handlers

        /// <summary>
        /// Command hanlder
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<IEnumerable<AccrualPeriodDto>>> Handle(ListAccrualPeriodCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion
    }

}
