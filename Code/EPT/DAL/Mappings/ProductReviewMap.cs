using EPT.DAL.DomainClasses;
using System.Data.Entity.ModelConfiguration;

namespace EPT.DAL.Mappings
{
    public class ProductReviewMap : EntityTypeConfiguration<ProductReview>
    {
        public ProductReviewMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductReviewID);

            // Properties
            this.Property(t => t.ReviewerName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EmailAddress)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Comments)
                .HasMaxLength(3850);

            this.Property(t => t.RowVersion)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductReview", "Production");
            this.Property(t => t.ProductReviewID).HasColumnName("ProductReviewID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ReviewerName).HasColumnName("ReviewerName");
            this.Property(t => t.ReviewDate).HasColumnName("ReviewDate");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.Rating).HasColumnName("Rating");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductReviews)
                .HasForeignKey(d => d.ProductID);

        }
    }
}
