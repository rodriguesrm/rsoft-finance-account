using FluentValidator;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Create category response model
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateCategoryResponse : RpcReply<Guid?>
    {

        ///<inheritdoc/>
        public CreateCategoryResponse
        (
            StatusCode statusCode, 
            Guid? responseData, 
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, responseData, notifications, errorMessage) { }

    }
}
