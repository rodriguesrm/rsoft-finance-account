using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Commands;
using RSoft.Lib.Design.Infra.Data;
using System.Threading;
using System.Threading.Tasks;
using DomainAccount = RSoft.Account.Core.Entities.Account;
using RSoft.Account.Application.Handlers.Abstractions;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Change status Account command handler
    /// </summary>
    public class ChangeStatusAccountCommandHandler : UpdateCommandHandlerBase<ChangeStatusAccountCommand, bool, DomainAccount>, IRequestHandler<ChangeStatusAccountCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IAccountDomainService _accountDomainService;
        private readonly ILogger<CreateAccountCommandHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a handler instance
        /// </summary>
        /// <param name="uow">Unit of work controller instance</param>
        /// <param name="accountDomainService">Account domain/core service</param>
        /// <param name="logger">Logger object</param>
        public ChangeStatusAccountCommandHandler(IUnitOfWork uow, IAccountDomainService accountDomainService, ILogger<CreateAccountCommandHandler> logger) : base(logger)
        {
            _uow = uow;
            _accountDomainService = accountDomainService;
            _logger = logger;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<DomainAccount> GetEntityByKeyAsync(ChangeStatusAccountCommand request, CancellationToken cancellationToken)
            => await _accountDomainService.GetByKeyAsync(request.Id, cancellationToken);

        ///<inheritdoc/>
        protected override void PrepareEntity(ChangeStatusAccountCommand request, DomainAccount entity)
        {
            entity.IsActive = request.IsActive;
        }

        ///<inheritdoc/>
        protected override Task<bool> SaveAsync(DomainAccount entity, CancellationToken cancellationToken)
        {
            _ = _accountDomainService.Update(entity.Id, entity);
            _ = _uow.SaveChanges();
            return Task.FromResult(true);
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
