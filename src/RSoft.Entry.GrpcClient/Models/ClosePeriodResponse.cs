using FluentValidator;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Close accrual period response model
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ClosePeriodResponse : RpcReply<Empty>
    {

        ///<inheritdoc/>
        public ClosePeriodResponse
        (
            StatusCode statusCode,
            Empty responseData, 
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, responseData, notifications, errorMessage) { }

    }
}
