using Google.Protobuf.WellKnownTypes;
using FluentValidator;
using Grpc.Core;
using System.Collections.Generic;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Update category response model
    /// </summary>
    public class UpdateCategoryResponse : RpcReply<Empty>
    {

        ///<inheritdoc/>
        public UpdateCategoryResponse
        (
            StatusCode statusCode,
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, new Empty(), notifications, errorMessage) { }

    }
}
