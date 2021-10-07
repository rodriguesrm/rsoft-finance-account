using Google.Protobuf.WellKnownTypes;
using FluentValidator;
using Grpc.Core;
using System.Collections.Generic;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Update PaymentMethod response model
    /// </summary>
    public class UpdatePaymentMethodResponse : RpcReply<Empty>
    {

        ///<inheritdoc/>
        public UpdatePaymentMethodResponse
        (
            StatusCode statusCode,
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, new Empty(), notifications, errorMessage) { }

    }
}
