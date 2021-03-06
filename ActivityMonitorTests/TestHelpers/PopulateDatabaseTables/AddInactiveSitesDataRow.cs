﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitor;

namespace ActivityMonitorTests.TestHelpers
{
    public static partial class PopulateTable
    {
        public static tbRPT_InactiveSites AddInactiveSitesDataRow(string organisation, Nullable<System.DateTime> dateEmailSent, DateTime dateCreated, Nullable<System.DateTime> dateUpdated)
        {
            tbRPT_InactiveSites row = new tbRPT_InactiveSites
            {
                Org = organisation,
                DateEmailSent = dateEmailSent,
                DateCreated = dateCreated,
                DateUpdated = dateUpdated
            };

            return row;
        }
    }
}
