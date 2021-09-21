using RSoft.Account.Infra.Tables;
using RSoft.Lib.Common.ValueObjects;
using System;
using AccrualPeriodDomain = RSoft.Account.Core.Entities.AccrualPeriod;

namespace RSoft.Account.Infra.Extensions
{

    /// <summary>
    /// AccrualPeriod extesions
    /// </summary>
    public static class AccrualPeriodExtension
    {

        /// <summary>
        /// Maps table to entity
        /// </summary>
        /// <param name="table">Table entity to map</param>
        public static AccrualPeriodDomain Map(this AccrualPeriod table)
            => Map(table, true);

        /// <summary>
        /// Maps table to entity
        /// </summary>
        /// <param name="table">Table entity to map</param>
        /// <param name="useLazy">Load related data</param>
        public static AccrualPeriodDomain Map(this AccrualPeriod table, bool useLazy)
        {
            AccrualPeriodDomain result = null;

            if (table != null)
            {

                result = new AccrualPeriodDomain()
                {
                    Year = table.Year,
                    Month = table.Month,
                    OpeningBalance = table.OpeningBalance,
                    TotalCredits = table.TotalCredits,
                    TotalDebts = table.TotalDebts,
                    CreatedOn = table.CreatedOn,
                    ChangedOn = table.ChangedOn
                };

                if (table.IsClosed)
                {
                    result.CloseAccrualPeriod(table.UserIdClosed.Value, table.TotalCredits, table.TotalDebts);
                    if (useLazy)
                        result.ClosedAuthor = new AuthorNullable<Guid>(table.UserIdClosed.Value, table.ClosedAuthor?.GetFullName() ?? "***");
                }

                if (useLazy)
                    result.MapAuthor(table);

            }

            return result;

        }

        /// <summary>
        /// Maps entity to table
        /// </summary>
        /// <param name="entity">Domain entity to map</param>
        public static AccrualPeriod Map(this AccrualPeriodDomain entity)
        {

            AccrualPeriod result = null;

            if (entity != null)
            {
                result = new AccrualPeriod()
                {
                    Year = entity.Year,
                    Month = entity.Month,
                    OpeningBalance = entity.OpeningBalance,
                    TotalCredits = entity.TotalCredits,
                    TotalDebts = entity.TotalDebts,
                    IsClosed = entity.IsClosed,
                    UserIdClosed = entity.ClosedAuthor?.Id,
                    CreatedOn = entity.CreatedOn,
                    CreatedBy = entity.CreatedAuthor.Id
                };
            }

            return result;

        }

        /// <summary>
        /// Maps entity to an existing table
        /// </summary>
        /// <param name="entity">Domain entity to map</param>
        /// <param name="table">Instance of existing table entity</param>
        public static AccrualPeriod Map(this AccrualPeriodDomain entity, AccrualPeriod table)
        {

            if (entity != null && table != null)
            {
                table.Year = entity.Year;
                table.Month = entity.Month;
                table.OpeningBalance = entity.OpeningBalance;
                table.TotalCredits = entity.TotalCredits;
                table.TotalDebts = entity.TotalDebts;
                table.IsClosed = entity.IsClosed;
                table.UserIdClosed = entity.ClosedAuthor?.Id;
            }

            return table;

        }

    }

}
