using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using RSoft.Lib.Design.Application.Handlers;
using RSoft.Lib.Design.Infra.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Create category command handler
    /// </summary>
    public class CreateCategoryCommandHandler : CreateCommandHandlerBase<CreateCategoryCommand, Guid?, Category>, IRequestHandler<CreateCategoryCommand, CommandResult<Guid?>>
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
        /// <param name="logger">Logger object</param>
        /// <param name="uow">Unit of work controller object</param>
        public CreateCategoryCommandHandler(ICategoryDomainService categoryDomainService, ILogger<CreateCategoryCommandHandler> logger, IUnitOfWork uow) : base(logger)
        {
            _categoryDomainService = categoryDomainService;
            _uow = uow;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<Guid?>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override Category PrepareEntity(CreateCategoryCommand request)
            => new() { Name = request.Name };

        ///<inheritdoc/>
        protected override async Task<Guid?> SaveAsync(Category entity, CancellationToken cancellationToken)
        {
            entity = await _categoryDomainService.AddAsync(entity, cancellationToken);
            _ = await _uow.SaveChangesAsync();
            return entity.Id;
        }

        #endregion

    }
}
