using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // Checks if organisation has sent particular messages
        public bool OrganisationHasSentAMSMessages(string organisation)
        {
            var _organisation = (from GPActivity in _EPMS_StatisticsContext.tbGPdailyactivity
                                 where GPActivity.org == organisation
                                 orderby GPActivity.date descending
                                 select GPActivity)
                                 .FirstOrDefault();

            if (_organisation == null)
            {
                _log.Add("WARNING: No records found in tbGPDailyActivity for organisation: " + organisation);
                throw new Exception("WARNING: No records found in tbGPDailyActivity for organisation: " + organisation);
            }

            // if (_organisation.amsPrescriptions == 0 && _organisation.gpRegistrationUpdatesRequests == 0)
            if (_organisation.amsPrescriptions == 0)
                return false;

            return true;
        }
    }
}
