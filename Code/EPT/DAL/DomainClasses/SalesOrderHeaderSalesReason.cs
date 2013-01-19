using System.ComponentModel.DataAnnotations.Schema;
using ETP.DAL.Core;

namespace EPT.DAL.DomainClasses
{
	[Table("SalesOrderHeaderSalesReason")]
    public class SalesOrderHeaderSalesReason  : IObjectWithState
    {

        [NotMapped]
        public State State { get; set; }


        public int SalesOrderID { get; set; }
        public int SalesReasonID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual SalesOrderHeader SalesOrderHeader { get; set; }
        public virtual SalesReason SalesReason { get; set; }
    }
}
