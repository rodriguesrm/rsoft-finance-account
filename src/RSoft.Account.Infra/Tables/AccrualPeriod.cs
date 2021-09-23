using RSoft.Lib.Design.Infra.Data;
using RSoft.Lib.Design.Infra.Data.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Account.Infra.Tables
{

    /// <summary>
    /// Accrual period posting record table class
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Class to map database table in EntityFramework")]
    public class AccrualPeriod : TableAuditBase<AccrualPeriod, Guid>, ITable, IAuditNavigation<Guid, User>
    {


        #region Constructors

        /// <summary>
        /// Create a new table instance
        /// </summary>
        public AccrualPeriod() : base()
        {
            Transactions = new HashSet<Transaction>();
        }

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
        /// Closed status flag
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// Closing user id
        /// </summary>
        public Guid? UserIdClosed { get; set; }

        #endregion

        #region Navigation/Lazy

        /// <summary>
        /// Created author data
        /// </summary>
        public virtual User CreatedAuthor { get; set; }

        /// <summary>
        /// Changed author data
        /// </summary>
        public virtual User ChangedAuthor { get; set; }

        /// <summary>
        /// Period closing time author
        /// </summary>
        public virtual User ClosedAuthor { get; set; }

        /// <summary>
        /// Transactions for this accrual period
        /// </summary>
        public virtual ICollection<Transaction> Transactions { get; set; }

        #endregion

    }
}
