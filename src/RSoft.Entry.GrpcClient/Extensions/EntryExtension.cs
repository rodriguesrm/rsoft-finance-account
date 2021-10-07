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
    /// Entry extensions methods
    /// </summary>
    public static class EntryExtension
    {

        /// <summary>
        /// Map create-Entry-reply model to create-Entry-response model
        /// </summary>
        /// <param name="reply">CreateEntryReply instance</param>
        public static CreateEntryResponse ToCreateEntryResponse(this CreateEntryReply reply)
            => new CreateEntryResponse(StatusCode.OK, new Guid(reply.Id));

        /// <summary>
        /// Map RpcException to create-Entry-response model
        /// </summary>
        /// <param name="rpcEx">Rpc exception object instance</param>
        public static CreateEntryResponse ToCreateEntryResponse(this RpcException rpcEx)
        {

            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(GrpcEntryServiceProvider.CreateEntry), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new CreateEntryResponse
            (
                rpcEx.StatusCode,
                null,
                notifications,
                errorMessage
            );

        }

        /// <summary>
        /// Map RpcException to create-Entry-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static CreateEntryResponse ToCreateEntryResponse(this Exception ex)
            => new CreateEntryResponse
                (
                    StatusCode.Internal,
                    null,
                    errorMessage: ex.Message
                );

        /// <summary>
        /// Map RpcException to update-Entry-response model
        /// </summary>
        /// <param name="rpcEx">RpcException object instance</param>
        public static UpdateEntryResponse ToUpdateEntryResponse(this RpcException rpcEx)
        {
            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(GrpcEntryServiceProvider.UpdateEntry), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new UpdateEntryResponse
            (
                rpcEx.StatusCode,
                notifications,
                errorMessage
            );
        }

        /// <summary>
        /// Map RpcException to update-Entry-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static UpdateEntryResponse ToUpdateEntryResponse(this Exception ex)
            => new UpdateEntryResponse
                (
                    StatusCode.Internal,
                    null,
                    errorMessage: ex.Message
                );

        /// <summary>
        /// Map RpcException to change-Entry-status-response model
        /// </summary>
        /// <param name="rpcEx">RpcException object instance</param>
        /// <param name="methodName">Method name (for notification)</param>
        public static ChangeEntryStatusResponse ToChangeEntryStatusResponse(this RpcException rpcEx, string methodName)
        {

            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(methodName, rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new ChangeEntryStatusResponse
            (
                rpcEx.StatusCode,
                notifications,
                errorMessage
            );

        }

        /// <summary>
        /// Map Exception to change-Entry-status-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static ChangeEntryStatusResponse ToChangeEntryStatusResponse(this Exception ex)
            =>  new ChangeEntryStatusResponse
                (
                    StatusCode.Internal,
                    errorMessage: ex.Message
                );

        /// <summary>
        /// Map Entry-detail model to Entry-dto-model
        /// </summary>
        /// <param name="detail">Entry detail model instance</param>
        public static EntryDto Map(this EntryDetail detail)
        {

            EntryDto dto = new EntryDto()
            {
                Id = new Guid(detail.Id),
                Name = detail.Name,
                IsActive = detail.IsActive,
                CreatedBy = new AuditAuthor<Guid>(detail.CreatedOn.ToDateTime(), new Guid(detail.CreatedBy.Id), detail.CreatedBy.Name)
            };

            if (detail.Category.Data != null)
                dto.Category = new SimpleIdentification<Guid>(new Guid(detail.Category.Data.Id), detail.Category.Data.Name);

            if (detail.ChangedBy != null)
                dto.ChangedBy = new AuditAuthor<Guid>(detail.ChangedOn.Data.ToDateTime(), new Guid(detail.ChangedBy.Data.Id), detail.ChangedBy.Data.Name);

            return dto;

        }

        /// <summary>
        /// Map Entry-detail model to Entry-detail-response model
        /// </summary>
        /// <param name="detail">Entry detail model instance</param>
        public static EntryDetailResponse ToEntryDetailResponse(this EntryDetail detail)
            => new EntryDetailResponse
            (
                StatusCode.OK,
                detail.Map()
            );

        /// <summary>
        /// Map RpcException to Entry-detail-response model
        /// </summary>
        /// <param name="rpcEx">RpcException object instance</param>
        public static EntryDetailResponse ToEntryDetailResponse(this RpcException rpcEx)
        {
            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(GrpcEntryServiceProvider.GetEntry), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new EntryDetailResponse
            (
                rpcEx.StatusCode,
                null,
                notifications,
                errorMessage
            );
        }

        /// <summary>
        /// Map RpcException to Entry-detail-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static EntryDetailResponse ToEntryDetailResponse(this Exception ex)
            => new EntryDetailResponse
            (
                StatusCode.Internal,
                null,
                errorMessage: ex.Message
            );

        /// <summary>
        /// Map list-Entry-reply model to list-Entry-detail-response model
        /// </summary>
        /// <param name="reply">ListEntryReply object instance</param>
        public static ListEntryDetailResponse ToListEntryDetailResponse(this ListEntryReply reply )
            => new ListEntryDetailResponse
            (
                StatusCode.OK,
                reply.Data.Select(s => s.Map()).ToList()
            );

        /// <summary>
        /// Map RpcException to Entry-detail-response model
        /// </summary>
        /// <param name="rpcEx">RpcException object instance</param>
        public static ListEntryDetailResponse ToListEntryDetailResponse(this RpcException rpcEx)
        {
            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(GrpcEntryServiceProvider.GetEntry), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new ListEntryDetailResponse
            (
                rpcEx.StatusCode,
                null,
                notifications,
                errorMessage
            );
        }

        /// <summary>
        /// Map RpcException to Entry-detail-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static ListEntryDetailResponse ToListEntryDetailResponse(this Exception ex)
            => new ListEntryDetailResponse
            (
                StatusCode.Internal,
                null,
                errorMessage: ex.Message
            );

    }
}