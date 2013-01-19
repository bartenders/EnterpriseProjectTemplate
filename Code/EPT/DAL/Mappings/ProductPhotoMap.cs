using EPT.DAL.DomainClasses;
using System.Data.Entity.ModelConfiguration;

namespace EPT.DAL.Mappings
{
    public class ProductPhotoMap : EntityTypeConfiguration<ProductPhoto>
    {
        public ProductPhotoMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPhotoID);

            // Properties
            this.Property(t => t.ThumbnailPhotoFileName)
                .HasMaxLength(50);

            this.Property(t => t.LargePhotoFileName)
                .HasMaxLength(50);

            this.Property(t => t.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductPhoto", "Production");
            this.Property(t => t.ProductPhotoID).HasColumnName("ProductPhotoID");
            this.Property(t => t.ThumbNailPhoto).HasColumnName("ThumbNailPhoto");
            this.Property(t => t.ThumbnailPhotoFileName).HasColumnName("ThumbnailPhotoFileName");
            this.Property(t => t.LargePhoto).HasColumnName("LargePhoto");
            this.Property(t => t.LargePhotoFileName).HasColumnName("LargePhotoFileName");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");
        }
    }
}
