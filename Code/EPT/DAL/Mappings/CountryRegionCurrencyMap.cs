using EPT.DAL.DomainClasses;
using System.Data.Entity.ModelConfiguration;

namespace EPT.DAL.Mappings
{
    public class CountryRegionCurrencyMap : EntityTypeConfiguration<CountryRegionCurrency>
    {
        public CountryRegionCurrencyMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CountryRegionCode, t.CurrencyCode });

            // Properties
            this.Property(t => t.CountryRegionCode)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("CountryRegionCurrency", "Sales");
            this.Property(t => t.CountryRegionCode).HasColumnName("CountryRegionCode");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");

            // Relationships
            this.HasRequired(t => t.CountryRegion)
                .WithMany(t => t.CountryRegionCurrencies)
                .HasForeignKey(d => d.CountryRegionCode);
            this.HasRequired(t => t.Currency)
                .WithMany(t => t.CountryRegionCurrencies)
                .HasForeignKey(d => d.CurrencyCode);

        }
    }
}
