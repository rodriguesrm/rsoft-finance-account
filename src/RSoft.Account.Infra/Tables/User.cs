using RSoft.Lib.Common.Contracts.Entities;
using RSoft.Lib.Design.Infra.Data.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Account.Infra.Tables
{

    /// <summary>
    /// User table entity
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Class to map database table in EntityFramework")]
    public class User : TableIdBase<Guid, User>, ITable, IActive, IFullName
    {

        #region Constructors

        /// <summary>
        /// Create a new user instance
        /// </summary>
        public User() : base(Guid.NewGuid())
        {
            Initialize();
        }

        /// <summary>
        /// Create a new user instance
        /// </summary>
        /// <param name="id">User id value</param>
        public User(Guid id) : base(id)
        {
            Initialize();
        }

        /// <summary>
        /// Create a new user instance
        /// </summary>
        /// <param name="id">User id text</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.FormatException"></exception>
        /// <exception cref="System.OverflowException"></exception>
        public User(string id) : base()
        {
            Id = new Guid(id);
        }

        #endregion

        #region Properties

        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName{ get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Active status flag
        /// </summary>
        public bool IsActive { get; set; }

        #endregion

        #region Navigation/Lazy

        /// <summary>
        /// Categories created by this user
        /// </summary>
        public virtual ICollection<Category> CreatedCategories { get; set; }

        /// <summary>
        /// Categories changed by this user
        /// </summary>
        public virtual ICollection<Category> ChangedCategories { get; set; }

        /// <summary>
        /// Payment menthods created by this user
        /// </summary>
        public virtual ICollection<PaymentMethod> CreatedPaymentMethods { get; set; }
        
        /// <summary>
        /// Payment methods changed by this user
        /// </summary>
        public virtual ICollection<PaymentMethod> ChangedPaymentMethods { get; set; }

        /// <summary>
        /// Accounts created by this user
        /// </summary>
        public virtual ICollection<Account> CreatedAccounts { get; set; }

        /// <summary>
        /// Accounts changed by this user
        /// </summary>
        public virtual ICollection<Account> ChangedAccounts { get; set; }

        /// <summary>
        /// Transactions created by this user
        /// </summary>
        public virtual ICollection<Transaction> CreatedTransactions { get; set; }

        /// <summary>
        /// Accrual periods created by this user
        /// </summary>
        public virtual ICollection<AccrualPeriod> CreatedAccrualPeriods { get; set; }

        /// <summary>
        /// Accrual periods changed by this user
        /// </summary>
        public virtual ICollection<AccrualPeriod> ChangedAccrualPeriods { get; set; }

        /// <summary>
        /// Accrual periods closed by this user
        /// </summary>
        public virtual ICollection<AccrualPeriod> ClosedAccrualPeriods { get; set; }

        #endregion

        #region Local methods

        /// <summary>
        /// Iniatialize objects/properties/fields with default values
        /// </summary>
        private void Initialize()
        {
            IsActive = true;
            CreatedCategories = new HashSet<Category>();
            ChangedCategories = new HashSet<Category>();
            CreatedPaymentMethods = new HashSet<PaymentMethod>();
            ChangedPaymentMethods = new HashSet<PaymentMethod>();
            CreatedAccounts = new HashSet<Account>();
            ChangedAccounts = new HashSet<Account>();
            CreatedTransactions = new HashSet<Transaction>();
            CreatedAccrualPeriods = new HashSet<AccrualPeriod>();
            ChangedAccrualPeriods = new HashSet<AccrualPeriod>();
            ClosedAccrualPeriods = new HashSet<AccrualPeriod>();
        }

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public string GetFullName()
            => $"{FirstName} {LastName}";

        #endregion

    }
}
