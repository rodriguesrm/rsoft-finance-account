using RSoft.Lib.Common.Contracts.Entities;
using RSoft.Lib.Common.ValueObjects;
using System;

namespace RSoft.Account.Application.Extensions
{

    /// <summary>
    /// Author extension
    /// </summary>
    public static class AuthorExtension
    {

        /// <summary>
        /// Set service author to entity
        /// </summary>
        /// <param name="entity">Entity to set author</param>
        /// <param name="isUpdate">Update status flag</param>
        public static void SetServiceAuthor(this IAuditAuthor<Guid> entity, bool isUpdate = false)
        {
            Guid userId = new("3f3b94db-d868-4cb3-8098-214a53eccc35");
            string userName = "Account Service";

            if (isUpdate)
                entity.ChangedAuthor = new AuthorNullable<Guid>(userId, userName);
            else
                entity.CreatedAuthor = new Author<Guid>(userId, userName);
        }

    }
}
