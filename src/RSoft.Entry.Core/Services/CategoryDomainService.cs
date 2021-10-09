using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Common.Contracts.Web;
using RSoft.Lib.Common.ValueObjects;
using RSoft.Lib.Design.Domain.Services;
using System;

namespace RSoft.Entry.Core.Services
{

    /// <summary>
    /// Category domain service operations
    /// </summary>
    public class CategoryDomainService : DomainServiceBase<Category, Guid, ICategoryProvider>, ICategoryDomainService
    {

        #region Constructors

        /// <summary>
        /// Create a new scopde domain service instance
        /// </summary>
        /// <param name="provider">Category provier</param>
        /// <param name="authenticatedCategory">Authenticated Category object</param>
        public CategoryDomainService(ICategoryProvider provider, IAuthenticatedUser authenticatedCategory) : base(provider, authenticatedCategory) { }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        public override void PrepareSave(Category entity, bool isUpdate) 
        {
            if (isUpdate)
            {
                if (entity.ChangedAuthor == null)
                {
                    entity.ChangedAuthor = new AuthorNullable<Guid>(_authenticatedUser.Id.Value, $"{_authenticatedUser.FirstName} {_authenticatedUser.LastName}");
                    entity.ChangedOn = DateTime.UtcNow;
                }
            }
            else
            {
                entity.CreatedAuthor = new Author<Guid>(_authenticatedUser.Id.Value, $"{_authenticatedUser.FirstName} {_authenticatedUser.LastName}");
                entity.CreatedOn = DateTime.UtcNow;
            }
        }

        #endregion

    }
}
