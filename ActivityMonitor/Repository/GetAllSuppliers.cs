using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // Return all distinct supplier values from tbOrgSupplier
        // TODO: rather than return all distict, return all that match values passed loaded from config file
        public List<String> GetAllSuppliers()
        {
            var _suppliers = (from OrgSupplier in _EPMS_StatisticsContext.tbOrgSupplier
                              orderby OrgSupplier.supplier
                              select OrgSupplier.supplier)
                            .Distinct();

            if (_suppliers.Count() == 0)
            {
                _log.Add("WARNING: No Suppliers found in tbOrgSupplier");
                // throw error?
            }

            return _suppliers.ToList<String>();
        }
    }
}
