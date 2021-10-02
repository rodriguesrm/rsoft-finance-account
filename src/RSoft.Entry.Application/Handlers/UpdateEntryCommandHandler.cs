using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Infra.Data;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Lib.Design.Application.Handlers;
using DomainEntry = RSoft.Entry.Core.Entities.Entry;
using DomainCategory = RSoft.Entry.Core.Entities.Category;
using MassTransit;
using RSoft.Finance.Contracts.Events;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// Create entry command handler
    /// </summary>
    public class UpdateEntryCommandHandler : UpdateCommandHandlerBase<UpdateEntryCommand, bool, DomainEntry>, IRequestHandler<UpdateEntryCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IEntryDomainService _entryDomainService;
        private readonly IBusControl _bus;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new handler instance
        /// </summary>
        /// <param name="entryDomainService">Entry domain service object</param>
        /// <param name="uow">Unit of work controller object</param>
        /// <param name="logger">Logger object</param>
        /// <param name="bus">Messaging bus control</param>
        public UpdateEntryCommandHandler(IEntryDomainService entryDomainService, IUnitOfWork uow, ILogger<CreateEntryCommandHandler> logger, IBusControl bus) : base(logger)
        {
            _entryDomainService = entryDomainService;
            _uow = uow;
            _bus = bus;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<DomainEntry> GetEntityByKeyAsync(UpdateEntryCommand request, CancellationToken cancellationToken)
            => await _entryDomainService.GetByKeyAsync(request.Id, cancellationToken);

        ///<inheritdoc/>
        protected override void PrepareEntity(UpdateEntryCommand request, DomainEntry entity)
        {
            entity.Name = request.Name;
            if (request.CategoryId.HasValue)
                entity.Category = new DomainCategory(request.CategoryId.Value);
        }

        ///<inheritdoc/>
        protected override async Task<bool> SaveAsync(DomainEntry entity, CancellationToken cancellationToken)
        {
            _ = _entryDomainService.Update(entity.Id, entity);
            _ = await _uow.SaveChangesAsync(cancellationToken);
            await _bus.Publish(new AccountChangedEvent(entity.Id, entity.Name, entity.Category.Id), cancellationToken);
            return true;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<bool>> Handle(UpdateEntryCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

    }
}