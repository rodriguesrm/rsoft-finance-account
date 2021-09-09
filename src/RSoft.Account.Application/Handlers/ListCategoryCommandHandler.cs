using MediatR;
using Microsoft.Extensions.Logging;
using RSoft.Account.Application.Extensions;
using RSoft.Account.Application.Handlers.Abstractions;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Finance.Contracts.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Application.Handlers
{

    /// <summary>
    /// List category command handler
    /// </summary>
    public class ListCategoryCommandHandler : ListCommandHandlerBase<ListCategoryCommand, CategoryDto, Category>, IRequestHandler<ListCategoryCommand, CommandResult<IEnumerable<CategoryDto>>>
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
        public ListCategoryCommandHandler(ICategoryDomainService categoryDomainService, ILogger<ListCategoryCommandHandler> logger) : base(logger)
        {
            _categoryDomainService = categoryDomainService;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override async Task<IEnumerable<Category>> GetAllAsync(ListCategoryCommand request, CancellationToken cancellationToken)
            => await _categoryDomainService.GetAllAsync(cancellationToken);

        ///<inheritdoc/>
        protected override IEnumerable<CategoryDto> MapEntities(IEnumerable<Category> entities)
            => entities.Map();

        #endregion

        #region Handlers

        /// <summary>
        /// Command hanlder
        /// </summary>
        /// <param name="request">Request command data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task<CommandResult<IEnumerable<CategoryDto>>> Handle(ListCategoryCommand request, CancellationToken cancellationToken)
            => await RunHandler(request, cancellationToken);

        #endregion
    }

}
