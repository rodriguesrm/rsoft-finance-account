using FluentValidator;
using Grpc.Core;
using RSoft.Entry.Contracts.Models;
using System.Collections.Generic;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Get Entry response model
    /// </summary>
    public class EntryDetailResponse : RpcReply<EntryDto>
    {

        ///<inheritdoc/>
        public EntryDetailResponse
        (
            StatusCode statusCode,
            EntryDto responseData,
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, responseData, notifications, errorMessage) { }

    }
}
