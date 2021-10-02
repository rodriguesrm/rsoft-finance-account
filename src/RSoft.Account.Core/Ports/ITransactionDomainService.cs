using RSoft.Entry.Core.Entities;
using RSoft.Lib.Design.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Core.Ports
{

    /// <summary>
    /// Transaction domain service interface
    /// </summary>
    public interface ITransactionDomainService : IDomainServiceBase<Transaction, Guid>
    {

        /// <summary>
        /// Get transactions by filters
        /// </summary>
        Task<IEnumerable<Transaction>> GetByFilterAsync(IListTransactionFilter filter, CancellationToken cancellationToken);

        /// <summary>
        /// Perform validate accrual period
        /// </summary>
        /// <param name="transaction">Transaction entity object instance</param>
        void ValidateAccrualPeriod(Transaction transaction);

    }

}