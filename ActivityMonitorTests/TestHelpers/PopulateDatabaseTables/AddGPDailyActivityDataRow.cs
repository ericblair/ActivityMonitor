using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitor;

namespace ActivityMonitorTests.TestHelpers
{
    public static partial class PopulateTable
    {
        public static tbGPdailyactivity AddGPDailyActivityDataRow(string organisation, int daysToSubtractFromDate)
        {
            tbGPdailyactivity row = new tbGPdailyactivity
            {
                org = organisation,
                date = DateTime.Today.AddDays(-daysToSubtractFromDate),
                amsPrescriptions = 1,
                amsAmendments = 1,
                amsCancellations = 1,
                cmsPrescriptions = 1,
                cmsCancellations = 1,
                cmsUpdatesRequests = 1,
                cmsTreatmentUpdates = 1,
                cmsComplianceUpdates = 1,
                gpRegistrationUpdatesRequests = 1
            };

            return row;
        }

        public static tbGPdailyactivity AddGPDailyActivityDataRow(string organisation, int daysToSubtractFromDate, bool active)
        {
            if (active == true)
                return AddGPDailyActivityDataRow(organisation, daysToSubtractFromDate);

            tbGPdailyactivity row = new tbGPdailyactivity
            {
                org = organisation,
                date = DateTime.Today,
                amsPrescriptions = 0,
                amsAmendments = 0,
                amsCancellations = 0,
                cmsPrescriptions = 0,
                cmsCancellations = 0,
                cmsUpdatesRequests = 0,
                cmsTreatmentUpdates = 0,
                cmsComplianceUpdates = 0,
                gpRegistrationUpdatesRequests = 0
            };

            return row;
        }
    }
}
