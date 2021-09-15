using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RSoft.Account.Contracts.Commands;
using System;
using System.Threading.Tasks;
using RSoft.Account.GrpcService.Extensions;
using RSoft.Account.Grpc.Protobuf;
using proto = RSoft.Account.Grpc.Protobuf;
using RSoft.Account.Contracts.Models;
using System.Collections.Generic;
using RSoft.Account.Contracts.FilterArguments;
using RSoft.Finance.Contracts.Enum;

namespace RSoft.Account.GrpcService.Services
{

    /// <summary>
    /// Transaction gRPC Service
    /// </summary>
    //[Authorize]
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
        /// Get transaction by id
        /// </summary>
        /// <param name="request">Transaction request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<TransactionDetail> GetTransaction(GetTransactionRequest request, ServerCallContext context)
            => await GrpcServiceHelpers.SendCommand<TransactionDetail, GetTransactionByIdCommand, TransactionDto>
            (
                nameof(GetTransaction),
                () => new(new Guid(request.Id)),
                (reply, result) => { result.Response.Map(reply); },
                logger: _logger
            );

        /// <summary>
        /// List transactions applying the filters entered
        /// </summary>
        /// <param name="request">Transaction request data</param>
        /// <param name="context">Server call context object</param>
        public override async Task<ListTransactionReply> ListTransaction(ListTransactionRequest request, ServerCallContext context)
        {
            try
            {
                return await GrpcServiceHelpers.SendCommand<ListTransactionReply, ListTransactionCommand, IEnumerable<TransactionDto>>
                (
                    nameof(ListTransaction),
                    () =>
                    {

                        PeriodDateFilter periodDate = null;
                        if (request.PeriodDate?.Data != null)
                        {

                            DateTime? startAt = null;
                            DateTime? endAt = null;
                            if (request.PeriodDate.Data != null && request.PeriodDate.Data.StartAt.Data != null)
                                startAt = request.PeriodDate.Data.StartAt.Data.ToDateTime();
                            if (request.PeriodDate.Data != null && request.PeriodDate.Data.EndAt.Data != null)
                                endAt = request.PeriodDate.Data.EndAt.Data.ToDateTime();

                            periodDate = new PeriodDateFilter() { StartAt = startAt, EndAt = endAt };
                        }

                        PeriodYearMonthFilter periodYearMonth = null;
                        if (request.PeriodYearMonth?.Data != null)
                            periodYearMonth = new PeriodYearMonthFilter(request.PeriodYearMonth.Data.Year, request.PeriodYearMonth.Data.Month);

                        Guid? accountId = null;
                        if (Guid.TryParse(request.AccountId, out Guid idParsed))
                            accountId = idParsed;

                        TransactionTypeEnum? transactionType = null;
                        if (request.TransactionTypeId.HasValue)
                            transactionType = (TransactionTypeEnum)request.TransactionTypeId.Value;

                        Guid? paymentMethodId = null;
                        if (Guid.TryParse(request.PaymentMethodId, out idParsed))
                            paymentMethodId = idParsed;

                        return new ListTransactionCommand(periodDate, periodYearMonth, accountId, transactionType, paymentMethodId);
                    },
                    (reply, result) => result.Response.Map(reply),
                    logger: _logger
                );
            }
            catch (ArgumentException ex)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
            catch (Exception) { throw; }

        }

        #endregion

    }
}
