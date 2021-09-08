using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using System;
using System.Threading.Tasks;
using RSoft.Account.Grpc.Account;
using RSoft.Account.Contracts.Models;
using RSoft.Account.GrpcService.Extensions;
using System.Collections.Generic;

namespace RSoft.Account.GrpcService.Services
{

    /// <summary>
    /// Account gRPC Service
    /// </summary>
    [Authorize]
    public class AccountGrpcService : Grpc.Account.Account.AccountBase
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
            => GrpcServiceHelpers.SendCommand<CreateAccountReply, CreateAccountCommand, Guid?>
            (
                nameof(CreateAccount),
                () =>
                {
                    Guid? categoryId = null;
                    if (Guid.TryParse(request.CategoryId, out Guid id))
                        categoryId = id;
                    return new CreateAccountCommand(request.Name, categoryId);
                },
                (reply, result) => reply.Id = result.Response.Value.ToString(),
                logger: _logger
            );

        /// <summary>
        /// Update an existing account
        /// </summary>
        /// <param name="request">Account request data</param>
        /// <param name="context">Server call context object</param>
        public override Task<UpdateAccountReply> UpdateAccount(UpdateAccountRequest request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<UpdateAccountReply, UpdateAccountCommand, bool>
            (
                nameof(UpdateAccount),
                () =>
                {
                    Guid? categoryId = null;
                    if (Guid.TryParse(request.CategoryId, out Guid id))
                        categoryId = id;
                    return new UpdateAccountCommand(new Guid(request.Id), request.Name, categoryId);
                },
                logger: _logger
            );

        /// <summary>
        /// Enable an account
        /// </summary>
        /// <param name="request">Account request data</param>
        /// <param name="context">Server call context object</param>
        public override Task<ChangeStatusAccountReply> EnableAccount(ChangeStatusAccountRequest request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<ChangeStatusAccountReply, ChangeStatusAccountCommand, bool>
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
        public override Task<ChangeStatusAccountReply> DisableAccount(ChangeStatusAccountRequest request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<ChangeStatusAccountReply, ChangeStatusAccountCommand, bool>
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
        public override Task<GetAccountReply> GetAccount(GetAccountRequest request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<GetAccountReply, GetAccountByIdCommand, AccountDto>
            (
                nameof(GetAccount),
                () => new(new Guid(request.Id)),
                (reply, result) => reply.Data = result.Response.Map(),
                logger: _logger
            );

        /// <summary>
        /// List all accounts
        /// </summary>
        /// <param name="request">Account request data</param>
        /// <param name="context">Server call context object</param>
        public override Task<ListAccountReply> ListAccount(ListAccountRequest request, ServerCallContext context)
            => GrpcServiceHelpers.SendCommand<ListAccountReply, ListAccountCommand, IEnumerable<AccountDto>>
            (
                nameof(ListAccount),
                () => new(),
                (reply, result) => reply.Data.Add(result.Response.Map()),
                logger: _logger
            );

        #endregion

    }
}
