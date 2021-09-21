using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Lib.Common.Abstractions;
using RSoft.Lib.Common.Contracts.Entities;
using RSoft.Lib.Design.Domain.Entities;
using System;
using RSoft.Lib.Common.ValueObjects;

namespace RSoft.Account.Core.Entities
{

    /// <summary>
    /// Accrual period posting record entity class
    /// </summary>
    public class AccrualPeriod : EntityAuditBase<AccrualPeriod, Guid>, IEntity, IAuditAuthor<Guid>
    {

        #region Constructors

        /// <summary>
        /// Create a new Account instance
        /// </summary>
        public AccrualPeriod() : base() { }

        #endregion

        #region Properties

        /// <summary>
        /// Time course year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Time course month
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Opening balance for the period
        /// </summary>
        public float OpeningBalance { get; set; }

        /// <summary>
        /// Total credit entries
        /// </summary>
        public float TotalCredits { get; set; } = 0.0f;

        /// <summary>
        /// Total debts entries
        /// </summary>
        public float TotalDebts { get; set; } = 0.0f;

        /// <summary>
        /// Balance for the accrual period (TotalCredits - TotalDebts)
        /// </summary>
        public float AccrualPeriodBalance 
            => TotalCredits - TotalDebts;

        /// <summary>
        /// Closing Balance for the period (OpeningBalance + TotalCredits - TotalDebts)
        /// </summary>
        public float ClosingBalance
            => IsClosed ? 0 : OpeningBalance + TotalCredits - TotalDebts;

        /// <summary>
        /// Closed status flag
        /// </summary>
        public bool IsClosed { get; private set; }

        #endregion

        #region Navigation/Lazy

        /// <summary>
        /// Data of the user who made the closure
        /// </summary>
        public AuthorNullable<Guid> ClosedAuthor { get; set; }

        #endregion

        #region Public Methods

        ///<inheritdoc/>
        public override void Validate()
        {

            IStringLocalizer<AccrualPeriod> localizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<AccrualPeriod>>();
            if (CreatedAuthor != null) AddNotifications(CreatedAuthor.Notifications);

            if (Year < 2020 || Year > 2999)
                AddNotification(nameof(AccrualPeriod), localizer["INVALID_YEAR"]);
            if (Month <1 || Month > 12)
                AddNotification(nameof(AccrualPeriod), localizer["INVALID_MONTH"]);

        }

        /// <summary>
        /// Close accrual period
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="totalCredits">Total amount of credit</param>
        /// <param name="totalDebts">Total amount of debt</param>
        public void CloseAccrualPeriod(Guid userId, float totalCredits, float totalDebts)
        {
            IsClosed = true;
            TotalCredits = totalCredits;
            TotalDebts = totalDebts;
            ClosedAuthor = new AuthorNullable<Guid>(userId, "***");
        }

        #endregion
    }
}
