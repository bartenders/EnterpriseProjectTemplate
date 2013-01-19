using EPT.DAL.DomainClasses;
using System.Data.Entity.ModelConfiguration;

namespace EPT.DAL.Mappings
{
    public class DatabaseLogMap : EntityTypeConfiguration<DatabaseLog>
    {
        public DatabaseLogMap()
        {
            // Primary Key
            this.HasKey(t => t.DatabaseLogID);

            // Properties
            this.Property(t => t.DatabaseUser)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Event)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Schema)
                .HasMaxLength(128);

            this.Property(t => t.Object)
                .HasMaxLength(128);

            this.Property(t => t.TSQL)
                .IsRequired();

            this.Property(t => t.XmlEvent)
                .IsRequired();

            this.Property(t => t.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DatabaseLog");
            this.Property(t => t.DatabaseLogID).HasColumnName("DatabaseLogID");
            this.Property(t => t.PostTime).HasColumnName("PostTime");
            this.Property(t => t.DatabaseUser).HasColumnName("DatabaseUser");
            this.Property(t => t.Event).HasColumnName("Event");
            this.Property(t => t.Schema).HasColumnName("Schema");
            this.Property(t => t.Object).HasColumnName("Object");
            this.Property(t => t.TSQL).HasColumnName("TSQL");
            this.Property(t => t.XmlEvent).HasColumnName("XmlEvent");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");
        }
    }
}
