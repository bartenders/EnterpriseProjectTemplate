using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ETP.DAL.Core;

namespace EPT.DAL.DomainClasses
{
	[Table("ScrapReason")]
    public class ScrapReason  : IObjectWithState
    {

        [NotMapped]
        public State State { get; set; }


        public ScrapReason()
        {
            this.WorkOrders = new List<WorkOrder>();
        }

        public short ScrapReasonID { get; set; }
        public string Name { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
    }
}
