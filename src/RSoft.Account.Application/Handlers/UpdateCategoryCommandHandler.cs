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
    /// Create category command handler
    /// </summary>
    public class UpdateCategoryCommandHandler : UpdateCommandHandlerBase<UpdateCategoryCommand, bool, Category>, IRequestHandler<UpdateCategoryCommand, CommandResult<bool>>
    {

        #region Local objects/variables

        private readonly IUnitOfWork _uow;
        private readonly ICategoryDomainService _categoryDomainService;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new handler instance
        /// </summary>
        /// <param name="categoryDomainService">Category domain service object</param>
        /// <param name="uow">Unit of work controller object</param>
        /// <param name="logger">Logger object</param>
        public UpdateCategoryCommandHandler(ICategoryDomainService categoryDomainService, IUnitOfWork uow, ILogger<CreateCategoryCommandHandler> logger) : base(logger)
        {
            _categoryDomainService = categoryDomainService;
            _uow = uow;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<Category> GetEntityByKeyAsync(UpdateCategoryCommand request, CancellationToken cancellationToken)
            => await _categoryDomainService.GetByKeyAsync(request.Id, cancellationToken);

        ///<inheritdoc/>
        protected override void PrepareEntity(UpdateCategoryCommand request, Category entity)
        {
            entity.Name = request.Name;
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
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

    }
}
