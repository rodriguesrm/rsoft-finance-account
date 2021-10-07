using FluentValidator;
using Grpc.Core;
using RSoft.Entry.Contracts.Models;
using System.Collections.Generic;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Get Entry response model
    /// </summary>
    public class ListEntryDetailResponse : RpcReply<IEnumerable<EntryDto>>
    {

        ///<inheritdoc/>
        public ListEntryDetailResponse
        (
            StatusCode statusCode,
            IEnumerable<EntryDto> responseData,
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, responseData, notifications, errorMessage) { }

    }
}
