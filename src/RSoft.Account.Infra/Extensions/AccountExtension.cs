using AccountDomain = RSoft.Account.Core.Entities.Account;
using AccountTable = RSoft.Account.Infra.Tables.Account;
using CategoryDomain = RSoft.Account.Core.Entities.Category;

namespace RSoft.Account.Infra.Extensions
{

    /// <summary>
    /// Account extesions
    /// </summary>
    public static class AccountExtension
    {

        /// <summary>
        /// Maps table to entity
        /// </summary>
        /// <param name="table">Table entity to map</param>
        public static AccountDomain Map(this AccountTable table)
            => Map(table, true);

        /// <summary>
        /// Maps table to entity
        /// </summary>
        /// <param name="table">Table entity to map</param>
        /// <param name="useLazy">Load related data</param>
        public static AccountDomain Map(this AccountTable table, bool useLazy)
        {
            AccountDomain result = null;

            if (table != null)
            {

                result = new AccountDomain(table.Id)
                {
                    Name = table.Name,
                    CreatedOn = table.CreatedOn,
                    ChangedOn = table.ChangedOn,
                    IsActive = table.IsActive,
                };

                if (useLazy)
                {
                    result.MapAuthor(table);
                    result.Category = table.Category?.Map(false);
                }
                else
                {
                    result.Category = new CategoryDomain(table.CategoryId);
                }

            }

            return result;

        }

        /// <summary>
        /// Maps entity to table
        /// </summary>
        /// <param name="entity">Domain entity to map</param>
        public static AccountTable Map(this AccountDomain entity)
        {

            AccountTable result = null;

            if (entity != null)
            {
                result = new AccountTable(entity.Id)
                {
                    Name = entity.Name,
                    CreatedOn = entity.CreatedOn,
                    CreatedBy = entity.CreatedAuthor.Id,
                    CategoryId = entity.Category.Id,
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
        public static AccountTable Map(this AccountDomain entity, AccountTable table)
        {

            if (entity != null && table != null)
            {
                table.Name = entity.Name;
                table.CategoryId = entity.Category.Id;
                table.ChangedOn = entity.ChangedOn;
                table.ChangedBy = entity.ChangedAuthor.Id;
                table.IsActive = entity.IsActive;
            }

            return table;

        }

    }

}
