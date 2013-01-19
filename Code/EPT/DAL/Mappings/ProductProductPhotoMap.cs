using System.ComponentModel.DataAnnotations.Schema;
using EPT.DAL.DomainClasses;
using System.Data.Entity.ModelConfiguration;

namespace EPT.DAL.Mappings
{
    public class ProductProductPhotoMap : EntityTypeConfiguration<ProductProductPhoto>
    {
        public ProductProductPhotoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductID, t.ProductPhotoID });

            // Properties
            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductPhotoID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductProductPhoto", "Production");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductPhotoID).HasColumnName("ProductPhotoID");
            this.Property(t => t.Primary).HasColumnName("Primary");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductProductPhotoes)
                .HasForeignKey(d => d.ProductID);
            this.HasRequired(t => t.ProductPhoto)
                .WithMany(t => t.ProductProductPhotoes)
                .HasForeignKey(d => d.ProductPhotoID);

        }
    }
}
