using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSoft.Entry.Infra.Tables;

namespace RSoft.Entry.Infra.Configurations
{

    /// <summary>
    /// Transaction table configuration
    /// </summary>
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {

            builder.ToTable(nameof(Transaction));

            #region PK

            builder.HasKey(k => k.Id);

            #endregion

            #region Columns

            builder.Property(c => c.Year)
                .HasColumnName(nameof(Transaction.Year))
                .HasColumnType("smallint")
                .IsRequired();

            builder.Property(c => c.Month)
                .HasColumnName(nameof(Transaction.Month))
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(c => c.Date)
                .HasColumnName(nameof(Transaction.Date))
                .HasColumnType("date")
                .IsRequired();

            builder.Property(nameof(Transaction.TransactionType))
                .HasColumnName(nameof(Transaction.TransactionType))
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(c => c.Amount)
                .HasColumnName(nameof(Transaction.Amount))
                .HasColumnType("decimal(16,2)")
                .IsRequired();

            builder.Property(c => c.AccountId)
                .IsRequired();

            builder.Property(c => c.PaymentMethodId)
                .IsRequired();

            #endregion

            #region FKs

            builder.HasOne(o => o.AccrualPeriod)
                .WithMany(d => d.Transactions)
                .HasForeignKey(fk => new { fk.Year, fk.Month })
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(AccrualPeriod)}_{nameof(Transaction)}_{nameof(Transaction.Year)}{nameof(Transaction.Month)}");

            builder.HasOne(o => o.CreatedAuthor)
                .WithMany(d => d.CreatedTransactions)
                .HasForeignKey(fk => fk.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(User)}_{nameof(Transaction)}_{nameof(Transaction.CreatedBy)}");

            builder.HasOne(o => o.Account)
                .WithMany(d => d.Transactions)
                .HasForeignKey(fk => fk.AccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(User)}_{nameof(Transaction)}_{nameof(Transaction.AccountId)}");

            builder.HasOne(o => o.PaymentMethod)
                .WithMany(d => d.Transactions)
                .HasForeignKey(fk => fk.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(User)}_{nameof(Transaction)}_{nameof(Transaction.PaymentMethodId)}");

            #endregion

            #region Indexes

            builder
                .HasIndex(i => i.Year)
                .HasDatabaseName($"IX_{nameof(Transaction)}_{nameof(Transaction.Year)}");

            builder
                .HasIndex(i => i.Month)
                .HasDatabaseName($"IX_{nameof(Transaction)}_{nameof(Transaction.Month)}");

            builder
                .HasIndex(i => i.Date)
                .HasDatabaseName($"IX_{nameof(Transaction)}_{nameof(Transaction.Date)}");

            #endregion

        }

    }
}
