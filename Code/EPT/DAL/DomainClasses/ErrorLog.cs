using System;
using System.ComponentModel.DataAnnotations.Schema;
using ETP.DAL.Core;

namespace EPT.DAL.DomainClasses
{
	[Table("ErrorLog")]
    public class ErrorLog  : IObjectWithState
    {

        [NotMapped]
        public State State { get; set; }


        public int ErrorLogID { get; set; }
        public System.DateTime ErrorTime { get; set; }
        public string UserName { get; set; }
        public int ErrorNumber { get; set; }
        public Nullable<int> ErrorSeverity { get; set; }
        public Nullable<int> ErrorState { get; set; }
        public string ErrorProcedure { get; set; }
        public Nullable<int> ErrorLine { get; set; }
        public string ErrorMessage { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
