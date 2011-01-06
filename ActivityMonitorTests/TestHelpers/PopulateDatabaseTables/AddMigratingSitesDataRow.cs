using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitor;

namespace ActivityMonitorTests.TestHelpers
{
    public static partial class PopulateTable
    {
        public static tbMigratingSites AddMigratingSitesDataRow(string organisation)
        {
            tbMigratingSites row = new tbMigratingSites
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

        public static tbMigratingSites AddMigratingSitesDataRow(string organisation, DateTime? migrationDate)
        {
            tbMigratingSites row = new tbMigratingSites
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
    }
}
