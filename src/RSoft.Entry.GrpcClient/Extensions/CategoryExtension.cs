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

namespace RSoft.Entry.GrpcClient.Extensions
{

    /// <summary>
    /// Category extensions methods
    /// </summary>
    public static class CategoryExtension
    {

        /// <summary>
        /// Map create-category-reply model to create-category-response model
        /// </summary>
        /// <param name="reply">CreateCategoryReply instance</param>
        public static CreateCategoryResponse ToCreateCategoryResponse(this CreateCategoryReply reply)
            => new CreateCategoryResponse(StatusCode.OK, new Guid(reply.Id));

        /// <summary>
        /// Map RpcException to create-category-response model
        /// </summary>
        /// <param name="rpcEx">Rpc exception object instance</param>
        public static CreateCategoryResponse ToCreateCategoryResponse(this RpcException rpcEx)
        {

            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(GrpcCategoryServiceProvider.CreateCategory), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new CreateCategoryResponse
            (
                rpcEx.StatusCode,
                null,
                notifications,
                errorMessage
            );

        }

        /// <summary>
        /// Map RpcException to create-category-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static CreateCategoryResponse ToCreateCategoryResponse(this Exception ex)
            => new CreateCategoryResponse
                (
                    StatusCode.Internal,
                    null,
                    errorMessage: ex.Message
                );

        /// <summary>
        /// Map RpcException to update-category-response model
        /// </summary>
        /// <param name="rpcEx">RpcException object instance</param>
        public static UpdateCategoryResponse ToUpdateCategoryResponse(this RpcException rpcEx)
        {
            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(GrpcCategoryServiceProvider.UpdateCategory), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new UpdateCategoryResponse
            (
                rpcEx.StatusCode,
                notifications,
                errorMessage
            );
        }

        /// <summary>
        /// Map RpcException to update-category-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static UpdateCategoryResponse ToUpdateCategoryResponse(this Exception ex)
            => new UpdateCategoryResponse
                (
                    StatusCode.Internal,
                    null,
                    errorMessage: ex.Message
                );

        /// <summary>
        /// Map RpcException to change-category-status-response model
        /// </summary>
        /// <param name="rpcEx">RpcException object instance</param>
        /// <param name="methodName">Method name (for notification)</param>
        public static ChangeCategoryStatusResponse ToChangeCategoryStatusResponse(this RpcException rpcEx, string methodName)
        {

            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(methodName, rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new ChangeCategoryStatusResponse
            (
                rpcEx.StatusCode,
                notifications,
                errorMessage
            );

        }

        /// <summary>
        /// Map Exception to change-category-status-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static ChangeCategoryStatusResponse ToChangeCategoryStatusResponse(this Exception ex)
            =>  new ChangeCategoryStatusResponse
                (
                    StatusCode.Internal,
                    errorMessage: ex.Message
                );

        /// <summary>
        /// Map category-detail model to category-dto-model
        /// </summary>
        /// <param name="detail">Category detail model instance</param>
        public static CategoryDto Map(this CategoryDetail detail)
        {
            CategoryDto dto = new CategoryDto()
            {
                Id = new Guid(detail.Id),
                Name = detail.Name,
                IsActive = detail.IsActive,
                CreatedBy = new AuditAuthor<Guid>(detail.CreatedOn.ToDateTime(), new Guid(detail.CreatedBy.Id), detail.CreatedBy.Name)
            };
            if (detail.ChangedBy != null)
                dto.ChangedBy = new AuditAuthor<Guid>(detail.ChangedOn.Data.ToDateTime(), new Guid(detail.ChangedBy.Data.Id), detail.ChangedBy.Data.Name);
            return dto;
        }

        /// <summary>
        /// Map category-detail model to category-detail-response model
        /// </summary>
        /// <param name="detail">Category detail model instance</param>
        public static CategoryDetailResponse ToCategoryDetailResponse(this CategoryDetail detail)
            => new CategoryDetailResponse
            (
                StatusCode.OK,
                detail.Map()
            );

        /// <summary>
        /// Map RpcException to category-detail-response model
        /// </summary>
        /// <param name="rpcEx">RpcException object instance</param>
        public static CategoryDetailResponse ToCategoryDetailResponse(this RpcException rpcEx)
        {
            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(GrpcCategoryServiceProvider.GetCategory), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new CategoryDetailResponse
            (
                rpcEx.StatusCode,
                null,
                notifications,
                errorMessage
            );
        }

        /// <summary>
        /// Map RpcException to category-detail-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static CategoryDetailResponse ToCategoryDetailResponse(this Exception ex)
            => new CategoryDetailResponse
            (
                StatusCode.Internal,
                null,
                errorMessage: ex.Message
            );

        /// <summary>
        /// Map list-category-reply model to list-category-detail-response model
        /// </summary>
        /// <param name="reply">ListCategoryReply object instance</param>
        public static ListCategoryDetailResponse ToListCategoryDetailResponse(this ListCategoryReply reply )
            => new ListCategoryDetailResponse
            (
                StatusCode.OK,
                reply.Data.Select(s => s.Map()).ToList()
            );

        /// <summary>
        /// Map RpcException to category-detail-response model
        /// </summary>
        /// <param name="rpcEx">RpcException object instance</param>
        public static ListCategoryDetailResponse ToListCategoryDetailResponse(this RpcException rpcEx)
        {
            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(GrpcCategoryServiceProvider.ListCategory), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new ListCategoryDetailResponse
            (
                rpcEx.StatusCode,
                null,
                notifications,
                errorMessage
            );
        }

        /// <summary>
        /// Map RpcException to category-detail-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static ListCategoryDetailResponse ToListCategoryDetailResponse(this Exception ex)
            => new ListCategoryDetailResponse
            (
                StatusCode.Internal,
                null,
                errorMessage: ex.Message
            );

    }
}