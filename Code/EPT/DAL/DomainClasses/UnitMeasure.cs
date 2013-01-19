using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ETP.DAL.Core;

namespace EPT.DAL.DomainClasses
{
	[Table("UnitMeasure")]
    public class UnitMeasure  : IObjectWithState
    {

        [NotMapped]
        public State State { get; set; }


        public UnitMeasure()
        {
            this.BillOfMaterials = new List<BillOfMaterial>();
            this.Products = new List<Product>();
            this.Products1 = new List<Product>();
            this.ProductVendors = new List<ProductVendor>();
        }

        public string UnitMeasureCode { get; set; }
        public string Name { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual ICollection<BillOfMaterial> BillOfMaterials { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Product> Products1 { get; set; }
        public virtual ICollection<ProductVendor> ProductVendors { get; set; }
    }
}
