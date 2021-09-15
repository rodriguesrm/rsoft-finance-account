using RSoft.Account.Core.Entities;
using RSoft.Lib.Design.Infra.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Core.Ports
{

    public interface ITransactionProvider : IRepositoryBase<Transaction, Guid>
    {

        /// <summary>
        /// Get transactions by filters
        /// </summary>
        /// <param name="filter">Filter arguments data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task<IEnumerable<Transaction>> GetByFilterAsync(IListTransactionFilter filter, CancellationToken cancellationToken = default);

    }
}