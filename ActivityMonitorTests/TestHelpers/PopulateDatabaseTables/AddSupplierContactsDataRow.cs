using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitor;

namespace ActivityMonitorTests.TestHelpers
{
    public static partial class PopulateTable
    {
        public static tbSupplierContacts AddSupplierContactsDataRow(int rid, string supplier, string contact)
        {
            tbSupplierContacts row = new tbSupplierContacts
            {
                Rid = rid,
                Supplier = supplier,
                Contact = contact
            };

            return row;
        }
    }
}
