using System;
using System.ComponentModel.DataAnnotations.Schema;
using ETP.DAL.Core;

namespace EPT.DAL.DomainClasses
{
	[Table("JobCandidate")]
    public class JobCandidate  : IObjectWithState
    {

        [NotMapped]
        public State State { get; set; }


        public int JobCandidateID { get; set; }
        public Nullable<int> BusinessEntityID { get; set; }
        public string Resume { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
