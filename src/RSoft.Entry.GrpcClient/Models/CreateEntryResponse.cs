using FluentValidator;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Create Entry response model
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateEntryResponse : RpcReply<Guid?>
    {

        ///<inheritdoc/>
        public CreateEntryResponse
        (
            StatusCode statusCode, 
            Guid? responseData, 
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, responseData, notifications, errorMessage) { }

    }
}
