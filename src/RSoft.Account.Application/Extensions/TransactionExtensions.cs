using RSoft.Account.Application.Arguments;
using RSoft.Account.Contracts.Commands;
using RSoft.Account.Contracts.Models;
using RSoft.Account.Core.Entities;
using RSoft.Finance.Contracts.Enum;
using RSoft.Finance.Contracts.Events;
using RSoft.Helpers.Extensions;
using RSoft.Lib.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Map transaction entity list to transaction dto list
        /// </summary>
        /// <param name="entities">Entity list</param>
        public static IEnumerable<TransactionDto> Map(this IEnumerable<Transaction> entities)
            => entities.Select(e => e.Map());

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

        /// <summary>
        /// Map command to filter object
        /// </summary>
        /// <param name="command">Command to map</param>
        public static ListTransactionFilter Map(this ListTransactionCommand command)
        {
            ListTransactionFilter result = new()
               {
                   StartAt = command.PeriodDate?.StartAt ?? null,
                   EndAt = command.PeriodDate?.EndAt ?? null,
                   Year = command.PeriodYearMonth?.Year ?? null,
                   Month = command.PeriodYearMonth?.Month ?? null,
                   AccountId = command.AccountId ?? null,
                   TransactionType = command.TransactionType ?? null,
                   PaymentMethodId = command.PaymentMethodId ?? null
               };
            if (result.StartAt.HasValue)
                result.StartAt = result.StartAt.Value.ToUniversalTime().Date;
            if (result.EndAt.HasValue)
                result.EndAt = result.EndAt.Value.ToUniversalTime().Date;
            return result;
        }

    }

}
