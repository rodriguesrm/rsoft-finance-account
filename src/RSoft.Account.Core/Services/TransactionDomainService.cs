using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Lib.Common.Contracts.Web;
using RSoft.Lib.Common.ValueObjects;
using RSoft.Lib.Design.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Core.Services
{

    /// <summary>
    /// Transaction domain service operations
    /// </summary>
    public class TransactionDomainService : DomainServiceBase<Transaction, Guid, ITransactionProvider>, ITransactionDomainService
    {

        #region Constructors

        /// <summary>
        /// Create a new scopde domain service instance
        /// </summary>
        /// <param name="provider">Transaction provier</param>
        /// <param name="authenticatedTransaction">Authenticated Transaction object</param>
        public TransactionDomainService(ITransactionProvider provider, IAuthenticatedUser authenticatedTransaction) : base(provider, authenticatedTransaction) { }

        #endregion

        #region Overrides

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
            //TODO: Language
            if (!filter.IsValid()) throw new ArgumentException("Arguments entered conflict or are invalid", nameof(filter));
            return await _repository.GetByFilterAsync(filter, cancellationToken);
        }

        #endregion

    }
}
