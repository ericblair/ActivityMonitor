using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitor;

namespace ActivityMonitorTests.TestHelpers
{
    public static partial class PopulateTable
    {
        public static ActivityMonitor.tbRPT_MigratingSites AddMigratingSitesDataRow(string organisation)
        {
            tbRPT_MigratingSites row = new tbRPT_MigratingSites
            {
                Organisation = organisation,
                OriginalSupplier = "test",
                FutureSupplier = "test",
                PlannedMigrationDate = null,
                DateNotificationEmailSent = null,
                DateUpdated = null
            };

            return row;
        }

        public static ActivityMonitor.tbRPT_MigratingSites AddMigratingSitesDataRow(string organisation, DateTime? migrationDate)
        {
            tbRPT_MigratingSites row = new tbRPT_MigratingSites
            {
                Organisation = organisation,
                OriginalSupplier = "test",
                FutureSupplier = "test",
                PlannedMigrationDate = migrationDate,
                DateNotificationEmailSent = null,
                DateUpdated = null
            };

            return row;
        }

        public static ActivityMonitor.tbRPT_MigratingSites AddMigratingSitesDataRow(string organisation, DateTime? migrationDate, DateTime? dateEmailSent)
        {
            tbRPT_MigratingSites row = new tbRPT_MigratingSites
            {
                Organisation = organisation,
                OriginalSupplier = "test",
                FutureSupplier = "test",
                PlannedMigrationDate = migrationDate,
                DateNotificationEmailSent = dateEmailSent,
                DateUpdated = null
            };

            return row;
        }

        public static ActivityMonitor.tbRPT_MigratingSites AddMigratingSitesDataRow(string organisation, string originalSupplier, string futureSupplier)
        {
            tbRPT_MigratingSites row = new tbRPT_MigratingSites
            {
                Organisation = organisation,
                OriginalSupplier = originalSupplier,
                FutureSupplier = futureSupplier,
                PlannedMigrationDate = null,
                DateNotificationEmailSent = null,
                DateUpdated = null
            };

            return row;
        }
    }
}
