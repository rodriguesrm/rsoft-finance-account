using FluentValidator;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Create PaymentMethod response model
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreatePaymentMethodResponse : RpcReply<Guid?>
    {

        ///<inheritdoc/>
        public CreatePaymentMethodResponse
        (
            StatusCode statusCode, 
            Guid? responseData, 
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, responseData, notifications, errorMessage) { }

    }
}
