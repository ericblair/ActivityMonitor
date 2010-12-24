using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // Checks for records from the previous day in tbGPDailyActivity 
        public bool ActivityTableHasBeenUpdated()
        {
            // LINQ doesn't recognise DateTime.Today.AddDays(-1) natively
            string _yesterday = DateTime.Today.AddDays(-1).ToString();

            return ActivityTableHasBeenUpdated(_yesterday);
        }

        // Checks in tbGPDailyActivity for the presence of records matching the passed in date
        // TODO: Investigate passing in a DateTime value as parameter and transforming it to correct format rather than using a string
        public bool ActivityTableHasBeenUpdated(string date)
        {
            DateTime _date = new DateTime();
            try
            {
                _date = DateTime.Parse(date);
            }
            catch (Exception ex)
            {
                _log.Add(ex.Message);
                throw new Exception(ex.Message);    // not sure about throwing this here....
            }

            var _recordsMatchingDate = from ActivityTable in _ReportingEntity.tbDailyActivityGP
                                       where ActivityTable.date == _date
                                       select ActivityTable;

            if (_recordsMatchingDate.Count() == 0)
                return false;

            return true;
        }
    }
}
