using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository 
    {
        public void SetOrganisationMigrationDate(string organisation, DateTime migrationDate)
        {
            var _organisation = (from x in _ReportingEntity.tbRPT_MigratingSites
                                 where x.Organisation == organisation
                                 select x)
                                 .FirstOrDefault();

            if (_organisation == null)
            {
                _log.Add("ERROR: App attempted to update organisation not present in tbRPT_MigratingSites. Org: " + organisation);
                return;
            }

            _organisation.PlannedMigrationDate = migrationDate;
            _ReportingEntity.SaveChanges();
        }
    }
}
