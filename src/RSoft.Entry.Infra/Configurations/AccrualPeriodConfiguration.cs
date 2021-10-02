using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSoft.Entry.Infra.Tables;

namespace RSoft.Entry.Infra.Configurations
{

    /// <summary>
    /// AccrualPeriod table configuration
    /// </summary>
    public class AccrualPeriodConfiguration : IEntityTypeConfiguration<AccrualPeriod>
    {
        
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<AccrualPeriod> builder)
        {

            builder.ToTable(nameof(AccrualPeriod));

            #region PK

            builder.HasKey(k => new { k.Year, k.Month });

            #endregion

            #region Columns

            builder.Property(c => c.Year)
                .HasColumnName(nameof(AccrualPeriod.Year))
                .HasColumnType("smallint")
                .IsRequired();

            builder.Property(c => c.Month)
                .HasColumnName(nameof(AccrualPeriod.Month))
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(c => c.OpeningBalance)
                .HasColumnName(nameof(AccrualPeriod.OpeningBalance))
                .IsRequired();

            builder.Property(c => c.TotalCredits)
                .HasColumnName(nameof(AccrualPeriod.TotalCredits))
                .IsRequired();

            builder.Property(c => c.TotalDebts)
                .HasColumnName(nameof(AccrualPeriod.TotalDebts))
                .IsRequired();

            builder.Property(c => c.IsClosed)
                .HasColumnName(nameof(AccrualPeriod.IsClosed))
                .HasColumnType("bit")
                .HasDefaultValue(false)
                .IsRequired();

            #endregion

            #region FKs

            builder.HasOne(o => o.CreatedAuthor)
                .WithMany(d => d.CreatedAccrualPeriods)
                .HasForeignKey(fk => fk.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(User)}_{nameof(AccrualPeriod)}_{nameof(AccrualPeriod.CreatedBy)}");

            builder.HasOne(o => o.ChangedAuthor)
                .WithMany(d => d.ChangedAccrualPeriods)
                .HasForeignKey(fk => fk.ChangedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(User)}_{nameof(AccrualPeriod)}_{nameof(AccrualPeriod.ChangedBy)}");

            builder.HasOne(o => o.ClosedAuthor)
                .WithMany(d => d.ClosedAccrualPeriods)
                .HasForeignKey(fk => fk.UserIdClosed)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(User)}_{nameof(AccrualPeriod)}_{nameof(AccrualPeriod.UserIdClosed)}");

            #endregion

            #region Indexes

            builder
                .HasIndex(i => i.CreatedBy)
                .HasDatabaseName($"IX_{nameof(AccrualPeriod)}_{nameof(AccrualPeriod.CreatedBy)}");

            builder
                .HasIndex(i => i.ChangedBy)
                .HasDatabaseName($"IX_{nameof(AccrualPeriod)}_{nameof(AccrualPeriod.ChangedBy)}");

            builder
                .HasIndex(i => i.UserIdClosed)
                .HasDatabaseName($"IX_{nameof(AccrualPeriod)}_{nameof(AccrualPeriod.UserIdClosed)}");

            #endregion

        }

    }
}
