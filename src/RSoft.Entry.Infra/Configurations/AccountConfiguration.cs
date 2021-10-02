using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSoft.Entry.Infra.Tables;

namespace RSoft.Entry.Infra.Configurations
{

    /// <summary>
    /// Account table configuration
    /// </summary>
    public class AccountConfiguration : IEntityTypeConfiguration<Tables.Entry>
    {
        
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<Tables.Entry> builder)
        {

            builder.ToTable(nameof(Tables.Entry));

            #region PK

            builder.HasKey(k => k.Id);

            #endregion

            #region Columns

            builder.Property(c => c.Name)
                .HasColumnName(nameof(Tables.Entry.Name))
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(c => c.CategoryId)
                .IsRequired();

            #endregion

            #region FKs

            builder.HasOne(o => o.CreatedAuthor)
                .WithMany(d => d.CreatedEntries)
                .HasForeignKey(fk => fk.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(User)}_{nameof(Tables.Entry)}_{nameof(Tables.Entry.CreatedBy)}");

            builder.HasOne(o => o.ChangedAuthor)
                .WithMany(d => d.ChangedEntries)
                .HasForeignKey(fk => fk.ChangedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK__{nameof(User)}_{nameof(Tables.Entry)}_{nameof(Tables.Entry.ChangedBy)}");

            builder.HasOne(o => o.Category)
                .WithMany(d => d.Entries)
                .HasForeignKey(fk => fk.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(Tables.Entry)}_{nameof(Tables.Entry.CategoryId)}");

            #endregion

            #region Indexes

            builder
                .HasIndex(i => i.Name)
                .HasDatabaseName($"AK_{nameof(Entry)}_{nameof(Tables.Entry.Name)}")
                .IsUnique();

            #endregion

        }

    }
}
