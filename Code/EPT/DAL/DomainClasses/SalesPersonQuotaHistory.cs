using System.ComponentModel.DataAnnotations.Schema;
using ETP.DAL.Core;

namespace EPT.DAL.DomainClasses
{
	[Table("SalesPersonQuotaHistory")]
    public class SalesPersonQuotaHistory  : IObjectWithState
    {

        [NotMapped]
        public State State { get; set; }


        public int BusinessEntityID { get; set; }
        public System.DateTime QuotaDate { get; set; }
        public decimal SalesQuota { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual SalesPerson SalesPerson { get; set; }
    }
}
