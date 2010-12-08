using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitor;

namespace ActivityMonitorTests.TestHelpers
{
    public static partial class PopulateTable
    {
        public static tbHealthBoardContacts AddHealthBoardContactsDataRow(int rid, string healthBoard, string contact)
        {
            tbHealthBoardContacts row = new tbHealthBoardContacts
            {
                Rid = rid,
                HealthBoard = healthBoard,
                Contact = contact
            };

            return row;
        }
    }
}
