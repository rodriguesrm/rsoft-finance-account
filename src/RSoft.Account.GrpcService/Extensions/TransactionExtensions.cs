using RSoft.Account.Contracts.Commands;
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
        /// Create command from request
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

    }

}
