using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Infra.Data;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Lib.Design.Application.Handlers;
using DomainAccount = RSoft.Account.Core.Entities.Account;
using DomainCategory = RSoft.Account.Core.Entities.Category;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Create Account command handler
    /// </summary>
    public class UpdateAccountCommandHandler : UpdateCommandHandlerBase<UpdateAccountCommand, bool, DomainAccount>, IRequestHandler<UpdateAccountCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly IAccountDomainService _accountDomainService;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new handler instance
        /// </summary>
        /// <param name="accountDomainService">Account domain service object</param>
        /// <param name="uow">Unit of work controller object</param>
        /// <param name="logger">Logger object</param>
        public UpdateAccountCommandHandler(IAccountDomainService accountDomainService, IUnitOfWork uow, ILogger<CreateAccountCommandHandler> logger) : base(logger)
        {
            _accountDomainService = accountDomainService;
            _uow = uow;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<DomainAccount> GetEntityByKeyAsync(UpdateAccountCommand request, CancellationToken cancellationToken)
            => await _accountDomainService.GetByKeyAsync(request.Id, cancellationToken);

        ///<inheritdoc/>
        protected override void PrepareEntity(UpdateAccountCommand request, DomainAccount entity)
        {
            entity.Name = request.Name;
            if (request.CategoryId.HasValue)
                entity.Category = new DomainCategory(request.CategoryId.Value);
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
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<bool>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

    }
}