using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using RSoft.Entry.Contracts.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using RSoft.Entry.GrpcService.Extensions;
using RSoft.Entry.Grpc.Protobuf;
using Google.Protobuf.WellKnownTypes;

namespace RSoft.Entry.GrpcService.Services
{

    /// <summary>
    /// Category gRPC Service
    /// </summary>
    [Authorize]
    public class CategoryGrpcService : Grpc.Protobuf.Category.CategoryBase
    {

        #region Local objects/variables

        private readonly ILogger<CategoryGrpcService> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new Category gRPC Service
        /// </summary>
        /// <param name="logger">Logger object</param>
        public CategoryGrpcService(ILogger<CategoryGrpcService> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<CreateCategoryReply> CreateCategory(CreateCategoryRequest request, ServerCallContext context)
            => await GrpcServiceHelpers.SendCommand<CreateCategoryReply, CreateCategoryCommand, Guid?>
            (
                nameof(CreateCategory), 
                () => new(request.Name),
                (reply, result) => reply.Id = result.Response.ToString(),
                logger: _logger
            );

        /// <summary>
        /// Update a existing category
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<Empty> UpdateCategory(UpdateCategoryRequest request, ServerCallContext context)
            => await GrpcServiceHelpers.SendCommand<Empty, UpdateCategoryCommand, bool>
            (
                nameof(UpdateCategory),
                () => new(new Guid(request.Id), request.Name),
                logger: _logger
            );

        /// <summary>
        /// Enable a category
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<Empty> EnableCategory(EnableCategoryRequest request, ServerCallContext context)
            => await GrpcServiceHelpers.SendCommand<Empty, ChangeStatusCategoryCommand, bool>
            (
                nameof(EnableCategory),
                () => new(new Guid(request.Id), true),
                logger: _logger
            );


        /// <summary>
        /// Disable a category
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<Empty> DisableCategory(DisableCategoryRequest request, ServerCallContext context)
            => await GrpcServiceHelpers.SendCommand<Empty, ChangeStatusCategoryCommand, bool>
            (
                nameof(DisableCategory),
                () => new(new Guid(request.Id), false),
                logger: _logger
            );

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<CategoryDetail> GetCategory(GetCategoryRequest request, ServerCallContext context)
            => await GrpcServiceHelpers.SendCommand<CategoryDetail, GetCategoryByIdCommand, CategoryDto>
            (
                nameof(GetCategory),
                () => new(new Guid(request.Id)),
                (reply, result) => result.Response.Map(reply),
                logger: _logger
            );

        /// <summary>
        /// List all categories
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<ListCategoryReply> ListCategory(Empty request, ServerCallContext context)
            => await GrpcServiceHelpers.SendCommand<ListCategoryReply, ListCategoryCommand, IEnumerable<CategoryDto>>
            (
                nameof(ListCategory),
                () => new(),
                (reply, result) => reply.Data.Add(result.Response.Map()),
                logger: _logger
            );

        #endregion

    }
}
