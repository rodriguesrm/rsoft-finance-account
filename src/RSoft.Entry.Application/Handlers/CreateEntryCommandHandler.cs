using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Ports;
using RSoft.Finance.Contracts.Events;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Application.Handlers;
using RSoft.Lib.Design.Infra.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using DomainEntry = RSoft.Entry.Core.Entities.Entry;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// Create entry command handler
    /// </summary>
    public class CreateEntryCommandHandler : CreateCommandHandlerBase<CreateEntryCommand, Guid?, DomainEntry>, IRequestHandler<CreateEntryCommand, CommandResult<Guid?>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IEntryDomainService _entryDomainService;
        private readonly IBusControl _bus;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a command handler object instance
        /// </summary>
        /// <param name="entryDomainService">Entry domain service object</param>
        /// <param name="logger">Logger object</param>
        /// <param name="uow">Unit of work controller object</param>
        /// <param name="bus">Message bus control</param>
        public CreateEntryCommandHandler(IUnitOfWork uow, IEntryDomainService entryDomainService, ILogger<CreateEntryCommandHandler> logger, IBusControl bus) : base(logger)
        {
            _uow = uow;
            _entryDomainService = entryDomainService;
            _bus = bus;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<Guid?>> Handle(CreateEntryCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override DomainEntry PrepareEntity(CreateEntryCommand request)
        {
            DomainEntry entity = new();
            entity.Name = request.Name;
            if (request.CategoryId.HasValue)
                entity.Category = new(request.CategoryId.Value);
            return entity;
        }

        ///<inheritdoc/>
        protected override async Task<Guid?> SaveAsync(DomainEntry entity, CancellationToken cancellationToken)
        {
            AccountCreatedEvent entryCreatedEvent = new(entity.Id, entity.Name, entity.Category.Id);
            entity = await _entryDomainService.AddAsync(entity, cancellationToken);
            _ = await _uow.SaveChangesAsync(cancellationToken);
            await _bus.Publish(entryCreatedEvent, cancellationToken);
            return entity.Id;
        }

        #endregion

    }
}
