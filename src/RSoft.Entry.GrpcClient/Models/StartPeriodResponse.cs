using FluentValidator;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Start accrual period response model
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class StartPeriodResponse : RpcReply<Empty>
    {

        ///<inheritdoc/>
        public StartPeriodResponse
        (
            StatusCode statusCode,
            Empty responseData, 
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, responseData, notifications, errorMessage) { }

    }
}
