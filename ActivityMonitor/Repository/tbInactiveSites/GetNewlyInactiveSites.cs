using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // Return list of all sites in tbRPT_InactiveSites where email has not yet been sent
        public List<String> GetNewlyInactiveSites()
        {
            var _inactiveSites = from InactiveSites in _ReportingEntity.tbRPT_InactiveSites
                                 where InactiveSites.DateEmailSent == null
                                 orderby InactiveSites.Org      // Added this to assist testing
                                 select InactiveSites.Org;

            if (_inactiveSites.Count() == 0)
            {
                _log.Add("INFORMATION: No newly inactive sites were found.");
            }

            return _inactiveSites.ToList<String>();
        }
    }
}
