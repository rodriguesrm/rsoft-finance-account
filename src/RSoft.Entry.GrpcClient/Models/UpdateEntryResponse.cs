using Google.Protobuf.WellKnownTypes;
using FluentValidator;
using Grpc.Core;
using System.Collections.Generic;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Update Entry response model
    /// </summary>
    public class UpdateEntryResponse : RpcReply<Empty>
    {

        ///<inheritdoc/>
        public UpdateEntryResponse
        (
            StatusCode statusCode,
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, new Empty(), notifications, errorMessage) { }

    }
}
