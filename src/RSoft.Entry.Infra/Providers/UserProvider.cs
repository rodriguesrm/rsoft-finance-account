using RSoft.Entry.Core.Ports;
using RSoft.Entry.Infra.Extensions;
using RSoft.Entry.Infra.Tables;
using RSoft.Lib.Design.Infra.Data;
using System;
using UserDomain = RSoft.Entry.Core.Entities.User;

namespace RSoft.Entry.Infra.Providers
{

    /// <summary>
    /// User provider
    /// </summary>
    public class UserProvider : RepositoryBase<UserDomain, User, Guid>, IUserProvider
    {

        #region Constructors

        ///<inheritdoc/>
        public UserProvider(EntryContext ctx) : base(ctx) { }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override UserDomain Map(User table)
            => table.Map();

        ///<inheritdoc/>
        protected override User MapForAdd(UserDomain entity)
            => entity.Map();

        ///<inheritdoc/>
        protected override User MapForUpdate(UserDomain entity, User table)
            => entity.Map(table);

        #endregion

    }
}
