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
            // No AMS messages have been received and site is not a dispensing site
            if ((this.OrganisationHasSentAMSMessages(organisation) == false) && (this.IsOrganisationDispensingSite(organisation) == false))
            {
                // Check for any activity from the site since tbDailyActivityGP was updated
                if (!this.HasSiteSentAMSGPMessagesSinceYesterday(organisation))
                    return false;
            }

            // otherwise, site is active or a dispensing site
            return true;
        }
    }
}
