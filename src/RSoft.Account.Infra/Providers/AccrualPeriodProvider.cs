using RSoft.Account.Core.Ports;
using RSoft.Account.Infra.Extensions;
using RSoft.Account.Infra.Tables;
using RSoft.Lib.Design.Exceptions;
using RSoft.Lib.Design.Infra.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using AccrualPeriodDomain = RSoft.Account.Core.Entities.AccrualPeriod;

namespace RSoft.Account.Infra.Providers
{

    /// <summary>
    /// AccrualPeriod provider
    /// </summary>
    public class AccrualPeriodProvider : RepositoryBase<AccrualPeriodDomain, AccrualPeriod, Guid>, IAccrualPeriodProvider
    {

        #region Constructors

        ///<inheritdoc/>
        public AccrualPeriodProvider(AccountContext ctx) : base(ctx) { }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        protected override AccrualPeriodDomain Map(AccrualPeriod table)
            => table.Map();

        ///<inheritdoc/>
        protected override AccrualPeriod MapForAdd(AccrualPeriodDomain entity)
            => entity.Map();

        ///<inheritdoc/>
        protected override AccrualPeriod MapForUpdate(AccrualPeriodDomain entity, AccrualPeriod table)
            => entity.Map(table);

#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member

        ///<inheritdoc/>
        ///<see cref="GetByKeyAsync(int, int, CancellationToken)"/>
        [Obsolete("This method should not be used for composite primary key entities. Use GetByKeyAsync(int, int, CancellationToken)", true)]

        public override Task<AccrualPeriodDomain> GetByKeyAsync(Guid key, CancellationToken cancellationToken = default)
            => throw new InvalidOperationException("This method should not be used for composite primary key entities.");

        ///<inheritdoc/>
        ///<see cref="Update(int, int, AccrualPeriodDomain)"/>
        [Obsolete("This method should not be used for composite primary key entities. Use GetByKeyAsync(int, int, AccrualPeriod)", true)]
        public override AccrualPeriodDomain Update(Guid key, AccrualPeriodDomain entity)
            => throw new InvalidOperationException("This method should not be used for composite primary key entities.");

#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public async Task<AccrualPeriodDomain> GetByKeyAsync(int year, int month, CancellationToken cancellationToken = default)
        {

            if (cancellationToken.IsCancellationRequested)
                return null;

            AccrualPeriod table = await Task.Run(() => _dbSet.Find(year, month));
            AccrualPeriodDomain entity = Map(table);

            if (cancellationToken.IsCancellationRequested)
                return null;

            return entity;

        }

        ///<inheritdoc/>
        public AccrualPeriodDomain Update(int year, int month, AccrualPeriodDomain entity)
        {

            if (entity.Invalid)
                throw new InvalidEntityException(nameof(entity));

            AccrualPeriod table = _dbSet.Find(year, month);
            if (table == null)
                throw new InvalidOperationException($"[{year},{month}] The data update operation cannot be completed because the entity does not exist in the database. The same may have been deleted.");

            table = MapForUpdate(entity, table);
            table = _dbSet.Update(table).Entity;

            entity = Map(table);
            return entity;

        }

        #endregion

    }
}
