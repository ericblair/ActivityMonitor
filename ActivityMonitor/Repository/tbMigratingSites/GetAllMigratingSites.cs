using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        public List<String> GetAllMigratingSites()
        {
            var _migratingSites = from x in _ReportingEntity.tbRPT_MigratingSites
                                  orderby x.Organisation      // Added this to assist testing
                                  select x.Organisation;

            if (_migratingSites.Count() == 0)
            {
                _log.Add("INFORMATION: No migrating sites were found.");
            }

            return _migratingSites.ToList<String>();
        }
    }
}
