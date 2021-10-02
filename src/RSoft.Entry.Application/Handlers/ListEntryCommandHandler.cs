using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Application.Extensions;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Contracts.Models;
using DomainEntry = RSoft.Entry.Core.Entities.Entry;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Lib.Design.Application.Handlers;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// Lista entry command handler
    /// </summary>
    public class ListEntryCommandHandler : ListCommandHandlerBase<ListEntryCommand, EntryDto, DomainEntry>, IRequestHandler<ListEntryCommand, CommandResult<IEnumerable<EntryDto>>>
    {

        #region Local objects/variables

        private readonly IEntryDomainService _entryDomainService;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new command handler instance
        /// </summary>
        /// <param name="entryDomainService">Entry domain/core service</param>
        /// <param name="logger">Logger object</param>
        public ListEntryCommandHandler(IEntryDomainService entryDomainService, ILogger<ListEntryCommandHandler> logger) : base(logger)
        {
            _entryDomainService = entryDomainService;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<IEnumerable<DomainEntry>> GetAllAsync(ListEntryCommand request, CancellationToken cancellationToken)
            => await _entryDomainService.GetAllAsync(cancellationToken);

        ///<inheritdoc/>
        protected override IEnumerable<EntryDto> MapEntities(IEnumerable<DomainEntry> entities)
            => entities.Map();

        #endregion

        #region Handlers

        /// <summary>
        /// Command hanlder
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<IEnumerable<EntryDto>>> Handle(ListEntryCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion
    }

}
