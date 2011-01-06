using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository 
    {
        public Dictionary<String, DateTime> FindUnnotifiedSitesWithLateMigrations()
        {
            Dictionary<String, DateTime> _sitesFound = new Dictionary<string, DateTime>();

            var _sites = (from x in _ReportingEntity.tbMigratingSites
                          where x.PlannedMigrationDate < DateTime.Today
                          && x.DateNotificationEmailSent == null
                          select x);

            foreach (tbMigratingSites site in _sites)
            {
                _log.Add("Migration Date has expired for site: " + site.Organisation
                            + " . Planned migration date: " + site.PlannedMigrationDate.ToString());
                _sitesFound.Add(site.Organisation, (DateTime)site.PlannedMigrationDate);
            }

            return _sitesFound;
        }
    }
}
