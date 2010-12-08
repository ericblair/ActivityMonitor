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
            // Need to think about how to avoid duplicate rows for org's. Trigger is obvious answer but hard to test (withoout integration tests)
            // may be more feasible just to add a check here (i.e. if count > 1 => log error)
            var _organisation = (from x in _EPMS_StatisticsContext.tbInactiveSites
                                 where organisation == x.Org
                                 select x);

            if (_organisation.Count() == 0)
                return false;

            return true;
        }
    }
}
