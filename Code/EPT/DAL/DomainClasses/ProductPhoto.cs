using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ETP.DAL.Core;

namespace EPT.DAL.DomainClasses
{
	[Table("ProductPhoto")]
    public class ProductPhoto  : IObjectWithState
    {

        [NotMapped]
        public State State { get; set; }


        public ProductPhoto()
        {
            this.ProductProductPhotoes = new List<ProductProductPhoto>();
        }

        public int ProductPhotoID { get; set; }
        public byte[] ThumbNailPhoto { get; set; }
        public string ThumbnailPhotoFileName { get; set; }
        public byte[] LargePhoto { get; set; }
        public string LargePhotoFileName { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual ICollection<ProductProductPhoto> ProductProductPhotoes { get; set; }
    }
}
