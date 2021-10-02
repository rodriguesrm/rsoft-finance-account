using EntryDomain = RSoft.Entry.Core.Entities.Entry;
using EntryTable = RSoft.Entry.Infra.Tables.Entry;
using CategoryDomain = RSoft.Entry.Core.Entities.Category;

namespace RSoft.Entry.Infra.Extensions
{

    /// <summary>
    /// Entry extesions
    /// </summary>
    public static class EntryExtension
    {

        /// <summary>
        /// Maps table to entity
        /// </summary>
        /// <param name="table">Table entity to map</param>
        public static EntryDomain Map(this EntryTable table)
            => Map(table, true);

        /// <summary>
        /// Maps table to entity
        /// </summary>
        /// <param name="table">Table entity to map</param>
        /// <param name="useLazy">Load related data</param>
        public static EntryDomain Map(this EntryTable table, bool useLazy)
        {
            EntryDomain result = null;

            if (table != null)
            {

                result = new EntryDomain(table.Id)
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
        public static EntryTable Map(this EntryDomain entity)
        {

            EntryTable result = null;

            if (entity != null)
            {
                result = new EntryTable(entity.Id)
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
        public static EntryTable Map(this EntryDomain entity, EntryTable table)
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
