using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        public bool IsOrganisationDispensingSite(string organisation)
        {
            var _organisation = (from Org in _ReportingEntity.tbOrgSupplier
                                 where Org.org == organisation
                                 select Org)
                                 .FirstOrDefault();

            if (_organisation == null)
            {
                _log.Add("WARNING: No records found in tbOrgSupplier for organisation: " + organisation);
                throw new Exception("WARNING: No records found in tbOrgSupplier for organisation: " + organisation);
            }

            return _organisation.disp;
        }
    }
}
