using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ETP.DAL.Core;

namespace EPT.DAL.DomainClasses
{
	[Table("CountryRegion")]
    public class CountryRegion  : IObjectWithState
    {

        [NotMapped]
        public State State { get; set; }


        public CountryRegion()
        {
            this.CountryRegionCurrencies = new List<CountryRegionCurrency>();
            this.SalesTerritories = new List<SalesTerritory>();
            this.StateProvinces = new List<StateProvince>();
        }

        public string CountryRegionCode { get; set; }
        public string Name { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public byte[] RowVersion { get; set; }
        public virtual ICollection<CountryRegionCurrency> CountryRegionCurrencies { get; set; }
        public virtual ICollection<SalesTerritory> SalesTerritories { get; set; }
        public virtual ICollection<StateProvince> StateProvinces { get; set; }
    }
}
