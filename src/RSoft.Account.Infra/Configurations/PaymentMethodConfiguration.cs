using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSoft.Account.Infra.Tables;

namespace RSoft.Account.Infra.Configurations
{

    /// <summary>
    /// PaymentMethod table configuration
    /// </summary>
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {

            builder.ToTable(nameof(PaymentMethod));

            #region PK

            builder.HasKey(k => k.Id);

            #endregion

            #region Columns

            builder.Property(c => c.Name)
                .HasColumnName(nameof(PaymentMethod.Name))
                .HasMaxLength(80)
                .IsUnicode(false)
                .IsRequired();

            #endregion

            #region FKs

            builder.HasOne(o => o.CreatedAuthor)
                .WithMany(d => d.CreatedPaymentMethods)
                .HasForeignKey(fk => fk.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(User)}_{nameof(PaymentMethod)}_{nameof(PaymentMethod.CreatedBy)}");

            builder.HasOne(o => o.ChangedAuthor)
                .WithMany(d => d.ChangedPaymentMethods)
                .HasForeignKey(fk => fk.ChangedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(User)}_{nameof(PaymentMethod)}_{nameof(PaymentMethod.ChangedBy)}");

            #endregion

            #region Indexes

            builder
                .HasIndex(i => i.Name)
                .HasDatabaseName($"AK_{nameof(PaymentMethod)}_{nameof(PaymentMethod.Name)}")
                .IsUnique();

            #endregion

        }

    }
}
