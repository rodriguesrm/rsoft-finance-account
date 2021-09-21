using RSoft.Account.Contracts.Models;
using RSoft.Account.Core.Entities;
using RSoft.Lib.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSoft.Account.Application.Extensions
{

    /// <summary>
    /// AccrualPeriod extensions
    /// </summary>
    public static class AccrualPeriodExtension
    {

        /// <summary>
        /// Map entity to dto
        /// </summary>
        /// <param name="entity">AccrualPeriod entity instance</param>
        public static AccrualPeriodDto Map(this AccrualPeriod entity)
        {
            AccrualPeriodDto result = null;
            if (entity != null)
            {
                result = new AccrualPeriodDto()
                {
                    Year = entity.Year,
                    Month = entity.Month,
                    OpeningBalance = entity.OpeningBalance,
                    TotalCredits = entity.TotalCredits,
                    TotalDebts = entity.TotalDebts,
                    AccrualPeriodBalance = entity.AccrualPeriodBalance,
                    ClosingBalance = entity.ClosingBalance,
                    IsClosed = entity.IsClosed,
                    CreatedBy = new AuditAuthor<Guid>(entity.CreatedOn, entity.CreatedAuthor.Id, entity.CreatedAuthor.Name)
                };

                if (entity.ChangedOn.HasValue && entity.ChangedAuthor != null && entity.ChangedAuthor.Id.HasValue)
                    result.ChangedBy =
                        new AuditAuthor<Guid>(entity.ChangedOn.Value, entity.ChangedAuthor.Id.Value, entity.ChangedAuthor.Name);

                if (entity.ClosedAuthor != null)
                    result.ClosedAuthor = new SimpleIdentification<Guid>(entity.ClosedAuthor.Id.Value, entity.ClosedAuthor.Name);

            }
            return result;
        }

        /// <summary>
        /// Map entity list to dto list
        /// </summary>
        /// <param name="entities">Entities list</param>
        public static IEnumerable<AccrualPeriodDto> Map(this IEnumerable<AccrualPeriod> entities)
        {
            IEnumerable<AccrualPeriodDto> result = new List<AccrualPeriodDto>();
            if (entities?.Count() > 0)
                result = entities.Select(e => e.Map());
            return result;
        }

    }

}
