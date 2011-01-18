using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // Return all rows from tbSupplierContacts where Supplier field matches paramter value
        public List<String> GetSupplierContactsEmailAddresses(string supplier)
        {
            var _supplierContacts = from SupplierContacts in _ReportingEntity.tbSupplierContacts
                                    where SupplierContacts.Supplier == supplier
                                    select SupplierContacts.Contact;

            if (_supplierContacts.Count() == 0)
            {
                _log.Add("WARNING: No contacts could be found in tbSupplierContacts matching value: " + supplier);
                // throw error?
            }

            return _supplierContacts.ToList<String>();
        }
    }
}
