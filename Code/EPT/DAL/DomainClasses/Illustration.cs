using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ETP.DAL.Core;

namespace EPT.DAL.DomainClasses
{
	[Table("Illustration")]
    public class Illustration  : IObjectWithState
    {

        [NotMapped]
        public State State { get; set; }


        public Illustration()
        {
            this.ProductModelIllustrations = new List<ProductModelIllustration>();
        }

        public int IllustrationID { get; set; }
        public string Diagram { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual ICollection<ProductModelIllustration> ProductModelIllustrations { get; set; }
    }
}
