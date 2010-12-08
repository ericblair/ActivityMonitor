using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // Update tbInactiveSites.DateEmailSent
        public void RecordDateInactiveWarningEmailWasSent(string organisation)
        {
            var _organisation = (from InactiveSites in _EPMS_StatisticsContext.tbInactiveSites
                                 where InactiveSites.Org == organisation
                                 select InactiveSites)
                                 .FirstOrDefault();

            if (_organisation == null)
            {
                _log.Add("ERROR: Tried to update the email was sent for a non-existant organisation from tbInactiveSites. Org: " + organisation);
                return;
            }

            _organisation.DateEmailSent = DateTime.Today;
            _EPMS_StatisticsContext.SaveChanges();
        }
    }
}
