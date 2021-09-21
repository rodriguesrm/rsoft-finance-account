using RSoft.Lib.Common.Contracts.Dtos;
using RSoft.Lib.Common.Dtos;
using RSoft.Lib.Common.Models;
using System;

namespace RSoft.Account.Contracts.Models
{

    /// <summary>
    /// AccrualPeriod data transport object
    /// </summary>
    public class AccrualPeriodDto : AppDtoAuditBase<Guid>, IAuditDto<Guid>
    {

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
        public float TotalCredits { get; set; }

        /// <summary>
        /// Total debts entries
        /// </summary>
        public float TotalDebts { get; set; }

        /// <summary>
        /// Balance for the accrual period (TotalCredits - TotalDebts)
        /// </summary>
        public float AccrualPeriodBalance { get; set; }

        /// <summary>
        /// Closing Balance for the period (OpeningBalance + TotalCredits - TotalDebts)
        /// </summary>
        public float ClosingBalance { get; set; }

        /// <summary>
        /// Closed status flag
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// Closed author data
        /// </summary>
        public SimpleIdentification<Guid> ClosedAuthor { get; set; }

        #endregion

    }

}
