using FluentValidator;
using Grpc.Core;
using RSoft.Entry.Contracts.Models;
using System.Collections.Generic;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Get AccrualPeriod response model
    /// </summary>
    public class ListAccrualPeriodDetailResponse : RpcReply<IEnumerable<AccrualPeriodDto>>
    {

        ///<inheritdoc/>
        public ListAccrualPeriodDetailResponse
        (
            StatusCode statusCode,
            IEnumerable<AccrualPeriodDto> responseData,
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, responseData, notifications, errorMessage) { }

    }
}
