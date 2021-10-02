using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSoft.Entry.Infra.Tables;

namespace RSoft.Entry.Infra.Configurations
{

    /// <summary>
    /// Category table configuration
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.ToTable(nameof(Category));

            #region PK

            builder.HasKey(k => k.Id);

            #endregion

            #region Columns

            builder.Property(c => c.Name)
                .HasColumnName(nameof(Category.Name))
                .HasMaxLength(80)
                .IsUnicode(false)
                .IsRequired();

            #endregion

            #region FKs

            builder.HasOne(o => o.CreatedAuthor)
                .WithMany(d => d.CreatedCategories)
                .HasForeignKey(fk => fk.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(User)}_{nameof(Category)}_{nameof(Category.CreatedBy)}");

            builder.HasOne(o => o.ChangedAuthor)
                .WithMany(d => d.ChangedCategories)
                .HasForeignKey(fk => fk.ChangedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName($"FK_{nameof(User)}_{nameof(Category)}_{nameof(Category.ChangedBy)}");

            #endregion

            #region Indexes

            builder
                .HasIndex(i => i.Name)
                .HasDatabaseName($"AK_{nameof(Category)}_{nameof(Category.Name)}")
                .IsUnique();

            #endregion

        }

    }
}
