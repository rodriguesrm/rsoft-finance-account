using RSoft.Account.Contracts.Models;
using RSoft.Account.Core.Entities;
using RSoft.Lib.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSoft.Account.Application.Extensions
{

    /// <summary>
    /// Category extensions
    /// </summary>
    public static class CategoryExtension
    {

        /// <summary>
        /// Map entity to dto
        /// </summary>
        /// <param name="entity">Category entity instance</param>
        public static CategoryDto Map(this Category entity)
        {
            CategoryDto result = null;
            if (entity != null)
            {
                result = new CategoryDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    IsActive = entity.IsActive,
                    CreatedBy = new AuditAuthor<Guid>(entity.CreatedOn, entity.CreatedAuthor.Id, entity.CreatedAuthor.Name)
                };

                if (entity.ChangedOn.HasValue && entity.ChangedAuthor != null && entity.ChangedAuthor.Id.HasValue)
                    result.ChangedBy =
                        new AuditAuthor<Guid>(entity.ChangedOn.Value, entity.ChangedAuthor.Id.Value, entity.ChangedAuthor.Name);
            }
            return result;
        }

        /// <summary>
        /// Map entity list to dto list
        /// </summary>
        /// <param name="entities">Entities list</param>
        public static IEnumerable<CategoryDto> Map(this IEnumerable<Category> entities)
        {
            IEnumerable<CategoryDto> result = null;
            if (entities?.Count() > 0)
                result = entities.Select(e => e.Map());
            return result;
        }

    }

}
