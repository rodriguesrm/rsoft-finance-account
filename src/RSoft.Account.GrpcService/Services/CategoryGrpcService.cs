using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Grpc;
using RSoft.Finance.Contracts.Commands;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using RSoft.Account.GrpcService.Extensions;

namespace RSoft.Account.GrpcService.Services
{

    /// <summary>
    /// Category gRPC Service
    /// </summary>
    [Authorize]
    public class CategoryGrpcService : Category.CategoryBase
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

        #region Local methods

        /// <summary>
        /// Send command via mediator
        /// </summary>
        /// <typeparam name="TReply"></typeparam>
        /// <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="createCommand"></param>
        /// <param name="successAction"></param>
        private async Task<TReply> SendCommand<TReply, TCommand, TResult>
        (
            string methodName, 
            Func<TCommand> createCommand, 
            Action<TReply, CommandResult<TResult>> successAction = null
        )
            where TReply : class
            where TCommand : IRequest<CommandResult<TResult>>
        {
            _logger.LogInformation($"Starting {methodName}");
            TReply reply = Activator.CreateInstance<TReply>();
            TCommand command = createCommand();
            CommandResult<TResult> result = await _mediator.Send(command);
            if (result.Success)
            {
                successAction?.Invoke(reply, result);
            }
            else
            {
                string jsonNotifications = JsonSerializer.Serialize(result.Errors);
                Status rpcStatus = new(StatusCode.InvalidArgument, jsonNotifications);
                throw new RpcException(status: rpcStatus, message: "BadRequest");
            }
            _logger.LogInformation($"{methodName} finished {(result.Success ? "sucessful" : "with errors")}");
            return reply;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<CreateCategoryReply> CreateCategory(CreateCategoryRequest request, ServerCallContext context)
            => await SendCommand<CreateCategoryReply, CreateCategoryCommand, Guid?>
            (
                nameof(CreateCategory), 
                () => new(request.Name),
                (reply, result) => reply.Id = result.Response.ToString()
            );

        /// <summary>
        /// Update a existing category
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<UpdateCategoryReply> UpdateCategory(UpdateCategoryRequest request, ServerCallContext context)
            => await SendCommand<UpdateCategoryReply, UpdateCategoryCommand, bool>
            (
                nameof(UpdateCategory),
                () => new(new Guid(request.Id), request.Name)
            );

        /// <summary>
        /// Enable a category
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<EnableCategoryReply> EnableCategory(EnableCategoryRequest request, ServerCallContext context)
            => await SendCommand<EnableCategoryReply, ChangeStatusCategoryCommand, bool>
            (
                nameof(EnableCategory),
                () => new(new Guid(request.Id), true)
            );


        /// <summary>
        /// Disable a category
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<DisableCategoryReply> DisableCategory(DisableCategoryRequest request, ServerCallContext context)
            => await SendCommand<DisableCategoryReply, ChangeStatusCategoryCommand, bool>
            (
                nameof(DisableCategory),
                () => new(new Guid(request.Id), false)
            );

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<GetCategoryReply> GetCategory(GetCategoryRequest request, ServerCallContext context)
            => await SendCommand<GetCategoryReply, GetCategoryByIdCommand, CategoryDto>
            (
                nameof(GetCategory),
                () => new(new Guid(request.Id)),
                (reply, result) => reply.Data = result.Response.Map()
            );

        /// <summary>
        /// List all categories
        /// </summary>
        /// <param name="request">Category request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<ListCategoryReply> ListCategory(ListCategoryRequest request, ServerCallContext context)
            => await SendCommand<ListCategoryReply, ListCategoryCommand, IEnumerable<CategoryDto>>
            (
                nameof(ListCategory),
                () => new(),
                (reply, result) => reply.Data.Add(result.Response.Map())
            );

        #endregion

    }
}
