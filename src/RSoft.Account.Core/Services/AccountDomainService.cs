using EntryAccount = RSoft.Entry.Core.Entities.Entry;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Common.Contracts.Web;
using RSoft.Lib.Common.ValueObjects;
using RSoft.Lib.Design.Domain.Services;
using System;

namespace RSoft.Entry.Core.Services
{

    /// <summary>
    /// Account domain service operations
    /// </summary>
    public class AccountDomainService : DomainServiceBase<EntryAccount, Guid, IAccountProvider>, IAccountDomainService
    {

        #region Constructors

        /// <summary>
        /// Create a new scopde domain service instance
        /// </summary>
        /// <param name="provider">Account provier</param>
        /// <param name="authenticatedAccount">Authenticated Account object</param>
        public AccountDomainService(IAccountProvider provider, IAuthenticatedUser authenticatedAccount) : base(provider, authenticatedAccount) { }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        public override void PrepareSave(EntryAccount entity, bool isUpdate) 
        {
            if (isUpdate)
            {
                entity.ChangedAuthor = new AuthorNullable<Guid>(_authenticatedUser.Id.Value, $"{_authenticatedUser.FirstName} {_authenticatedUser.LastName}");
                entity.ChangedOn = DateTime.UtcNow;
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
