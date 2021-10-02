using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RSoft.Entry.Contracts.Commands;
using System;
using System.Threading.Tasks;
using RSoft.Entry.Contracts.Models;
using RSoft.Entry.GrpcService.Extensions;
using System.Collections.Generic;
using RSoft.Entry.Grpc.Protobuf;
using Google.Protobuf.WellKnownTypes;

namespace RSoft.Entry.GrpcService.Services
{

    /// <summary>
    /// Account gRPC Service
    /// </summary>
    [Authorize]
    public class AccountGrpcService : Grpc.Protobuf.Account.AccountBase
    {

        #region Local objects/variables

        private readonly ILogger<AccountGrpcService> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new Account gRPC Service
        /// </summary>
        /// <param name="logger">Logger object</param>
        public AccountGrpcService(ILogger<AccountGrpcService> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="request">Account request data</param>
        /// <param name="context">Server call context object</param>
        public override Task<CreateAccountReply> CreateAccount(CreateAccountRequest request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<CreateAccountReply, CreateEntryCommand, Guid?>
            (
                nameof(CreateAccount),
                () =>
                {
                    Guid? categoryId = null;
                    if (Guid.TryParse(request.CategoryId, out Guid id))
                        categoryId = id;
                    return new CreateEntryCommand(request.Name, categoryId);
                },
                (reply, result) => reply.Id = result.Response.Value.ToString(),
                logger: _logger
            );

        /// <summary>
        /// Update an existing account
        /// </summary>
        /// <param name="request">Account request data</param>
        /// <param name="context">Server call context object</param>
        public override Task<Empty> UpdateAccount(UpdateAccountRequest request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<Empty, UpdateEntryCommand, bool>
            (
                nameof(UpdateAccount),
                () =>
                {
                    Guid? categoryId = null;
                    if (Guid.TryParse(request.CategoryId, out Guid id))
                        categoryId = id;
                    return new UpdateEntryCommand(new Guid(request.Id), request.Name, categoryId);
                },
                logger: _logger
            );

        /// <summary>
        /// Enable an account
        /// </summary>
        /// <param name="request">Account request data</param>
        /// <param name="context">Server call context object</param>
        public override Task<Empty> EnableAccount(ChangeStatusAccountRequest request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<Empty, ChangeStatusEntryCommand, bool>
            (
                nameof(DisableAccount),
                () => new(new Guid(request.Id), true),
                logger: _logger
            );

        /// <summary>
        /// Disable an account
        /// </summary>
        /// <param name="request">Account request data</param>
        /// <param name="context">Server call context object</param>
        public override Task<Empty> DisableAccount(ChangeStatusAccountRequest request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<Empty, ChangeStatusEntryCommand, bool>
            (
                nameof(DisableAccount),
                () => new(new Guid(request.Id), false),
                logger: _logger
            );

        /// <summary>
        /// Get an account by id
        /// </summary>
        /// <param name="request">Account request data</param>
        /// <param name="context">Server call context object</param>
        public override Task<AccountDetail> GetAccount(GetAccountRequest request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<AccountDetail, GetEntryByIdCommand, EntryDto>
            (
                nameof(GetAccount),
                () => new(new Guid(request.Id)),
                (reply, result) => result.Response.Map(reply),
                logger: _logger
            );

        /// <summary>
        /// List all accounts
        /// </summary>
        /// <param name="request">Account request data</param>
        /// <param name="context">Server call context object</param>
        public override Task<ListAccountReply> ListAccount(Empty request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<ListAccountReply, ListEntryCommand, IEnumerable<EntryDto>>
            (
                nameof(ListAccount),
                () => new(),
                (reply, result) => reply.Data.Add(result.Response.Map()),
                logger: _logger
            );

        #endregion

    }
}
