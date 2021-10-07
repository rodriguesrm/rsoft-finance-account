using RSoft.Entry.GrpcClient.Models;
using System;
using RSoft.Entry.Grpc.Protobuf;
using Grpc.Core;
using System.Collections.Generic;
using FluentValidator;
using RSoft.Entry.Contracts.Models;
using RSoft.Lib.Common.Models;
using System.Linq;
using RSoft.Entry.GrpcClient.Providers;
using RSoft.Finance.Contracts.Enum;

namespace RSoft.Entry.GrpcClient.Extensions
{

    /// <summary>
    /// PaymentMethod extensions methods
    /// </summary>
    public static class PaymentMethodExtension
    {

        /// <summary>
        /// Map create-PaymentMethod-reply model to create-PaymentMethod-response model
        /// </summary>
        /// <param name="reply">CreatePaymentMethodReply instance</param>
        public static CreatePaymentMethodResponse ToCreatePaymentMethodResponse(this CreatePaymentMethodReply reply)
            => new CreatePaymentMethodResponse(StatusCode.OK, new Guid(reply.Id));

        /// <summary>
        /// Map RpcException to create-PaymentMethod-response model
        /// </summary>
        /// <param name="rpcEx">Rpc exception object instance</param>
        public static CreatePaymentMethodResponse ToCreatePaymentMethodResponse(this RpcException rpcEx)
        {

            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(GrpcPaymentMethodServiceProvider.CreatePaymentMethod), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new CreatePaymentMethodResponse
            (
                rpcEx.StatusCode,
                null,
                notifications,
                errorMessage
            );

        }

        /// <summary>
        /// Map RpcException to create-PaymentMethod-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static CreatePaymentMethodResponse ToCreatePaymentMethodResponse(this Exception ex)
            => new CreatePaymentMethodResponse
                (
                    StatusCode.Internal,
                    null,
                    errorMessage: ex.Message
                );

        /// <summary>
        /// Map RpcException to update-PaymentMethod-response model
        /// </summary>
        /// <param name="rpcEx">RpcException object instance</param>
        public static UpdatePaymentMethodResponse ToUpdatePaymentMethodResponse(this RpcException rpcEx)
        {
            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(GrpcPaymentMethodServiceProvider.UpdatePaymentMethod), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new UpdatePaymentMethodResponse
            (
                rpcEx.StatusCode,
                notifications,
                errorMessage
            );
        }

        /// <summary>
        /// Map RpcException to update-PaymentMethod-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static UpdatePaymentMethodResponse ToUpdatePaymentMethodResponse(this Exception ex)
            => new UpdatePaymentMethodResponse
                (
                    StatusCode.Internal,
                    null,
                    errorMessage: ex.Message
                );

        /// <summary>
        /// Map RpcException to change-PaymentMethod-status-response model
        /// </summary>
        /// <param name="rpcEx">RpcException object instance</param>
        /// <param name="methodName">Method name (for notification)</param>
        public static ChangePaymentMethodStatusResponse ToChangePaymentMethodStatusResponse(this RpcException rpcEx, string methodName)
        {

            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(methodName, rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new ChangePaymentMethodStatusResponse
            (
                rpcEx.StatusCode,
                notifications,
                errorMessage
            );

        }

        /// <summary>
        /// Map Exception to change-PaymentMethod-status-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static ChangePaymentMethodStatusResponse ToChangePaymentMethodStatusResponse(this Exception ex)
            =>  new ChangePaymentMethodStatusResponse
                (
                    StatusCode.Internal,
                    errorMessage: ex.Message
                );

        /// <summary>
        /// Map PaymentMethod-detail model to PaymentMethod-dto-model
        /// </summary>
        /// <param name="detail">PaymentMethod detail model instance</param>
        public static PaymentMethodDto Map(this PaymentMethodDetail detail)
        {
            PaymentMethodDto dto = new PaymentMethodDto()
            {
                Id = new Guid(detail.Id),
                Name = detail.Name,
                IsActive = detail.IsActive,
                PaymentType = (PaymentTypeEnum)int.Parse(detail.PaymentType.Id),
                CreatedBy = new AuditAuthor<Guid>(detail.CreatedOn.ToDateTime(), new Guid(detail.CreatedBy.Id), detail.CreatedBy.Name)
            };
            if (detail.ChangedBy != null)
                dto.ChangedBy = new AuditAuthor<Guid>(detail.ChangedOn.Data.ToDateTime(), new Guid(detail.ChangedBy.Data.Id), detail.ChangedBy.Data.Name);
            return dto;
        }

        /// <summary>
        /// Map PaymentMethod-detail model to PaymentMethod-detail-response model
        /// </summary>
        /// <param name="detail">PaymentMethod detail model instance</param>
        public static PaymentMethodDetailResponse ToPaymentMethodDetailResponse(this PaymentMethodDetail detail)
            => new PaymentMethodDetailResponse
            (
                StatusCode.OK,
                detail.Map()
            );

        /// <summary>
        /// Map RpcException to PaymentMethod-detail-response model
        /// </summary>
        /// <param name="rpcEx">RpcException object instance</param>
        public static PaymentMethodDetailResponse ToPaymentMethodDetailResponse(this RpcException rpcEx)
        {
            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(GrpcPaymentMethodServiceProvider.GetPaymentMethod), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new PaymentMethodDetailResponse
            (
                rpcEx.StatusCode,
                null,
                notifications,
                errorMessage
            );
        }

        /// <summary>
        /// Map RpcException to PaymentMethod-detail-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static PaymentMethodDetailResponse ToPaymentMethodDetailResponse(this Exception ex)
            => new PaymentMethodDetailResponse
            (
                StatusCode.Internal,
                null,
                errorMessage: ex.Message
            );

        /// <summary>
        /// Map list-PaymentMethod-reply model to list-PaymentMethod-detail-response model
        /// </summary>
        /// <param name="reply">ListPaymentMethodReply object instance</param>
        public static ListPaymentMethodDetailResponse ToListPaymentMethodDetailResponse(this ListPaymentMethodReply reply )
            => new ListPaymentMethodDetailResponse
            (
                StatusCode.OK,
                reply.Data.Select(s => s.Map()).ToList()
            );

        /// <summary>
        /// Map RpcException to PaymentMethod-detail-response model
        /// </summary>
        /// <param name="rpcEx">RpcException object instance</param>
        public static ListPaymentMethodDetailResponse ToListPaymentMethodDetailResponse(this RpcException rpcEx)
        {
            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(GrpcPaymentMethodServiceProvider.GetPaymentMethod), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new ListPaymentMethodDetailResponse
            (
                rpcEx.StatusCode,
                null,
                notifications,
                errorMessage
            );
        }

        /// <summary>
        /// Map RpcException to PaymentMethod-detail-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static ListPaymentMethodDetailResponse ToListPaymentMethodDetailResponse(this Exception ex)
            => new ListPaymentMethodDetailResponse
            (
                StatusCode.Internal,
                null,
                errorMessage: ex.Message
            );

    }
}