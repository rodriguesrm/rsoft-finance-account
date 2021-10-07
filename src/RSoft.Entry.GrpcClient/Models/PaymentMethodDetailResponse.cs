using FluentValidator;
using Grpc.Core;
using RSoft.Entry.Contracts.Models;
using System.Collections.Generic;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Get PaymentMethod response model
    /// </summary>
    public class PaymentMethodDetailResponse : RpcReply<PaymentMethodDto>
    {

        ///<inheritdoc/>
        public PaymentMethodDetailResponse
        (
            StatusCode statusCode,
            PaymentMethodDto responseData,
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, responseData, notifications, errorMessage) { }

    }
}
