using Google.Protobuf.WellKnownTypes;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Grpc.Protobuf;
using System;

namespace RSoft.Account.GrpcService.Extensions
{

    /// <summary>
    /// Transaction exteionsons
    /// </summary>
    public static class TransactionExtensions
    {

        /// <summary>
        /// Map command from request
        /// </summary>
        /// <param name="request">Request data</param>
        public static CreateTransactionCommand Map(this CreateTransactionRequest request)
        {

            Guid? accountId = null;
            Guid? paymentMethodId = null;

            if (Guid.TryParse(request.AccountId, out Guid id))
                accountId = id;
            if (Guid.TryParse(request.PaymentMethodId, out id))
                paymentMethodId = id;

            CreateTransactionCommand command = new(request.Date.ToDateTime(), request.TransactionType, (float)request.Amount, request.Comment, accountId, paymentMethodId);
            return command;

        }

        /// <summary>
        /// Map transaction-dto from request
        /// </summary>
        /// <param name="dto">Request to map</param>
        /// <param name="reply">Reply to recieve map</param>
        public static void Map(this TransactionDto dto, TransactionDetail reply)
        {
            if (dto != null)
            {
                reply.Id = dto.Id.ToString();
                reply.Year = dto.Year;
                reply.Month = dto.Month;
                reply.Date = Timestamp.FromDateTime(dto.Date.ToUniversalTime());
                reply.TransactionType = new SimpleIdName() { Id = dto.TransactionType.Id.ToString(), Name = dto.TransactionType.Name };
                reply.Amount = dto.Amount;
                reply.Comment = dto.Comment;
                reply.AccountId = new SimpleIdName() { Id = dto.Account.Id.ToString(), Name = dto.Account.Name };
                reply.PaymentMethodId = new SimpleIdName() { Id = dto.PaymentMethod.Id.ToString(), Name = dto.PaymentMethod.Name };
                reply.TransactionAuthor = new AuthorDetail() { Id = dto.CreatedBy.Id.ToString(), Name = dto.CreatedBy.Name };
            }
        }

    }

}
