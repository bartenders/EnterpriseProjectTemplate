using System.ComponentModel.DataAnnotations.Schema;
using EPT.DAL.DomainClasses;
using System.Data.Entity.ModelConfiguration;

namespace EPT.DAL.Mappings
{
    public class ProductInventoryMap : EntityTypeConfiguration<ProductInventory>
    {
        public ProductInventoryMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductID, t.LocationID });

            // Properties
            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LocationID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Shelf)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductInventory", "Production");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.LocationID).HasColumnName("LocationID");
            this.Property(t => t.Shelf).HasColumnName("Shelf");
            this.Property(t => t.Bin).HasColumnName("Bin");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.rowguid).HasColumnName("rowguid");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");

            // Relationships
            this.HasRequired(t => t.Location)
                .WithMany(t => t.ProductInventories)
                .HasForeignKey(d => d.LocationID);
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductInventories)
                .HasForeignKey(d => d.ProductID);

        }
    }
}
