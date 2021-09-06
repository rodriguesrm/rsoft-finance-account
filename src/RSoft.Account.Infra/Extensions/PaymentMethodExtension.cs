using RSoft.Account.Infra.Tables;
using PaymentMethodDomain = RSoft.Account.Core.Entities.PaymentMethod;

namespace RSoft.Account.Infra.Extensions
{

    /// <summary>
    /// PaymentMethod extesions
    /// </summary>
    public static class PaymentMethodExtension
    {

        /// <summary>
        /// Maps table to entity
        /// </summary>
        /// <param name="table">Table entity to map</param>
        public static PaymentMethodDomain Map(this PaymentMethod table)
            => Map(table, true);

        /// <summary>
        /// Maps table to entity
        /// </summary>
        /// <param name="table">Table entity to map</param>
        /// <param name="useLazy">Load related data</param>
        public static PaymentMethodDomain Map(this PaymentMethod table, bool useLazy)
        {
            PaymentMethodDomain result = null;

            if (table != null)
            {

                result = new PaymentMethodDomain(table.Id)
                {
                    Name = table.Name,
                    PaymentType = table.PaymentType,
                    CreatedOn = table.CreatedOn,
                    ChangedOn = table.ChangedOn,
                    IsActive = table.IsActive
                };

                if (useLazy)
                {
                    result.MapAuthor(table);
                }

            }

            return result;

        }

        /// <summary>
        /// Maps entity to table
        /// </summary>
        /// <param name="entity">Domain entity to map</param>
        public static PaymentMethod Map(this PaymentMethodDomain entity)
        {

            PaymentMethod result = null;

            if (entity != null)
            {
                result = new PaymentMethod(entity.Id)
                {
                    Name = entity.Name,
                    PaymentType = entity.PaymentType.Value,
                    CreatedOn = entity.CreatedOn,
                    CreatedBy = entity.CreatedAuthor.Id,
                    IsActive = entity.IsActive
                };
            }

            return result;

        }

        /// <summary>
        /// Maps entity to an existing table
        /// </summary>
        /// <param name="entity">Domain entity to map</param>
        /// <param name="table">Instance of existing table entity</param>
        public static PaymentMethod Map(this PaymentMethodDomain entity, PaymentMethod table)
        {

            if (entity != null && table != null)
            {
                table.Name = entity.Name;
                table.PaymentType = entity.PaymentType.Value;
                table.ChangedOn = entity.ChangedOn;
                table.ChangedBy = entity.ChangedAuthor.Id;
                table.IsActive = entity.IsActive;
            }

            return table;

        }

    }

}
