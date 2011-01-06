using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // Update tbMigrationSites.DateNotificationEmailSent
        public void RecordDateLateMigrationEmailWasSent(string organisation)
        {
            var _organisation = (from x in _ReportingEntity.tbMigratingSites
                                 where x.Organisation == organisation
                                 select x)
                                 .FirstOrDefault();

            if (_organisation == null)
            {
                _log.Add("ERROR: Tried to update the email was sent for a non-existant organisation from tbMigratingSites. Org: " + organisation);
                return;
            }

            _organisation.DateNotificationEmailSent = DateTime.Today;
            _ReportingEntity.SaveChanges();
        }
    }
}
