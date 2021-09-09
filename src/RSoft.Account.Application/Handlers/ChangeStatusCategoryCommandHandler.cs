using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Commands;
using RSoft.Lib.Design.Infra.Data;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Account.Application.Handlers.Abstractions;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Change status category command handler
    /// </summary>
    public class ChangeStatusCategoryCommandHandler : UpdateCommandHandlerBase<ChangeStatusCategoryCommand, bool, Category>, IRequestHandler<ChangeStatusCategoryCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly ICategoryDomainService _categoryDomainService;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a handler instance
        /// </summary>
        /// <param name="uow">Unit of work controller instance</param>
        /// <param name="categoryDomainService">Category domain/core service</param>
        /// <param name="logger">Logger object</param>
        public ChangeStatusCategoryCommandHandler(IUnitOfWork uow, ICategoryDomainService categoryDomainService, ILogger<CreateCategoryCommandHandler> logger) : base(logger)
        {
            _uow = uow;
            _categoryDomainService = categoryDomainService;
            _logger = logger;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<Category> GetEntityByKeyAsync(ChangeStatusCategoryCommand request, CancellationToken cancellationToken)
            => await _categoryDomainService.GetByKeyAsync(request.Id, cancellationToken);

        ///<inheritdoc/>
        protected override void PrepareEntity(ChangeStatusCategoryCommand request, Category entity)
        {
            entity.IsActive = request.IsActive;
        }

        ///<inheritdoc/>
        protected override Task<bool> SaveAsync(Category entity, CancellationToken cancellationToken)
        {
            _ = _categoryDomainService.Update(entity.Id, entity);
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
        public async Task<CommandResult<bool>> Handle(ChangeStatusCategoryCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

    }
}
