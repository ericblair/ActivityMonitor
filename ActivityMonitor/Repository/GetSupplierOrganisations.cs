using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // return all organisations for specified supplier
        public List<String> GetSupplierOrganisations(string supplier)
        {
            var _organisations = from OrgSupplier in _ReportingEntity.tbOrgSupplier
                                 where OrgSupplier.supplier == supplier
                                 orderby OrgSupplier.org
                                 select OrgSupplier.org;

            if (_organisations.Count() == 0)
            {
                _log.Add("WARNING: No organisations found for supplier: " + supplier);
            }

            return _organisations.ToList<String>();
        }
    }
}
