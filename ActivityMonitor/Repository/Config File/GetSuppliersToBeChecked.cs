using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // Load the suppliers we wish to check from a config file (if I want to pass this on to Simon I'll prob create a table to hold this data
        public List<String> GetSuppliersToBeChecked()
        {
            List<String> _suppliers = null;

            _suppliers = new List<string>(ConfigurationManager.AppSettings["SuppliersToCheck"].Split(new char[] { ';' }));

            return _suppliers;
        }
    }
}
