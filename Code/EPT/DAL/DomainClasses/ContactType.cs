using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ETP.DAL.Core;

namespace EPT.DAL.DomainClasses
{
	[Table("ContactType")]
    public class ContactType  : IObjectWithState
    {

        [NotMapped]
        public State State { get; set; }


        public ContactType()
        {
            this.BusinessEntityContacts = new List<BusinessEntityContact>();
        }

        public int ContactTypeID { get; set; }
        public string Name { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual ICollection<BusinessEntityContact> BusinessEntityContacts { get; set; }
    }
}
