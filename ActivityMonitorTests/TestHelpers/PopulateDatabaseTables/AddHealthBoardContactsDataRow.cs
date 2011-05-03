using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitor;

namespace ActivityMonitorTests.TestHelpers
{
    public static partial class PopulateTable
    {
        public static tbRPT_HealthBoardContacts AddHealthBoardContactsDataRow(int rid, string healthBoard, string contact)
        {
            tbRPT_HealthBoardContacts row = new tbRPT_HealthBoardContacts
            {
                Rid = rid,
                HealthBoard = healthBoard,
                Contact = contact
            };

            return row;
        }
    }
}
