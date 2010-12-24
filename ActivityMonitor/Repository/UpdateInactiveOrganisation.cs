using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // Updates known inactive site 
        public void UpdateInactiveOrganisation(string organisation)
        {
            var _organisation = (from InactiveSites in _ReportingEntity.tbInactiveSites
                                 where InactiveSites.Org == organisation
                                 select InactiveSites)
                                 .FirstOrDefault();

            if (_organisation == null)
            {
                _log.Add("ERROR: App attempted to update organisation not present in tbInactiveSites. Org: " + organisation);
                return;
            }

            _organisation.DateUpdated = DateTime.Today;
            _ReportingEntity.SaveChanges();
        }
    }
}
