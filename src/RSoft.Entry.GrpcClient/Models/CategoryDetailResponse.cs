using FluentValidator;
using Grpc.Core;
using RSoft.Entry.Contracts.Models;
using System.Collections.Generic;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// Get category response model
    /// </summary>
    public class CategoryDetailResponse : RpcReply<CategoryDto>
    {

        ///<inheritdoc/>
        public CategoryDetailResponse
        (
            StatusCode statusCode,
            CategoryDto responseData,
            ICollection<Notification> notifications = null,
            string errorMessage = null
        ) : base(statusCode, responseData, notifications, errorMessage) { }

    }
}
