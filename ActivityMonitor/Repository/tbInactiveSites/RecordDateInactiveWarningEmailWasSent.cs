using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // Update tbRPT_InactiveSites.DateEmailSent
        public void RecordDateInactiveWarningEmailWasSent(string organisation)
        {
            var _organisation = (from InactiveSites in _ReportingEntity.tbRPT_InactiveSites
                                 where InactiveSites.Org == organisation
                                 select InactiveSites)
                                 .FirstOrDefault();

            if (_organisation == null)
            {
                _log.Add("ERROR: Tried to update the email was sent for a non-existant organisation from tbRPT_InactiveSites. Org: " + organisation);
                return;
            }

            _organisation.DateEmailSent = DateTime.Today;
            _ReportingEntity.SaveChanges();
        }
    }
}
