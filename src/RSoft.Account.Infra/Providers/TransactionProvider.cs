using RSoft.Account.Core.Ports;
using RSoft.Account.Infra.Extensions;
using RSoft.Account.Infra.Tables;
using RSoft.Lib.Design.Infra.Data;
using System;
using TransactionDomain = RSoft.Account.Core.Entities.Transaction;

namespace RSoft.Account.Infra.Providers
{

    /// <summary>
    /// Transaction provider
    /// </summary>
    public class TransactionProvider : RepositoryBase<TransactionDomain, Transaction, Guid>, ITransactionProvider
    {

        #region Constructors

        ///<inheritdoc/>
        public TransactionProvider(AccountContext ctx) : base(ctx) { }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override TransactionDomain Map(Transaction table)
            => table.Map();

        ///<inheritdoc/>
        protected override Transaction MapForAdd(TransactionDomain entity)
            => entity.Map();

        ///<inheritdoc/>
        protected override Transaction MapForUpdate(TransactionDomain entity, Transaction table)
            => entity.Map(table);

        #endregion

    }
}
