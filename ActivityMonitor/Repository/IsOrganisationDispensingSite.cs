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
            var _organisation = (from Org in _EPMS_StatisticsContext.tbOrganisation
                                 where Org.id == organisation
                                 select Org.dispensing)
                                 .FirstOrDefault();

            if (_organisation == null)
            {
                _log.Add("WARNING: No records found in tbOrganisation for organisation: " + organisation);
                throw new Exception("WARNING: No records found in tbOrganisation for organisation: " + organisation);
            }

            return (bool)_organisation;
        }
    }
}
