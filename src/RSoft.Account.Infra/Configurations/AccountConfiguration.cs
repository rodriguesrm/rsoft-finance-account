using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSoft.Account.Infra.Tables;

namespace RSoft.Account.Infra.Configurations
{

    /// <summary>
    /// Account table configuration
    /// </summary>
    public class AccountConfiguration : IEntityTypeConfiguration<Tables.Account>
    {
        
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<Tables.Account> builder)
        {

            builder.ToTable(nameof(Tables.Account));

            #region PK

            builder.HasKey(k => k.Id);

            #endregion

            #region Columns

            builder.Property(c => c.Name)
                .HasColumnName(nameof(Tables.Account.Name))
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(c => c.CategoryId)
                .IsRequired();

            #endregion

            #region FKs

            builder.HasOne(o => o.CreatedAuthor)
                .WithMany(d => d.CreatedAccounts)
                .HasForeignKey(fk => fk.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(User)}_{nameof(Tables.Account)}_{nameof(Tables.Account.CreatedBy)}");

            builder.HasOne(o => o.ChangedAuthor)
                .WithMany(d => d.ChangedAccounts)
                .HasForeignKey(fk => fk.ChangedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK__{nameof(User)}_{nameof(Tables.Account)}_{nameof(Tables.Account.ChangedBy)}");

            builder.HasOne(o => o.Category)
                .WithMany(d => d.Accounts)
                .HasForeignKey(fk => fk.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(Tables.Account)}_{nameof(Tables.Account.CategoryId)}");

            #endregion

            #region Indexes

            builder
                .HasIndex(i => i.Name)
                .HasDatabaseName($"AK_{nameof(Account)}_{nameof(Tables.Account.Name)}")
                .IsUnique();

            #endregion

        }

    }
}
