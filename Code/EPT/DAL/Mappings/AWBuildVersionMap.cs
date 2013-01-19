using EPT.DAL.DomainClasses;
using System.Data.Entity.ModelConfiguration;

namespace EPT.DAL.Mappings
{
    public class AWBuildVersionMap : EntityTypeConfiguration<AWBuildVersion>
    {
        public AWBuildVersionMap()
        {
            // Primary Key
            this.HasKey(t => t.SystemInformationID);

            // Properties
            this.Property(t => t.Database_Version)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("AWBuildVersion");
            this.Property(t => t.SystemInformationID).HasColumnName("SystemInformationID");
            this.Property(t => t.Database_Version).HasColumnName("Database Version");
            this.Property(t => t.VersionDate).HasColumnName("VersionDate");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");
        }
    }
}
