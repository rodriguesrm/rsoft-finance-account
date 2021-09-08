using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using System;
using System.Threading.Tasks;
using RSoft.Account.Grpc.Account;

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

        #endregion

    }
}
