﻿using RSoft.Account.Core.Entities;
using RSoft.Account.Core.Ports;
using RSoft.Lib.Common.Contracts.Web;
using RSoft.Lib.Common.ValueObjects;
using RSoft.Lib.Design.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Account.Core.Services
{

    /// <summary>
    /// AccrualPeriod domain service operations
    /// </summary>
    public class AccrualPeriodDomainService : DomainServiceBase<AccrualPeriod, Guid, IAccrualPeriodProvider>, IAccrualPeriodDomainService
    {

        #region Constructors

        /// <summary>
        /// Create a new scopde domain service instance
        /// </summary>
        /// <param name="provider">AccrualPeriod provier</param>
        /// <param name="authenticatedAccrualPeriod">Authenticated AccrualPeriod object</param>
        public AccrualPeriodDomainService(IAccrualPeriodProvider provider, IAuthenticatedUser authenticatedAccrualPeriod) : base(provider, authenticatedAccrualPeriod) { }

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

        ///<inheritdoc/>
        ///<see cref="GetByKeyAsync(int, int, CancellationToken)"/>
        [Obsolete("This method should not be used for composite primary key entities. Use GetByKeyAsync(int, int, CancellationToken)", true)]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override Task<AccrualPeriod> GetByKeyAsync(Guid key, CancellationToken cancellationToken = default)
        => throw new InvalidOperationException("This method should not be used for composite primary key entities.");
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public async Task<AccrualPeriod> GetByKeyAsync(int year, int month, CancellationToken cancellationToken = default)
            => await _repository.GetByKeyAsync(year, month, cancellationToken);

        #endregion

    }
}
