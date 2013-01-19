using EPT.DAL.DomainClasses;
using System.Data.Entity.ModelConfiguration;

namespace EPT.DAL.Mappings
{
    public class SalesTaxRateMap : EntityTypeConfiguration<SalesTaxRate>
    {
        public SalesTaxRateMap()
        {
            // Primary Key
            this.HasKey(t => t.SalesTaxRateID);

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
            this.ToTable("SalesTaxRate", "Sales");
            this.Property(t => t.SalesTaxRateID).HasColumnName("SalesTaxRateID");
            this.Property(t => t.StateProvinceID).HasColumnName("StateProvinceID");
            this.Property(t => t.TaxType).HasColumnName("TaxType");
            this.Property(t => t.TaxRate).HasColumnName("TaxRate");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.rowguid).HasColumnName("rowguid");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");

            // Relationships
            this.HasRequired(t => t.StateProvince)
                .WithMany(t => t.SalesTaxRates)
                .HasForeignKey(d => d.StateProvinceID);

        }
    }
}
