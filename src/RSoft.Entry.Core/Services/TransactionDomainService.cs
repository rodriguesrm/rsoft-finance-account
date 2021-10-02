using FluentValidator;
using Microsoft.Extensions.Localization;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Lib.Common.Contracts.Web;
using RSoft.Lib.Common.ValueObjects;
using RSoft.Lib.Design.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Core.Services
{

    /// <summary>
    /// Transaction domain service operations
    /// </summary>
    public class TransactionDomainService : DomainServiceBase<Transaction, Guid, ITransactionProvider>, ITransactionDomainService
    {

        #region Local objects/variables

        private readonly IStringLocalizer<TransactionDomainService> _localizer;
        private readonly IAccrualPeriodProvider _accrualPeriodProvider;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new scopde domain service instance
        /// </summary>
        /// <param name="provider">Transaction provier</param>
        /// <param name="authenticatedTransaction">Authenticated Transaction object</param>
        /// <param name="localizer">String localizar object</param>
        /// <param name="accrualPeriodProvider">Accrual period provider object</param>
        public TransactionDomainService
        (
            ITransactionProvider provider, 
            IAuthenticatedUser authenticatedTransaction, 
            IStringLocalizer<TransactionDomainService> localizer, 
            IAccrualPeriodProvider accrualPeriodProvider
        ) : base(provider, authenticatedTransaction)
        {
            _localizer = localizer;
            _accrualPeriodProvider = accrualPeriodProvider;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        public override async Task<Transaction> AddAsync(Transaction entity, CancellationToken cancellationToken = default)
        {
            ValidateAccrualPeriod(entity);
            return await base.AddAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Perform validate accrual period
        /// </summary>
        /// <param name="transaction">Transaction entity object instance</param>
        public void ValidateAccrualPeriod(Transaction transaction)
        {
            if (transaction.Valid)
            {
                AccrualPeriod accrualPeriod = _accrualPeriodProvider.GetByKeyAsync(transaction.Year, transaction.Month).Result;
                if (accrualPeriod == null)
                {
                    transaction.AddNotification(nameof(Transaction), _localizer["ACCRUAL_PERIOD_NOT_FOUND"]);
                }
                else
                {
                    if (accrualPeriod.IsClosed)
                        transaction.AddNotification(nameof(Transaction), _localizer["ACCRUAL_PERIOD_IS_CLOSED"]);
                }
            }
        }

        ///<inheritdoc/>
        public override void PrepareSave(Transaction entity, bool isUpdate) 
        {
            if (!isUpdate)
            { 
                entity.CreatedAuthor = new Author<Guid>(_authenticatedUser.Id.Value, $"{_authenticatedUser.FirstName} {_authenticatedUser.LastName}");
                entity.CreatedOn = DateTime.UtcNow;
            }
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<Transaction>> GetByFilterAsync(IListTransactionFilter filter, CancellationToken cancellationToken)
        {
            if (!filter.IsValid()) throw new ArgumentException(_localizer["LIST_FILTER_INVALID_ARGS"], nameof(filter));
            return await _repository.GetByFilterAsync(filter, cancellationToken);
        }

        #endregion

    }
}
