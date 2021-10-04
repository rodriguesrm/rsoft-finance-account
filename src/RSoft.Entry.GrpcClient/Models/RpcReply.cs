using FluentValidator;
using Grpc.Core;
using System.Collections.Generic;

namespace RSoft.Entry.GrpcClient.Models
{

    /// <summary>
    /// RpcReply base abstract class
    /// </summary>
    /// <typeparam name="TDto">Type of data-transport-object</typeparam>
    public abstract class RpcReply<TDto> : Notifiable
    {

        #region Constructors

        /// <summary>
        /// Create a new object instance
        /// </summary>
        /// <param name="statusCode">Rpc status code</param>
        /// <param name="responseData">Response data object/value</param>
        /// <param name="notifications">Notifications list</param>
        /// <param name="errorMessage">Error message</param>
        public RpcReply(StatusCode statusCode, TDto responseData, ICollection<Notification> notifications = null, string errorMessage = null)
        {
            StatusCode = statusCode;
            ResponseData = responseData;
            if (notifications != null)
                AddNotifications(notifications);
            ErrorMessage = errorMessage;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Rpc Status
        /// </summary>
        public StatusCode StatusCode { get; private set; }

        /// <summary>
        /// Response data
        /// </summary>
        public TDto ResponseData { get; private set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; private set; }

        #endregion

    }
}
