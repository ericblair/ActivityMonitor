using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository 
    {
        public string GetMigratingSiteOriginalSupplier(string organisation)
        {
            var _originalSupplier = (from x in _ReportingEntity.tbMigratingSites
                                     where x.Organisation == organisation
                                     select x.OriginalSupplier)
                                     .FirstOrDefault();

            if (_originalSupplier == null)
            {
                _log.Add("No original supplier value could be found for site: " + organisation);
            }

            return _originalSupplier;
        }
    }
}
