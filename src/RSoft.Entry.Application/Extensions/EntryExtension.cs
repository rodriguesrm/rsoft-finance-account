using RSoft.Entry.Contracts.Models;
using DomainEntry = RSoft.Entry.Core.Entities.Entry;
using RSoft.Lib.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSoft.Entry.Application.Extensions
{

    /// <summary>
    /// Account extensions
    /// </summary>
    public static class EntryExtension
    {

        /// <summary>
        /// Map entity to dto
        /// </summary>
        /// <param name="entity">Account entity instance</param>
        public static AccountDto Map(this DomainEntry entity)
        {
            AccountDto result = null;
            if (entity != null)
            {

                result = new AccountDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    IsActive = entity.IsActive,
                    CreatedBy = new AuditAuthor<Guid>(entity.CreatedOn, entity.CreatedAuthor.Id, entity.CreatedAuthor.Name)
                };

                if (entity.Category != null)
                    result.Category = new SimpleIdentification<Guid>(entity.Category.Id, entity.Category.Name);

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
        public static IEnumerable<AccountDto> Map(this IEnumerable<DomainEntry> entities)
        {
            IEnumerable<AccountDto> result = new List<AccountDto>();
            if (entities?.Count() > 0)
                result = entities.Select(e => e.Map());
            return result;
        }

    }

}
