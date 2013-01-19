using EPT.DAL.DomainClasses;
using System.Data.Entity.ModelConfiguration;

namespace EPT.DAL.Mappings
{
    public class SalesReasonMap : EntityTypeConfiguration<SalesReason>
    {
        public SalesReasonMap()
        {
            // Primary Key
            this.HasKey(t => t.SalesReasonID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ReasonType)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("SalesReason", "Sales");
            this.Property(t => t.SalesReasonID).HasColumnName("SalesReasonID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ReasonType).HasColumnName("ReasonType");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");
        }
    }
}
