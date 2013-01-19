using System.ComponentModel.DataAnnotations.Schema;
using EPT.DAL.DomainClasses;
using System.Data.Entity.ModelConfiguration;

namespace EPT.DAL.Mappings
{
    public class SalesOrderDetailMap : EntityTypeConfiguration<SalesOrderDetail>
    {
        public SalesOrderDetailMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SalesOrderID, t.SalesOrderDetailID });

            // Properties
            this.Property(t => t.SalesOrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SalesOrderDetailID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CarrierTrackingNumber)
                .HasMaxLength(25);

            this.Property(t => t.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("SalesOrderDetail", "Sales");
            this.Property(t => t.SalesOrderID).HasColumnName("SalesOrderID");
            this.Property(t => t.SalesOrderDetailID).HasColumnName("SalesOrderDetailID");
            this.Property(t => t.CarrierTrackingNumber).HasColumnName("CarrierTrackingNumber");
            this.Property(t => t.OrderQty).HasColumnName("OrderQty");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.SpecialOfferID).HasColumnName("SpecialOfferID");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.UnitPriceDiscount).HasColumnName("UnitPriceDiscount");
            this.Property(t => t.LineTotal).HasColumnName("LineTotal");
            this.Property(t => t.rowguid).HasColumnName("rowguid");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");

            // Relationships
            this.HasRequired(t => t.SalesOrderHeader)
                .WithMany(t => t.SalesOrderDetails)
                .HasForeignKey(d => d.SalesOrderID);
            this.HasRequired(t => t.SpecialOfferProduct)
                .WithMany(t => t.SalesOrderDetails)
                .HasForeignKey(d => new { d.SpecialOfferID, d.ProductID });

        }
    }
}
