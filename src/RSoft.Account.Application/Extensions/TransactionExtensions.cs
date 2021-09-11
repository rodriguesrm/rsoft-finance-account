using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Core.Entities;
using RSoft.Finance.Contracts.Enum;
using RSoft.Finance.Contracts.Events;
using RSoft.Helpers.Extensions;
using RSoft.Lib.Common.Models;
using System;
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
        /// Map transaction entity to transaction dto
        /// </summary>
        /// <param name="entity">Entity to map</param>
        public static TransactionDto Map(this Transaction entity)
        {
            TransactionDto result = null;
            if (entity != null)
            {
                result = new TransactionDto()
                {
                    Id = entity.Id,
                    Date = entity.Date,
                    TransactionType = new SimpleIdentification<int>((int)entity.TransactionType.Value, entity.TransactionType.Value.GetDescription()),
                    Amount = entity.Amount,
                    Comment = entity.Comment,
                    Account = new SimpleIdentification<Guid>(entity.Account.Id, entity.Account.Name),
                    PaymentMethod = new SimpleIdentification<Guid>(entity.PaymentMethod.Id, entity.PaymentMethod.Name),
                    CreatedBy = new AuditAuthor<Guid>(entity.CreatedOn, entity.CreatedAuthor.Id, entity.CreatedAuthor.Name)
                };
            }
            return result;
        }

        /// <summary>
        /// Map entity to event
        /// </summary>
        /// <param name="entity">Entity to map</param>
        public static TransactionCreatedEvent MapToEvent(this Transaction entity)
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
