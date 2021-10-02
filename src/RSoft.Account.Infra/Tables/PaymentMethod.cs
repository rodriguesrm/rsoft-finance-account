using RSoft.Finance.Contracts.Enum;
using RSoft.Lib.Common.Contracts.Entities;
using RSoft.Lib.Design.Infra.Data;
using RSoft.Lib.Design.Infra.Data.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Infra.Tables
{

    /// <summary>
    /// PaymentMethod table entity
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Class to map database table in EntityFramework")]
    public class PaymentMethod : TableIdNameAuditBase<Guid, PaymentMethod>, ITable, IAuditNavigation<Guid, User>, IActive
    {

        #region Constructors

        /// <summary>
        /// Create a new table instance
        /// </summary>
        public PaymentMethod() : base(Guid.NewGuid(), null)
        {
            Initialize();
        }

        /// <summary>
        /// Create a new table instance
        /// </summary>
        /// <param name="id">User id value</param>
        public PaymentMethod(Guid id) : base(id, null)
        {
            Initialize();
        }

        /// <summary>
        /// Create a new table instance
        /// </summary>
        /// <param name="id">User id text</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.FormatException"></exception>
        /// <exception cref="System.OverflowException"></exception>
        public PaymentMethod(string id) : base()
        {
            Id = new Guid(id);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Payment type
        /// </summary>
        public PaymentTypeEnum PaymentType { get; set; }

        /// <summary>
        /// Active status flag
        /// </summary>
        public bool IsActive { get; set; }

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
        /// Transactions by this payment method
        /// </summary>
        public virtual ICollection<Transaction> Transactions { get; set; }

        #endregion

        #region Local methods

        /// <summary>
        /// Iniatialize objects/properties/fields with default values
        /// </summary>
        private void Initialize()
        {
            IsActive = true;
            Transactions = new HashSet<Transaction>();
        }

        #endregion

        #region Public methods

        #endregion

    }
}