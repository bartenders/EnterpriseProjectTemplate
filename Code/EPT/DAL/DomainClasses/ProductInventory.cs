using System.ComponentModel.DataAnnotations.Schema;
using ETP.DAL.Core;

namespace EPT.DAL.DomainClasses
{
	[Table("ProductInventory")]
    public class ProductInventory  : IObjectWithState
    {

        [NotMapped]
        public State State { get; set; }


        public int ProductID { get; set; }
        public short LocationID { get; set; }
        public string Shelf { get; set; }
        public byte Bin { get; set; }
        public short Quantity { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
    }
}
