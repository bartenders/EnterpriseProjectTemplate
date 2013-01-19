using System.ComponentModel.DataAnnotations.Schema;
using ETP.DAL.Core;

namespace EPT.DAL.DomainClasses
{
	[Table("CountryRegionCurrency")]
    public class CountryRegionCurrency  : IObjectWithState
    {

        [NotMapped]
        public State State { get; set; }


        public string CountryRegionCode { get; set; }
        public string CurrencyCode { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual CountryRegion CountryRegion { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
