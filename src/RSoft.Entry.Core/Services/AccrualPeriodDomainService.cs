using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using RSoft.Entry.Core.Entities;
using RSoft.Entry.Core.Ports;
using RSoft.Finance.Contracts.Enum;
using RSoft.Lib.Common.Contracts.Web;
using RSoft.Lib.Common.Models;
using RSoft.Lib.Common.ValueObjects;
using RSoft.Lib.Design.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Entry.Core.Services
{

    /// <summary>
    /// AccrualPeriod domain service operations
    /// </summary>
    public class AccrualPeriodDomainService : DomainServiceBase<AccrualPeriod, Guid, IAccrualPeriodProvider>, IAccrualPeriodDomainService
    {

        #region Local objects/variables

        private readonly IStringLocalizer<AccrualPeriodDomainService> _localizer;
        private readonly ITransactionProvider _transactionProvider;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new scopde domain service instance
        /// </summary>
        /// <param name="provider">AccrualPeriod provider</param>
        /// <param name="transactionProvider">Transaction provider</param>
        /// <param name="authenticatedAccrualPeriod">Authenticated AccrualPeriod object</param>
        /// <param name="localizer">String localizer object</param>
        public AccrualPeriodDomainService
        (
            IAccrualPeriodProvider provider,
            ITransactionProvider transactionProvider,
            IAuthenticatedUser authenticatedAccrualPeriod,
            IStringLocalizer<AccrualPeriodDomainService> localizer
        ) : base(provider, authenticatedAccrualPeriod) 
        {
            _transactionProvider = transactionProvider;
            _localizer = localizer;
        }

        #endregion

        #region Overrides

        ///<inheritdoc/>
        public override void PrepareSave(AccrualPeriod entity, bool isUpdate) 
        {
            if (isUpdate)
            {
                entity.ChangedAuthor = new AuthorNullable<Guid>(_authenticatedUser.Id.Value, $"{_authenticatedUser.FirstName} {_authenticatedUser.LastName}");
                entity.ChangedOn = DateTime.UtcNow;
            }
            else
            {
                entity.CreatedAuthor = new Author<Guid>(_authenticatedUser.Id.Value, $"{_authenticatedUser.FirstName} {_authenticatedUser.LastName}");
                entity.CreatedOn = DateTime.UtcNow;
            }
        }

#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member

        ///<inheritdoc/>
        ///<see cref="GetByKeyAsync(int, int, CancellationToken)"/>
        [Obsolete("This method should not be used for composite primary key entities. Use GetByKeyAsync(int, int, CancellationToken)", true)]
        [ExcludeFromCodeCoverage(Justification = "Obsolete method")]
        public override Task<AccrualPeriod> GetByKeyAsync(Guid key, CancellationToken cancellationToken = default)
            => throw new InvalidOperationException("This method should not be used for composite primary key entities.");

        ///<inheritdoc/>
        ///<see cref="Update(int, int, AccrualPeriod)"/>
        [Obsolete("This method should not be used for composite primary key entities. Use Update(int, int, AccrualPeriod)", true)]
        [ExcludeFromCodeCoverage(Justification = "Obsolete method")]
        public override AccrualPeriod Update(Guid key, AccrualPeriod entity)
            => throw new InvalidOperationException("This method should not be used for composite primary key entities.");

        ///<inheritdoc/>
        ///<see cref="Delete(int, int)"/>
        [Obsolete("This method should not be used for composite primary key entities. Use Delete(int, int)", true)]
        [ExcludeFromCodeCoverage(Justification = "Obsolete method")]
        public override void Delete(Guid key)
            => throw new InvalidOperationException("This method should not be used for composite primary key entities.");

#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public async Task<AccrualPeriod> GetByKeyAsync(int year, int month, CancellationToken cancellationToken = default)
            => await _repository.GetByKeyAsync(year, month, cancellationToken);


        /// <summary>
        /// Update entity on context
        /// </summary>
        /// <param name="year">Year number</param>
        /// <param name="month">Month number</param>
        /// <param name="entity">Entity to update</param>
        public AccrualPeriod Update(int year, int month, AccrualPeriod entity)
        {
            entity.Validate();
            if (entity.Invalid) return entity;
            PrepareSave(entity, true);
            AccrualPeriod savedEntity = _repository.Update(year, month, entity);
            return savedEntity;
        }

        ///<inheritdoc/>
        public void Delete(int year, int month)
            => _repository.Delete(year, month);

        ///<inheritdoc/>
        public async Task<SimpleOperationResult> ClosePeriodAsync(int year, int month, CancellationToken cancellationToken = default)
        {
            //BACKLOG: Use command/events (messaging) to divide operation
            IDictionary<string, string> errors = new Dictionary<string, string>();
            AccrualPeriod accrualPeriod = await _repository.GetByKeyAsync(year, month, cancellationToken);
            if (accrualPeriod == null)
            {
                errors.Add("ClosePeriod", _localizer["ACCRUAL_PERIOD_NOT_FOUND"]);
            }
            else
            {

                if (accrualPeriod.IsClosed)
                {
                    errors.Add("ClosePeriod", _localizer["ACCRUAL_PERIOD_ALREADY_CLOSED"]);
                }
                else
                {

                    IEnumerable<Transaction> transactions =
                        await _transactionProvider.GetByFilterAsync(new ListTransactionFilterArgument(year, month), cancellationToken);

                    float totalCredits = transactions.Where(t => t.TransactionType == TransactionTypeEnum.Credit).Sum(t => t.Amount);
                    float totalDebts = transactions.Where(t => t.TransactionType == TransactionTypeEnum.Debt).Sum(t => t.Amount);

                    accrualPeriod.CloseAccrualPeriod(_authenticatedUser.Id.Value, totalCredits, totalDebts);

                    _repository.Update(year, month, accrualPeriod);

                }

            }
            return new SimpleOperationResult(errors.Count == 0, errors);

        }

        #endregion

        #region Internal classes

        /// <summary>
        /// Transaction filter argument
        /// </summary>
        [ExcludeFromCodeCoverage(Justification = "Anemic class")]
        class ListTransactionFilterArgument : IListTransactionFilter
        {

            /// <summary>
            /// Create a new object instance
            /// </summary>
            /// <param name="year">Year number</param>
            /// <param name="month">Month number</param>
            public ListTransactionFilterArgument(int? year, int? month)
            {
                Year = year;
                Month = month;
            }

            public DateTime? StartAt { get; }
            public DateTime? EndAt { get; }
            public int? Year { get; private set; }
            public int? Month { get; private set; }
            public Guid? EntryId { get; }
            public TransactionTypeEnum? TransactionType { get; }
            public Guid? PaymentMethodId { get; }
        }

        #endregion

    }
}
