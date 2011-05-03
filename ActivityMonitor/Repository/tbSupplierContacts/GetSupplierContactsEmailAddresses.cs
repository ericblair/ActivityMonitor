using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // Return all rows from tbRPT_SupplierContacts where Supplier field matches paramter value
        public List<String> GetSupplierContactsEmailAddresses(string supplier)
        {
            var _supplierContacts = from SupplierContacts in _ReportingEntity.tbRPT_SupplierContacts
                                    where SupplierContacts.Supplier == supplier
                                    select SupplierContacts.Contact;

            if (_supplierContacts.Count() == 0)
            {
                _log.Add("WARNING: No contacts could be found in tbRPT_SupplierContacts matching value: " + supplier);
                // throw error?
            }

            return _supplierContacts.ToList<String>();
        }
    }
}
