using Microsoft.EntityFrameworkCore;
using RSoft.Account.Infra.Configurations;
using RSoft.Account.Infra.Tables;
using RSoft.Lib.Design.Infra.Data;
using System;

namespace RSoft.Account.Infra
{

    /// <summary>
    /// Account database context
    /// </summary>
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
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentMethodConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        #endregion

        #region DbSets

        /// <summary>
        /// Account dbset
        /// </summary>
        public virtual DbSet<Tables.Account> Accounts { get; set; }

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
