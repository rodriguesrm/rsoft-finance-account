using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Commands;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Account.Application.Extensions;
using RSoft.Account.Application.Handlers.Abstractions;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// Get Category by id command handler
    /// </summary>
    public class GetCategoryByIdCommandHandler : GetByKeyCommandHandlerBase<GetCategoryByIdCommand, CategoryDto, Category>, IRequestHandler<GetCategoryByIdCommand, CommandResult<CategoryDto>>
    {

        #region Local objects/variables

        private readonly ICategoryDomainService _categoryDomainService;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new command handler instance
        /// </summary>
        /// <param name="categoryDomainService">Category domain/core service</param>
        /// <param name="logger">Logger object</param>
        public GetCategoryByIdCommandHandler(ICategoryDomainService categoryDomainService, ILogger<GetCategoryByIdCommandHandler> logger) : base(logger)
        {
            _categoryDomainService = categoryDomainService;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<Category> GetEntityByKeyAsync(GetCategoryByIdCommand request, CancellationToken cancellationToken)
            => await _categoryDomainService.GetByKeyAsync(request.Id);

        ///<inheritdoc/>
        protected override CategoryDto MapEntity(Category entity)
            => entity.Map();

        #endregion

        #region Handlers

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="request">Command request data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<CategoryDto>> Handle(GetCategoryByIdCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion

    }
}
