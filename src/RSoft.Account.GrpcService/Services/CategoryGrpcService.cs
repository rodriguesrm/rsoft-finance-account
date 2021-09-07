using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Grpc;
using RSoft.Finance.Contracts.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using System.Collections.Generic;
using RSoft.Account.GrpcService.Extensions;

namespace RSoft.Account.GrpcService.Services
{

    /// <summary>
    /// Category gRPC Service
    /// </summary>
    [Authorize]
    public class CategoryGrpcService : Grpc.Category.CategoryBase
    {

        #region Local objects/variables

        private readonly IMediator _mediator;
        private readonly ILogger<CategoryGrpcService> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new Category gRPC Service
        /// </summary>
        /// <param name="mediator">Mediator object</param>
        /// <param name="logger">Logger object</param>
        public CategoryGrpcService(IMediator mediator, ILogger<CategoryGrpcService> logger)
        {
            _mediator = mediator;
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
        {
            _logger.LogInformation("Starting CreateCategory");
            CreateCategoryReply reply = new();
            CreateCategoryCommand command = new(request.Name);
            CommandResult<Guid?> result = await _mediator.Send(command);
            if (result.Response.HasValue)
            {
                reply.Success = true;
                reply.Id = result.Response.Value.ToString();
            }
            else
            {
                result.Errors.ToList().ForEach(err =>
                {
                    reply.Errors.Add(new ErrorsDictionary() { Key = err.Property, Value = err.Message });
                });
            }
            _logger.LogInformation($"CreateCategory finished {(reply.Success ? "sucessful" : "with errors")}");
            return reply;
        }

        /// <summary>
        /// Update a existing category
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<UpdateCategoryReply> UpdateCategory(UpdateCategoryRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Starting UpdateCategory");
            UpdateCategoryReply reply = new();
            UpdateCategoryCommand command = new(new Guid(request.Id), request.Name);
            CommandResult<bool> result = await _mediator.Send(command);
            if (result.Response)
            {
                reply.Success = true;
            }
            else
            {
                result.Errors.ToList().ForEach(err =>
                {
                    reply.Errors.Add(new ErrorsDictionary() { Key = err.Property, Value = err.Message });
                });
            }
            _logger.LogInformation($"UpdateCategory finished {(reply.Success ? "sucessful" : "with errors")}");
            return reply;
        }

        /// <summary>
        /// Enable a category
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<EnableCategoryReply> EnableCategory(EnableCategoryRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Starting EnableCategory");
            EnableCategoryReply reply = new();
            ChangeStatusCategoryCommand command = new(new Guid(request.Id), true);
            CommandResult<bool> result = await _mediator.Send(command);
            if (result.Response)
            {
                reply.Success = true;
            }
            else
            {
                result.Errors.ToList().ForEach(err =>
                {
                    reply.Errors.Add(new ErrorsDictionary() { Key = err.Property, Value = err.Message });
                });
            }
            _logger.LogInformation($"EnableCategory finished {(reply.Success ? "sucessful" : "with errors")}");
            return reply;
        }

        /// <summary>
        /// Disable a category
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<DisableCategoryReply> DisableCategory(DisableCategoryRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Starting EnableCategory");
            DisableCategoryReply reply = new();
            ChangeStatusCategoryCommand command = new(new Guid(request.Id), false);
            CommandResult<bool> result = await _mediator.Send(command);
            if (result.Response)
            {
                reply.Success = true;
            }
            else
            {
                result.Errors.ToList().ForEach(err =>
                {
                    reply.Errors.Add(new ErrorsDictionary() { Key = err.Property, Value = err.Message });
                });
            }
            _logger.LogInformation($"DisableCategory finished {(reply.Success ? "sucessful" : "with errors")}");
            return reply;
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<GetCategoryReply> GetCategory(GetCategoryRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Starting GetCategory");
            GetCategoryReply reply = new();
            GetCategoryByIdCommand command = new(new Guid(request.Id));
            CommandResult<CategoryDto> result = await _mediator.Send(command);
            if (result.Response != null)
            {
                reply.Data = result.Response.Map();
                reply.Success = true;
            }
            else
            {
                result.Errors.ToList().ForEach(err =>
                {
                    reply.Errors.Add(new ErrorsDictionary() { Key = err.Property, Value = err.Message });
                });
            }
            _logger.LogInformation($"GetCategory finished {(reply.Success ? "sucessful" : "with errors")}");
            return reply;
        }

        /// <summary>
        /// List all categories
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<ListCategoryReply> ListCategory(ListCategoryRequest request, ServerCallContext context)
        {

            _logger.LogInformation("Starting ListCategory");
            ListCategoryReply reply = new();
            ListCategoryCommand command = new();
            CommandResult<IEnumerable<CategoryDto>> result = await _mediator.Send(command);
            if (result.Response != null)
            {
                reply.Data.Add(result.Response.Map());
                reply.Success = true;
            }
            else
            {
                result.Errors.ToList().ForEach(err =>
                {
                    reply.Errors.Add(new ErrorsDictionary() { Key = err.Property, Value = err.Message });
                });
            }
            _logger.LogInformation($"ListCategory finished {(reply.Success ? "sucessful" : "with errors")}");
            return reply;

        }

        #endregion

    }
}
