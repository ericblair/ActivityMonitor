using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitor;

namespace ActivityMonitorTests.TestHelpers
{
    public static partial class PopulateTable
    {
        public static tbRPT_SupplierContacts AddSupplierContactsDataRow(int rid, string supplier, string contact)
        {
            tbRPT_SupplierContacts row = new tbRPT_SupplierContacts
            {
                Rid = rid,
                Supplier = supplier,
                Contact = contact
            };

            return row;
        }
    }
}
