using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Infra.Data;
using System.Threading;
using System.Threading.Tasks;
using DomainEntry = RSoft.Entry.Core.Entities.Entry;
using RSoft.Lib.Design.Application.Handlers;
using MassTransit;
using RSoft.Finance.Contracts.Events;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// Change status Entry command handler
    /// </summary>
    public class ChangeStatusEntryCommandHandler : UpdateCommandHandlerBase<ChangeStatusEntryCommand, bool, DomainEntry>, IRequestHandler<ChangeStatusEntryCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IEntryDomainService _entryDomainService;
        private readonly IBusControl _bus;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a handler instance
        /// </summary>
        /// <param name="uow">Unit of work controller instance</param>
        /// <param name="entryDomainService">Entry domain/core service</param>
        /// <param name="logger">Logger object</param>
        /// <param name="bus">Messaging bus control</param>
        public ChangeStatusEntryCommandHandler(IUnitOfWork uow, IEntryDomainService entryDomainService, ILogger<CreateEntryCommandHandler> logger, IBusControl bus) : base(logger)
        {
            _uow = uow;
            _entryDomainService = entryDomainService;
            _bus = bus;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<DomainEntry> GetEntityByKeyAsync(ChangeStatusEntryCommand request, CancellationToken cancellationToken)
            => await _entryDomainService.GetByKeyAsync(request.Id, cancellationToken);

        ///<inheritdoc/>
        protected override void PrepareEntity(ChangeStatusEntryCommand request, DomainEntry entity)
        {
            entity.IsActive = request.IsActive;
        }

        ///<inheritdoc/>
        protected override async Task<bool> SaveAsync(DomainEntry entity, CancellationToken cancellationToken)
        {
            _ = _entryDomainService.Update(entity.Id, entity);
            _ = await _uow.SaveChangesAsync(cancellationToken);
            await _bus.Publish(new AccountStatusChangedEvent(entity.Id, entity.IsActive), cancellationToken);
            return true;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Command request data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<bool>> Handle(ChangeStatusEntryCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

    }
}
