using RSoft.Entry.GrpcClient.Models;
using RSoft.Finance.Contracts.Enum;
using System;
using System.Threading.Tasks;

namespace RSoft.Entry.GrpcClient.Providers
{

    /// <summary>
    /// RSoft gRpc Transaction Service provider interface contract
    /// </summary>
    public interface IGrpcTransactionServiceProvider
    {

        /// <summary>
        /// Create a credit transaction
        /// </summary>
        /// <param name="date">Transaction date</param>
        /// <param name="credit">Credit flag indicator (true=Credit/false=Debt)</param>
        /// <param name="amount">Amount value</param>
        /// <param name="comment">Comment for transaction</param>
        /// <param name="entryId">Entry id key value</param>
        /// <param name="paymentMethodId">Payment method id key value</param>
        Task<CreateTransactionResponse> CreateTransaction(DateTime date, bool credit, float amount, string comment, Guid entryId, Guid paymentMethodId);

        /// <summary>
        /// Rollback an existin transaction
        /// </summary>
        /// <param name="transactionId">Transaction id key value</param>
        /// <param name="comment">Comment to add a rollback transaction</param>
        /// <returns></returns>
        Task<RollbackTransactionResponse> RollbackTransaction(Guid transactionId, string comment);

        /// <summary>
        /// Get Transaction by id
        /// </summary>
        /// <param name="id">Transaction id key value</param>
        Task<TransactionDetailResponse> GetTransaction(Guid id);

        /// <summary>
        /// List transaction
        /// </summary>
        /// <param name="startAt">Date start</param>
        /// <param name="endAt">Date ends</param>
        /// <param name="entryId">Entry id key value</param>
        /// <param name="transactionType">Transaction type</param>
        /// <param name="paymentMethodId">Payment method id key value</param>
        Task<ListTransactionDetailResponse> ListTransaction(DateTime startAt, DateTime endAt, Guid? entryId, TransactionTypeEnum? transactionType, Guid? paymentMethodId);

        /// <summary>
        /// List transaction
        /// </summary>
        /// <param name="year">Year number</param>
        /// <param name="month">Month number</param>
        /// <param name="entryId">Entry id key value</param>
        /// <param name="transactionType">Transaction type</param>
        /// <param name="paymentMethodId">Payment method id key value</param>
        Task<ListTransactionDetailResponse> ListTransaction(int year, int month, Guid? entryId, TransactionTypeEnum? transactionType, Guid? paymentMethodId);

    }

}
