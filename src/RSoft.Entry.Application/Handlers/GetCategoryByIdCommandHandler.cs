using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Contracts.Models;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Design.Application.Commands;
using System.Threading;
using System.Threading.Tasks;
using RSoft.Entry.Application.Extensions;
using RSoft.Lib.Design.Application.Handlers;

namespace RSoft.Entry.Application.Handlers
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
            => await _categoryDomainService.GetByKeyAsync(request.Id, cancellationToken);

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
