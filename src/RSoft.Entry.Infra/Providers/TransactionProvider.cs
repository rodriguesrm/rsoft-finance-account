using Microsoft.EntityFrameworkCore;
using RSoft.Entry.Core.Ports;
using RSoft.Entry.Infra.Extensions;
using RSoft.Entry.Infra.Tables;
using RSoft.Lib.Design.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactionDomain = RSoft.Entry.Core.Entities.Transaction;

namespace RSoft.Entry.Infra.Providers
{

    /// <summary>
    /// Transaction provider
    /// </summary>
    public class TransactionProvider : RepositoryBase<TransactionDomain, Transaction, Guid>, ITransactionProvider
    {

        #region Constructors

        ///<inheritdoc/>
        public TransactionProvider(EntryContext ctx) : base(ctx) { }

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

        #region Public methods

        ///<inheritdoc/>
        public Task<IEnumerable<TransactionDomain>> GetByFilterAsync(IListTransactionFilter filter, CancellationToken cancellationToken)
        {

            IQueryable<Transaction> query = _dbSet
                .Include(x => x.Entry)
                .Include(x => x.PaymentMethod);

            if (filter.Year.HasValue)
                query = query.Where(t => t.Year == filter.Year.Value && t.Month == filter.Month.Value);
            else
                query = query.Where(t => t.Date >= filter.StartAt.Value && t.Date <= filter.EndAt.Value);


            if (filter.EntryId.HasValue)
                query = query.Where(t => t.EntryId == filter.EntryId.Value);

            if (filter.TransactionType.HasValue)
                query = query.Where(t => t.TransactionType == filter.TransactionType.Value);

            if (filter.PaymentMethodId.HasValue)
                query = query.Where(t => t.PaymentMethodId == filter.PaymentMethodId.Value);

            IEnumerable<Transaction> entities = query.ToList();
            IEnumerable<TransactionDomain> result = entities.Select(e => e.Map(false, true));

            return Task.FromResult(result);

        }

        #endregion

    }
}