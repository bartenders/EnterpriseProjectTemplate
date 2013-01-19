using System.ComponentModel.DataAnnotations.Schema;
using EPT.DAL.DomainClasses;
using System.Data.Entity.ModelConfiguration;

namespace EPT.DAL.Mappings
{
    public class BusinessEntityContactMap : EntityTypeConfiguration<BusinessEntityContact>
    {
        public BusinessEntityContactMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BusinessEntityID, t.PersonID, t.ContactTypeID });

            // Properties
            this.Property(t => t.BusinessEntityID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PersonID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ContactTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BusinessEntityContact", "Person");
            this.Property(t => t.BusinessEntityID).HasColumnName("BusinessEntityID");
            this.Property(t => t.PersonID).HasColumnName("PersonID");
            this.Property(t => t.ContactTypeID).HasColumnName("ContactTypeID");
            this.Property(t => t.rowguid).HasColumnName("rowguid");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");

            // Relationships
            this.HasRequired(t => t.BusinessEntity)
                .WithMany(t => t.BusinessEntityContacts)
                .HasForeignKey(d => d.BusinessEntityID);
            this.HasRequired(t => t.ContactType)
                .WithMany(t => t.BusinessEntityContacts)
                .HasForeignKey(d => d.ContactTypeID);
            this.HasRequired(t => t.Person)
                .WithMany(t => t.BusinessEntityContacts)
                .HasForeignKey(d => d.PersonID);

        }
    }
}
