using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository 
    {
        public void AddNewMigratingSite(string organisation, DateTime migrationDate)
        {
            try
            {
                tbMigratingSites _migratingSite = new tbMigratingSites
                {
                    Organisation = organisation,
                    OriginalSupplier = this.GetOrganisationSupplier(organisation),
                    FutureSupplier = null,  // Need to think about this....
                    PlannedMigrationDate = migrationDate,
                    DateNotificationEmailSent = null,
                    DateUpdated = null
                };

                _ReportingEntity.tbMigratingSites.AddObject(_migratingSite);
                _ReportingEntity.SaveChanges();
                _log.Add("New organisation added to tbMigratingSites: " + organisation);
            }
            catch (Exception ex)
            {
                _log.Add("ERROR: Occured while trying to save new organisation to tbMigratingSites. Org: " + organisation);
                _log.Add(ex.Message);
            }
        }
    }
}
