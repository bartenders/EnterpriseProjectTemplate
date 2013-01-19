using System;
using System.ComponentModel.DataAnnotations.Schema;
using ETP.DAL.Core;

namespace EPT.DAL.DomainClasses
{
	[Table("ProductVendor")]
    public class ProductVendor  : IObjectWithState
    {

        [NotMapped]
        public State State { get; set; }


        public int ProductID { get; set; }
        public int BusinessEntityID { get; set; }
        public int AverageLeadTime { get; set; }
        public decimal StandardPrice { get; set; }
        public Nullable<decimal> LastReceiptCost { get; set; }
        public Nullable<System.DateTime> LastReceiptDate { get; set; }
        public int MinOrderQty { get; set; }
        public int MaxOrderQty { get; set; }
        public Nullable<int> OnOrderQty { get; set; }
        public string UnitMeasureCode { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual Product Product { get; set; }
        public virtual UnitMeasure UnitMeasure { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
