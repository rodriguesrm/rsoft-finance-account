using Grpc.Core;
using Microsoft.Extensions.Logging;
using RSoft.Entry.GrpcClient.Abstractions;
using RSoft.Entry.GrpcClient.Extensions;
using RSoft.Entry.GrpcClient.Models;
using System;
using System.Threading.Tasks;
using RSoft.Entry.Grpc.Protobuf;
using Google.Protobuf.WellKnownTypes;
using RSoft.Finance.Contracts.Enum;

namespace RSoft.Entry.GrpcClient.Providers
{

    /// <summary>
    /// RSoft gRpc Transaction Service provider
    /// </summary>
    internal class GrpcTransactionServiceProvider : IGrpcTransactionServiceProvider
    {

        #region Local objects/variables

        private readonly ILogger<GrpcTransactionServiceProvider> _logger;
        private readonly IGrpcChannelFactory _channelFactory;

        private Transaction.TransactionClient _transactionClient = null;
        private string _token = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Creat a new Grpc Provider instance
        /// </summary>
        /// <param name="channelFactory">gRPC Channel factory object instance</param>
        /// <param name="loggerFactory">Logger factory object</param>
        public GrpcTransactionServiceProvider
        (
            IGrpcChannelFactory channelFactory,
            ILoggerFactory loggerFactory
        )
        {
            _channelFactory = channelFactory;
            _logger = loggerFactory?.CreateLogger<GrpcTransactionServiceProvider>();
        }

        #endregion

        #region Local methods

        /// <summary>
        /// List transaction appling filter criteria
        /// </summary>
        /// <param name="startAt">Date start</param>
        /// <param name="endAt">Date ends</param>
        /// <param name="year">Year number</param>
        /// <param name="month">Month number</param>
        /// <param name="entryId">Entry id key value</param>
        /// <param name="transactionType">Transaction type</param>
        /// <param name="paymentMethodId">Payment method id key value</param>
        private async Task<ListTransactionDetailResponse> ListTransaction(DateTime? startAt, DateTime? endAt, int? year, int? month, Guid? entryId, TransactionTypeEnum? transactionType, Guid? paymentMethodId)
        {

            ListTransactionRequest request = new ListTransactionRequest();

            if (startAt.HasValue && endAt.HasValue)
                request.PeriodDate = new NullablePeriodDate()
                {
                    Data = new PeriodDate()
                    {
                        StartAt = new NullableTimestamp() { Data = Timestamp.FromDateTime(startAt.Value) },
                        EndAt = new NullableTimestamp() { Data = Timestamp.FromDateTime(endAt.Value) }
                    }
                };

            if (year.HasValue && month.HasValue)
                request.PeriodYearMonth = new NullablePeriodeYearMonth()
                {
                    Data = new PeriodeYearMonth()
                    {
                        Year = year.Value,
                        Month = month.Value
                    }
                };

            request.EntryId = entryId?.ToString() ?? string.Empty ;
            
            if (transactionType.HasValue)
                request.TransactionType = (int)transactionType.Value;

            request.PaymentMethodId = paymentMethodId?.ToString() ?? string.Empty;

            ListTransactionDetailResponse resp;

            _logger?.LogInformation("Call ListTransaction on gRPC Service on {urlService}", _channelFactory.UrlServer);

            try
            {
                ListTransactionReply reply = await _transactionClient.ListTransactionAsync(request);
                resp = reply.ToListTransactionDetailResponse();
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToListTransactionDetailResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToListTransactionDetailResponse();
            }

            _logger?.LogInformation("ListTransaction Finished");

            return resp;

        }

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public void SetToken(string token)
        {
            _token = token;
            _transactionClient = new Transaction.TransactionClient(_channelFactory.CreateChannel(_token));
        }

        ///<inheritdoc/>
        public async Task<CreateTransactionResponse> CreateTransaction(DateTime date, bool credit, float amount, string comment, Guid entryId, Guid paymentMethodId)
        {

            CreateTransactionResponse resp;
            CreateTransactionRequest request =
                new CreateTransactionRequest()
                {
                    Date = Timestamp.FromDateTime(date),
                    TransactionType = (int)(credit ? TransactionTypeEnum.Credit : TransactionTypeEnum.Debt),
                    Amount = amount,
                    Comment = comment,
                    EntryId = entryId.ToString(),
                    PaymentMethodId = paymentMethodId.ToString()
                };

            _logger?.LogInformation("Call CreateTransaction on gRPC Service on {urlService}", _channelFactory.UrlServer);
            try
            {
                CreateTransactionReply reply = await _transactionClient.CreateTransactionAsync(request);
                resp = reply.ToCreateTransactionResponse();
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToCreateTransactionResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToCreateTransactionResponse();
            }

            _logger?.LogInformation("CreateTransaction Finished");

            return resp;

        }

        ///<inheritdoc/>
        public async Task<RollbackTransactionResponse> RollbackTransaction(Guid transactionId, string comment)
        {

            RollbackTransactionResponse resp;
            RollbackTransactionRequest request = new RollbackTransactionRequest() { Id = transactionId.ToString(), Comment = comment };

            _logger?.LogInformation("Call RollbackTransaction on gRPC Service on {urlService}", _channelFactory.UrlServer);

            try
            {
                RollbackTransactionReply reply = await _transactionClient.RollbackTransactionAsync(request);
                resp = reply.ToRollbackTransactionResponse();
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToRollbackTransactionResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToRollbackTransactionResponse();
            }

            _logger?.LogInformation("RollbackTransaction Finished");

            return resp;

        }

        ///<inheritdoc/>
        public async Task<TransactionDetailResponse> GetTransaction(Guid id)
        {

            GetTransactionRequest request = new GetTransactionRequest() { Id = id.ToString() };
            TransactionDetailResponse resp;

            _logger?.LogInformation("Call GetTransaction on gRPC Service on {urlService}", _channelFactory.UrlServer);


            try
            {
                TransactionDetail reply = await _transactionClient.GetTransactionAsync(request);
                resp = reply.ToTransactionDetailResponse();
            }
            catch (RpcException rpcEx)
            {
                resp = rpcEx.ToTransactionDetailResponse();
            }
            catch (Exception ex)
            {
                resp = ex.ToTransactionDetailResponse();
            }

            _logger?.LogInformation("GetTransaction Finished");

            return resp;

        }

        ///<inheritdoc/>
        public async Task<ListTransactionDetailResponse> ListTransaction(DateTime startAt, DateTime endAt, Guid? entryId = null, TransactionTypeEnum? transactionType = null, Guid? paymentMethodId = null)
            => await ListTransaction(startAt, endAt, null, null, entryId, transactionType, paymentMethodId);

        ///<inheritdoc/>
        public async Task<ListTransactionDetailResponse> ListTransaction(int year, int month, Guid? entryId = null, TransactionTypeEnum? transactionType = null, Guid? paymentMethodId = null)
            => await ListTransaction(null, null, year, month, entryId, transactionType, paymentMethodId);

        #endregion

    }

}
