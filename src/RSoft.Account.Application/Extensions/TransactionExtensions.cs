using RSoft.Account.Contracts.Commands;
using RSoft.Account.Core.Entities;
using RSoft.Finance.Contracts.Events;

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
            Transaction result = null;

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
                (int)entity.TransactionType.Value,
                entity.Amount,
                entity.Account.Id,
                entity.PaymentMethod.Id
            );

    }

}
