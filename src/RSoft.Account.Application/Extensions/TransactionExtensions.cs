using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Entities;
using RSoft.Finance.Contracts.Enum;
using RSoft.Finance.Contracts.Events;
using DomainAccount = RSoft.Account.Core.Entities.Account;

namespace RSoft.Account.Application.Extensions
{

    /// <summary>
    /// Transaction extensions
    /// </summary>
    public static class TransactionExtensions
    {

        /// <summary>
        /// Map command to entity
        /// </summary>
        /// <param name="command">Command object instance</param>
        public static Transaction Map(this CreateTransactionCommand command)
        {

            DomainAccount account = null;
            if (command.AccountId.HasValue)
                account = new DomainAccount(command.AccountId.Value);
            PaymentMethod paymentMethod = null;
            if (command.PaymentMethodId.HasValue)
                paymentMethod = new PaymentMethod(command.PaymentMethodId.Value);

            Transaction result = new()
            {
                Date = command.Date,
                TransactionType = (TransactionTypeEnum)command.TransactionType,
                Amount = command.Amount,
                Comment = command.Comment,
                Account = account,
                PaymentMethod = paymentMethod
            };

            return result;
        }

        /// <summary>
        /// Map entity to event
        /// </summary>
        /// <param name="entity">Entity to map</param>
        public static TransactionCreatedEvent Map(this Transaction entity)
            => new
            (
                entity.Id,
                entity.Year,
                entity.Month,
                entity.Date,
                entity.TransactionType.Value,
                entity.Amount,
                entity.Account.Id,
                entity.PaymentMethod.Id
            );

    }

}
