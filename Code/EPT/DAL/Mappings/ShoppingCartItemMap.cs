using System.Data.Entity.ModelConfiguration;
using EPT.DAL.DomainClasses;

namespace EPT.DAL.Mappings
{
    public class ShoppingCartItemMap : EntityTypeConfiguration<ShoppingCartItem>
    {
        public ShoppingCartItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ShoppingCartItemID);

            // Properties
            this.Property(t => t.ShoppingCartID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ShoppingCartItem", "Sales");
            this.Property(t => t.ShoppingCartItemID).HasColumnName("ShoppingCartItemID");
            this.Property(t => t.ShoppingCartID).HasColumnName("ShoppingCartID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ShoppingCartItems)
                .HasForeignKey(d => d.ProductID);

        }
    }
}
