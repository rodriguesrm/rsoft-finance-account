using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using System;
using System.Threading.Tasks;
using RSoft.Account.GrpcService.Extensions;
using RSoft.Account.Grpc.Protobuf;
using proto = RSoft.Account.Grpc.Protobuf;

namespace RSoft.Account.GrpcService.Services
{

    /// <summary>
    /// Transaction gRPC Service
    /// </summary>
    [Authorize]
    public class TransactionGrpcService : proto.Transaction.TransactionBase
    {

        #region Local objects/variables

        private readonly ILogger<TransactionGrpcService> _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new Transaction gRPC Service
        /// </summary>
        /// <param name="logger">Logger object</param>
        public TransactionGrpcService(ILogger<TransactionGrpcService> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Create a transaction
        /// </summary>
        /// <param name="request">Transaction request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<CreateTransactionReply> CreateTransaction(CreateTransactionRequest request, ServerCallContext context)
            => await GrpcServiceHelpers.SendCommand<CreateTransactionReply, CreateTransactionCommand, Guid?>
            (
                nameof(CreateTransaction),
                () => request.Map(),
                (reply, result) => reply.Id = result.Response.ToString(),
                logger: _logger
            );

        /// <summary>
        /// Delete a transaction
        /// </summary>
        /// <param name="request">Transaction request data</param>
        /// <param name="context">Server call context object</param>
        public override Task<DeleteTransactionReply> DeleteTransaction(DeleteTransactionRequest request, ServerCallContext context)
        {
            return base.DeleteTransaction(request, context);
        }

        #endregion

    }
}
