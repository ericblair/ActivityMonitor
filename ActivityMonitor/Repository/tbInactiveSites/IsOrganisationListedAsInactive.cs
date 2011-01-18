using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // Looks for organisation in tbInactiveSites
        public bool IsOrganisationListedAsInactive(string organisation)
        {
            var _organisation = (from x in _ReportingEntity.tbInactiveSites
                                 where organisation == x.Org
                                 select x);

            if (_organisation.Count() == 0)
                return false;

            return true;
        }
    }
}
