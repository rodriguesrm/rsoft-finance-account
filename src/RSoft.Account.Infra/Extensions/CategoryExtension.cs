using RSoft.Account.Infra.Tables;
using CategoryDomain = RSoft.Account.Core.Entities.Category;

namespace RSoft.Account.Infra.Extensions
{

    /// <summary>
    /// Category extesions
    /// </summary>
    public static class CategoryExtension
    {

        /// <summary>
        /// Maps table to entity
        /// </summary>
        /// <param name="table">Table entity to map</param>
        public static CategoryDomain Map(this Category table)
            => Map(table, true);

        /// <summary>
        /// Maps table to entity
        /// </summary>
        /// <param name="table">Table entity to map</param>
        /// <param name="useLazy">Load related data</param>
        public static CategoryDomain Map(this Category table, bool useLazy)
        {
            CategoryDomain result = null;

            if (table != null)
            {

                result = new CategoryDomain(table.Id)
                {
                    Name = table.Name,
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
        public static Category Map(this CategoryDomain entity)
        {

            Category result = null;

            if (entity != null)
            {
                result = new Category(entity.Id)
                {
                    Name = entity.Name,
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
        public static Category Map(this CategoryDomain entity, Category table)
        {

            if (entity != null && table != null)
            {
                table.Name = entity.Name;
                table.ChangedOn = entity.ChangedOn;
                table.ChangedBy = entity.ChangedAuthor.Id;
                table.IsActive = entity.IsActive;
            }

            return table;

        }

    }

}
