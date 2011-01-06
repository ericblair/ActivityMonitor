using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        public bool IsOrganisationInMigratingSitesTable(string organisation)
        {
            var _organisation = (from x in _ReportingEntity.tbMigratingSites
                                 where organisation == x.Organisation
                                 select x);

            if (_organisation.Count() == 0)
                return false;

            return true;
        }
    }
}
