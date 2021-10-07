using FluentValidator;
using Grpc.Core;
using RSoft.Entry.Contracts.Models;
using System.Collections.Generic;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Get Transaction response model
    /// </summary>
    public class TransactionDetailResponse : RpcReply<TransactionDto>
    {

        ///<inheritdoc/>
        public TransactionDetailResponse
        (
            StatusCode statusCode,
            TransactionDto responseData,
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, responseData, notifications, errorMessage) { }

    }
}
