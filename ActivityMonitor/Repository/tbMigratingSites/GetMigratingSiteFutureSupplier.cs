using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        public string GetMigratingSiteFutureSupplier(string organisation)
        {
            var _futureSupplier = (from x in _ReportingEntity.tbMigratingSites
                                     where x.Organisation == organisation
                                     select x.FutureSupplier)
                                     .FirstOrDefault();

            if (_futureSupplier == null)
            {
                _log.Add("No future supplier value could be found for site: " + organisation);
            }

            return _futureSupplier;
        }
    }
}
