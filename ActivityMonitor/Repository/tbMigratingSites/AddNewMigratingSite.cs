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
                tbRPT_MigratingSites _migratingSite = new tbRPT_MigratingSites
                {
                    Organisation = organisation,
                    OriginalSupplier = this.GetOrganisationSupplier(organisation),
                    FutureSupplier = null,  // Need to think about this....
                    PlannedMigrationDate = migrationDate,
                    DateNotificationEmailSent = null,
                    DateUpdated = null
                };

                _ReportingEntity.tbRPT_MigratingSites.AddObject(_migratingSite);
                _ReportingEntity.SaveChanges();
                _log.Add("New organisation added to tbRPT_MigratingSites: " + organisation);
            }
            catch (Exception ex)
            {
                _log.Add("ERROR: Occured while trying to save new organisation to tbRPT_MigratingSites. Org: " + organisation);
                _log.Add(ex.Message);
            }
        }
    }
}
