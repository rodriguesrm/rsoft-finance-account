using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Infra.Data;
using System.Threading;
using System.Threading.Tasks;
using EntryAccount = RSoft.Entry.Core.Entities.Entry;
using RSoft.Lib.Design.Application.Handlers;
using MassTransit;
using RSoft.Finance.Contracts.Events;

namespace RSoft.Entry.Application.Handlers
{

    /// <summary>
    /// Change status Account command handler
    /// </summary>
    public class ChangeStatusAccountCommandHandler : UpdateCommandHandlerBase<ChangeStatusAccountCommand, bool, EntryAccount>, IRequestHandler<ChangeStatusAccountCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IAccountDomainService _accountDomainService;
        private readonly IBusControl _bus;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a handler instance
        /// </summary>
        /// <param name="uow">Unit of work controller instance</param>
        /// <param name="accountDomainService">Account domain/core service</param>
        /// <param name="logger">Logger object</param>
        /// <param name="bus">Messaging bus control</param>
        public ChangeStatusAccountCommandHandler(IUnitOfWork uow, IAccountDomainService accountDomainService, ILogger<CreateAccountCommandHandler> logger, IBusControl bus) : base(logger)
        {
            _uow = uow;
            _accountDomainService = accountDomainService;
            _bus = bus;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<EntryAccount> GetEntityByKeyAsync(ChangeStatusAccountCommand request, CancellationToken cancellationToken)
            => await _accountDomainService.GetByKeyAsync(request.Id, cancellationToken);

        ///<inheritdoc/>
        protected override void PrepareEntity(ChangeStatusAccountCommand request, EntryAccount entity)
        {
            entity.IsActive = request.IsActive;
        }

        ///<inheritdoc/>
        protected override async Task<bool> SaveAsync(EntryAccount entity, CancellationToken cancellationToken)
        {
            _ = _accountDomainService.Update(entity.Id, entity);
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
        public async Task<CommandResult<bool>> Handle(ChangeStatusAccountCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

    }
}
