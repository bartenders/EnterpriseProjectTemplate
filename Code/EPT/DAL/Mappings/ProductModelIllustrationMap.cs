using System.ComponentModel.DataAnnotations.Schema;
using EPT.DAL.DomainClasses;
using System.Data.Entity.ModelConfiguration;

namespace EPT.DAL.Mappings
{
    public class ProductModelIllustrationMap : EntityTypeConfiguration<ProductModelIllustration>
    {
        public ProductModelIllustrationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductModelID, t.IllustrationID });

            // Properties
            this.Property(t => t.ProductModelID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IllustrationID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductModelIllustration", "Production");
            this.Property(t => t.ProductModelID).HasColumnName("ProductModelID");
            this.Property(t => t.IllustrationID).HasColumnName("IllustrationID");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");

            // Relationships
            this.HasRequired(t => t.Illustration)
                .WithMany(t => t.ProductModelIllustrations)
                .HasForeignKey(d => d.IllustrationID);
            this.HasRequired(t => t.ProductModel)
                .WithMany(t => t.ProductModelIllustrations)
                .HasForeignKey(d => d.ProductModelID);

        }
    }
}
