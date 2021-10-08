using FluentValidator;
using Grpc.Core;
using RSoft.Entry.Contracts.Models;
using RSoft.Entry.Grpc.Protobuf;
using RSoft.Entry.GrpcClient.Models;
using RSoft.Entry.GrpcClient.Providers;
using RSoft.Lib.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSoft.Entry.GrpcClient.Extensions
{

    /// <summary>
    /// Provides Transaction extenson methods
    /// </summary>
    public static class TransactionExtension
    {

        /// <summary>
        /// Map create-transaction-reply model to create-create-response model
        /// </summary>
        /// <param name="reply">CreateTransactionReply instance</param>
        public static CreateTransactionResponse ToCreateTransactionResponse(this CreateTransactionReply reply)
            => new CreateTransactionResponse(StatusCode.OK, new Guid(reply.Id));

        /// <summary>
        /// Map RpcException to create-transaction-response model
        /// </summary>
        /// <param name="rpcEx">Rpc exception object instance</param>
        public static CreateTransactionResponse ToCreateTransactionResponse(this RpcException rpcEx)
        {

            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(GrpcTransactionServiceProvider.CreateTransaction), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new CreateTransactionResponse
            (
                rpcEx.StatusCode,
                null,
                notifications,
                errorMessage
            );

        }

        /// <summary>
        /// Map RpcException to create-transaction-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static CreateTransactionResponse ToCreateTransactionResponse(this Exception ex)
            => new CreateTransactionResponse
                (
                    StatusCode.Internal,
                    null,
                    errorMessage: ex.Message
                );

        /// <summary>
        /// Map transaction-detail model to transaction-dto-model
        /// </summary>
        /// <param name="detail">Transaction detail model instance</param>
        public static TransactionDto Map(this TransactionDetail detail)
        {
            TransactionDto dto = new TransactionDto()
            {
                Id = new Guid(detail.Id),
                Amount = (float)detail.Amount,
                Comment = detail.Comment,
                Date = detail.Date.ToDateTime(),
                TransactionType = new SimpleIdentification<int>(int.Parse(detail.TransactionType.Id), detail.TransactionType.Name),
                PaymentMethod = new SimpleIdentification<Guid>(new Guid(detail.PaymentMethod.Id), detail.PaymentMethod.Name),
                Entry = new SimpleIdentification<Guid>(new Guid(detail.Entry.Id), detail.Entry.Name),
                CreatedBy = new AuditAuthor<Guid>(detail.CreatedOn.ToDateTime(), new Guid(detail.TransactionAuthor.Id), detail.TransactionAuthor.Name)
            };
            return dto;
        }

        /// <summary>
        /// Map Transaction-detail model to Transaction-detail-response model
        /// </summary>
        /// <param name="detail">Transaction detail model instance</param>
        public static TransactionDetailResponse ToTransactionDetailResponse(this TransactionDetail detail)
            => new TransactionDetailResponse
            (
                StatusCode.OK,
                detail.Map()
            );

        /// <summary>
        /// Map RpcException to create-transaction-response model
        /// </summary>
        /// <param name="rpcEx">Rpc exception object instance</param>
        public static TransactionDetailResponse ToTransactionDetailResponse(this RpcException rpcEx)
        {

            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(GrpcTransactionServiceProvider.GetTransaction), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new TransactionDetailResponse
            (
                rpcEx.StatusCode,
                null,
                notifications,
                errorMessage
            );

        }

        /// <summary>
        /// Map RpcException to create-transaction-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static TransactionDetailResponse ToTransactionDetailResponse(this Exception ex)
            => new TransactionDetailResponse
                (
                    StatusCode.Internal,
                    null,
                    errorMessage: ex.Message
                );

        /// <summary>
        /// Map list-Transaction-reply model to list-Transaction-detail-response model
        /// </summary>
        /// <param name="reply">ListTransactionReply object instance</param>
        public static ListTransactionDetailResponse ToListTransactionDetailResponse(this ListTransactionReply reply)
            => new ListTransactionDetailResponse
            (
                StatusCode.OK,
                reply.Data.Select(s => s.Map()).ToList()
            );

        /// <summary>
        /// Map RpcException to Transaction-detail-response model
        /// </summary>
        /// <param name="rpcEx">RpcException object instance</param>
        public static ListTransactionDetailResponse ToListTransactionDetailResponse(this RpcException rpcEx)
        {
            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(GrpcTransactionServiceProvider.ListTransaction), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new ListTransactionDetailResponse
            (
                rpcEx.StatusCode,
                null,
                notifications,
                errorMessage
            );
        }

        /// <summary>
        /// Map RpcException to Transaction-detail-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static ListTransactionDetailResponse ToListTransactionDetailResponse(this Exception ex)
            => new ListTransactionDetailResponse
            (
                StatusCode.Internal,
                null,
                errorMessage: ex.Message
            );

        /// <summary>
        /// Map create-transaction-reply model to rollback-transaction-response model
        /// </summary>
        /// <param name="reply">RollbackTransactionReply instance</param>
        public static RollbackTransactionResponse ToRollbackTransactionResponse(this RollbackTransactionReply reply)
            => new RollbackTransactionResponse(StatusCode.OK, new Guid(reply.Id));

        /// <summary>
        /// Map RpcException to Transaction-detail-response model
        /// </summary>
        /// <param name="rpcEx">RpcException object instance</param>
        public static RollbackTransactionResponse ToRollbackTransactionResponse(this RpcException rpcEx)
        {
            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(GrpcTransactionServiceProvider.RollbackTransaction), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new RollbackTransactionResponse
            (
                rpcEx.StatusCode,
                null,
                notifications,
                errorMessage
            );
        }

        /// <summary>
        /// Map RpcException to Transaction-detail-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static RollbackTransactionResponse ToRollbackTransactionResponse(this Exception ex)
            => new RollbackTransactionResponse
            (
                StatusCode.Internal,
                null,
                errorMessage: ex.Message
            );

    }
}
