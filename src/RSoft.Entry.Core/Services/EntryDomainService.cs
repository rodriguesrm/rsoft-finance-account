using DomainEntry = RSoft.Entry.Core.Entities.Entry;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Common.Contracts.Web;
using RSoft.Lib.Common.ValueObjects;
using RSoft.Lib.Design.Domain.Services;
using System;

namespace RSoft.Entry.Core.Services
{

    /// <summary>
    /// Entry domain service operations
    /// </summary>
    public class EntryDomainService : DomainServiceBase<DomainEntry, Guid, IEntryProvider>, IEntryDomainService
    {

        #region Constructors

        /// <summary>
        /// Create a new scopde domain service instance
        /// </summary>
        /// <param name="provider">Entry provider</param>
        /// <param name="authenticatedUser">Authenticated user object</param>
        public EntryDomainService(IEntryProvider provider, IAuthenticatedUser authenticatedUser) : base(provider, authenticatedUser) { }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        public override void PrepareSave(DomainEntry entity, bool isUpdate) 
        {
            if (isUpdate)
            {
                if (entity.ChangedAuthor == null) //TODO: Remove this IF when Consumers was moved to Worker
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
