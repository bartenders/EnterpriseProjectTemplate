using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ETP.DAL.Core;

namespace EPT.DAL.DomainClasses
{
	[Table("PhoneNumberType")]
    public class PhoneNumberType  : IObjectWithState
    {

        [NotMapped]
        public State State { get; set; }


        public PhoneNumberType()
        {
            this.PersonPhones = new List<PersonPhone>();
        }

        public int PhoneNumberTypeID { get; set; }
        public string Name { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual ICollection<PersonPhone> PersonPhones { get; set; }
    }
}
