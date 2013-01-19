using EPT.DAL.DomainClasses;
using System.Data.Entity.ModelConfiguration;

namespace EPT.DAL.Mappings
{
    public class ProductSubcategoryMap : EntityTypeConfiguration<ProductSubcategory>
    {
        public ProductSubcategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductSubcategoryID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductSubcategory", "Production");
            this.Property(t => t.ProductSubcategoryID).HasColumnName("ProductSubcategoryID");
            this.Property(t => t.ProductCategoryID).HasColumnName("ProductCategoryID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.rowguid).HasColumnName("rowguid");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");

            // Relationships
            this.HasRequired(t => t.ProductCategory)
                .WithMany(t => t.ProductSubcategories)
                .HasForeignKey(d => d.ProductCategoryID);

        }
    }
}
