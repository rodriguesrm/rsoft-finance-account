using System;
using RSoft.Account.Infra.Tables;
using RSoft.Lib.Common.Contracts.Entities;
using RSoft.Lib.Common.ValueObjects;
using RSoft.Lib.Design.Infra.Data;

namespace RSoft.Account.Infra.Extensions
{

    /// <summary>
    /// Provides author map extensions method
    /// </summary>
    public static class AuthorExtension
    {

        /// <summary>
        /// Add mapped authors in domain entity from table entity
        /// </summary>
        /// <param name="entity">Domain entity object</param>
        /// <param name="table">Table entity object</param>
        public static void MapAuthor(this IAuditAuthor<Guid> entity, IAuditNavigation<Guid, User> table)
        {
            entity.CreatedAuthor = new Author<Guid>(table.CreatedBy, table.CreatedAuthor?.GetFullName());
            if (table.ChangedBy != null)
                entity.ChangedAuthor = new AuthorNullable<Guid>(table.ChangedBy.Value, table.ChangedAuthor?.GetFullName() ?? "***");
        }

        public static void MapAuthor(this IAuditNavigation<Guid, User> table, IAuditAuthor<Guid> entity)
        {
            if (table.CreatedBy == Guid.Empty && entity.CreatedAuthor != null)
                table.CreatedBy = entity.CreatedAuthor.Id;
            if (entity.ChangedAuthor != null && entity.ChangedAuthor.Id.HasValue)
                table.ChangedBy = entity.ChangedAuthor.Id.Value;
        }

    }

}
