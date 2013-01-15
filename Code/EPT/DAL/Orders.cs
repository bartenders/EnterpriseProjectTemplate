using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPT.DAL.Northwind
{
    public partial class Orders: IDataErrorInfo
    {
        private string _error;

        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }

        public string Error
        {
            get { return _error; }
        }
    }
}
