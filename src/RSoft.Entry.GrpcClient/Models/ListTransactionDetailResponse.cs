using FluentValidator;
using Grpc.Core;
using RSoft.Entry.Contracts.Models;
using System.Collections.Generic;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Get Transaction response model
    /// </summary>
    public class ListTransactionDetailResponse : RpcReply<IEnumerable<TransactionDto>>
    {

        ///<inheritdoc/>
        public ListTransactionDetailResponse
        (
            StatusCode statusCode,
            IEnumerable<TransactionDto> responseData,
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, responseData, notifications, errorMessage) { }

    }
}
