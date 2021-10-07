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
    /// Accrual period extensions methods
    /// </summary>
    public static class AccrualPeriodExtension
    {

        #region Public methods

        /// <summary>
        /// Map accrual-period-detail-model to accrual-period-dto model
        /// </summary>
        /// <param name="detail">Object to map</param>
        public static AccrualPeriodDto Map(this AccrualPeriodDetail detail)
        {
            AccrualPeriodDto dto = new AccrualPeriodDto()
            {
                Year = detail.Year,
                Month = detail.Month,
                OpeningBalance = detail.OpeningBalance,
                TotalCredits = detail.TotalCredits,
                TotalDebts = detail.TotalDebts,
                AccrualPeriodBalance = detail.AccrualPeriodBalance,
                ClosingBalance = detail.ClosingBalance,
                IsClosed = detail.IsClosed,
                CreatedBy = new AuditAuthor<Guid>(detail.CreatedOn.ToDateTime(), new Guid(detail.CreatedBy.Id), detail.CreatedBy.Name)
            };

            if (detail.ChangedBy != null)
                dto.ChangedBy = new AuditAuthor<Guid>(detail.ChangedOn.Data.ToDateTime(), new Guid(detail.ChangedBy.Data.Id), detail.ChangedBy.Data.Name);

            if (detail.ClosedAuthor != null)
                dto.ClosedAuthor = new SimpleIdentification<Guid>(new Guid(detail.ClosedAuthor.Data.Id), detail.ClosedAuthor.Data.Name);

            return dto;
        }


        /// <summary>
        /// Map list-accrual-period-reply model to list-accrual-period-detail-response model
        /// </summary>
        /// <param name="reply">ListPeriodReply object instance</param>
        public static ListAccrualPeriodDetailResponse ToListAccrualPeriodDetailResponse(this ListPeriodReply reply)
            => new ListAccrualPeriodDetailResponse
            (
                StatusCode.OK,
                reply.Data.Select(s => s.Map()).ToList()
            );


        /// <summary>
        /// Map RpcException to listaccrual-period-detail-response model
        /// </summary>
        /// <param name="rpcEx">Exception object instance</param>
        public static ListAccrualPeriodDetailResponse ToListAccrualPeriodDetailResponse(this RpcException rpcEx)
        {
            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(nameof(IGrpcAccrualPeriodServiceProvider.ListPeriod), rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new ListAccrualPeriodDetailResponse
            (
                rpcEx.StatusCode,
                null,
                notifications,
                errorMessage
            );
        }

        /// <summary>
        /// Map Exception to list-accrual-period-detail-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static ListAccrualPeriodDetailResponse ToListAccrualPeriodDetailResponse(this Exception ex)
            => new ListAccrualPeriodDetailResponse
            (
                StatusCode.Internal,
                null,
                errorMessage: ex.Message
            );

        /// <summary>
        /// Map RpcException to start-period-detail-response model
        /// </summary>
        /// <param name="rpcEx">Exception object instance</param>
        /// <param name="methodName">Method of name called action</param>
        public static StartPeriodResponse ToStartPeriodResponseResponse(this RpcException rpcEx, string methodName)
        {
            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(methodName, rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new StartPeriodResponse
            (
                rpcEx.StatusCode,
                null,
                notifications,
                errorMessage
            );
        }

        /// <summary>
        /// Map Exception to start-period-detail-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static StartPeriodResponse ToStartPeriodResponseResponse(this Exception ex)
            => new StartPeriodResponse
            (
                StatusCode.Internal,
                null,
                errorMessage: ex.Message
            );

        /// <summary>
        /// Map RpcException to close-period-detail-response model
        /// </summary>
        /// <param name="rpcEx">Exception object instance</param>
        /// <param name="methodName">Method of name called action</param>
        public static ClosePeriodResponse ToClosePeriodResponseResponse(this RpcException rpcEx, string methodName)
        {
            IList<Notification> notifications = null;
            string errorMessage = null;

            if (rpcEx.StatusCode == StatusCode.InvalidArgument)
                notifications = new List<Notification>() { new Notification(methodName, rpcEx.Message) };
            else
                errorMessage = rpcEx.Message;

            return new ClosePeriodResponse
            (
                rpcEx.StatusCode,
                null,
                notifications,
                errorMessage
            );
        }

        /// <summary>
        /// Map Exception to close-period-detail-response model
        /// </summary>
        /// <param name="ex">Exception object instance</param>
        public static ClosePeriodResponse ToClosePeriodResponseResponse(this Exception ex)
            => new ClosePeriodResponse
            (
                StatusCode.Internal,
                null,
                errorMessage: ex.Message
            );

        #endregion

    }

}
