using RSoft.Entry.Core.Ports;
using RSoft.Entry.Infra.Extensions;
using RSoft.Lib.Design.Infra.Data;
using System;
using AccountDomain = RSoft.Entry.Core.Entities.Entry;
using AccountTable = RSoft.Entry.Infra.Tables.Account;

namespace RSoft.Entry.Infra.Providers
{

    /// <summary>
    /// Account provider
    /// </summary>
    public class AccountProvider : RepositoryBase<AccountDomain, AccountTable, Guid>, IAccountProvider
    {

        #region Constructors

        ///<inheritdoc/>
        public AccountProvider(AccountContext ctx) : base(ctx) { }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override AccountDomain Map(AccountTable table)
            => table.Map();

        ///<inheritdoc/>
        protected override AccountTable MapForAdd(AccountDomain entity)
            => entity.Map();

        ///<inheritdoc/>
        protected override AccountTable MapForUpdate(AccountDomain entity, AccountTable table)
            => entity.Map(table);

        #endregion

    }
}
