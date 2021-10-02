using Microsoft.EntityFrameworkCore;
using RSoft.Entry.Infra.Configurations;
using RSoft.Entry.Infra.Tables;
using RSoft.Lib.Design.Infra.Data;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RSoft.Entry.Infra
{

    /// <summary>
    /// Account database context
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "DbContext does not contains rules")]
    public class AccountContext : DbContextBase<Guid>
    {

        #region Constructors

        /// <summary>
        /// Create a new dbcontext instance
        /// </summary>
        /// <param name="options">Context options settings</param>
        public AccountContext(DbContextOptions options) : base(options) { }

        #endregion

        #region Overrides

        protected override void SetTableConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new AccrualPeriodConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        #endregion

        #region DbSets

        /// <summary>
        /// Account dbset
        /// </summary>
        public virtual DbSet<Tables.Account> Accounts { get; set; }

        /// <summary>
        /// Accrual periods dbset
        /// </summary>
        public virtual DbSet<AccrualPeriod> AccrualPeriods { get; set; }

        /// <summary>
        /// Categories dbset
        /// </summary>
        public virtual DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Payment methods dbset
        /// </summary>
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

        /// <summary>
        /// Transaction dbset
        /// </summary>
        public virtual DbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// User dbset
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        #endregion
    }
}
