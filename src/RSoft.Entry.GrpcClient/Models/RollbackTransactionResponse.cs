using FluentValidator;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Create Transaction response model
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RollbackTransactionResponse : RpcReply<Guid?>
    {

        ///<inheritdoc/>
        public RollbackTransactionResponse
        (
            StatusCode statusCode, 
            Guid? responseData, 
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, responseData, notifications, errorMessage) { }

    }
}
