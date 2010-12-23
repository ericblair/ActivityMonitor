using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        public bool IsOrganisationActive(string organisation)
        {
            if ((this.OrganisationHasSentAMSMessages(organisation) == false) && (this.IsOrganisationDispensingSite(organisation) == false))
            {
                // No AMS messages have been received and site is not a dispensing site
                return false;
            }

            // otherwise, site is active or a dispensing site
            return true;
        }
    }
}
