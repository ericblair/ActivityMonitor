using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        public string GetOrganisationSupplierReference(string organisation)
        {
            string _supplierRef = null;

            _supplierRef = (from Org in _ReportingEntity.tbEPS_Organisation
                            where Org.id == organisation
                            select Org.supplierReference)
                            .FirstOrDefault();

            if ((_supplierRef == null) || (_supplierRef == ""))
            {
                _log.Add("WARNING: No supplier reference was found for organisation: " + organisation);
                return "No Supplier Reference Found";
            }

            return _supplierRef;
        }
    }
}
