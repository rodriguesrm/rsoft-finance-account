using RSoft.Account.Core.Ports;
using RSoft.Account.Infra.Extensions;
using RSoft.Account.Infra.Tables;
using RSoft.Lib.Design.Infra.Data;
using System;
using UserDomain = RSoft.Account.Core.Entities.User;

namespace RSoft.Account.Infra.Providers
{

    /// <summary>
    /// User provider
    /// </summary>
    public class UserProvider : RepositoryBase<UserDomain, User, Guid>, IUserProvider
    {

        #region Constructors

        ///<inheritdoc/>
        public UserProvider(AccountContext ctx) : base(ctx) { }

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
