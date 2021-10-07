using FluentValidator;
using Grpc.Core;
using RSoft.Entry.Contracts.Models;
using System.Collections.Generic;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Get PaymentMethod response model
    /// </summary>
    public class ListPaymentMethodDetailResponse : RpcReply<IEnumerable<PaymentMethodDto>>
    {

        ///<inheritdoc/>
        public ListPaymentMethodDetailResponse
        (
            StatusCode statusCode,
            IEnumerable<PaymentMethodDto> responseData,
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, responseData, notifications, errorMessage) { }

    }
}
