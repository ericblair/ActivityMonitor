using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // Remove site from tbRPT_InactiveSites
        public void MarkOrganisationAsActive(string organisation)
        {
            var _organisation = (from InactiveSites in _ReportingEntity.tbRPT_InactiveSites
                                 where organisation == InactiveSites.Org
                                 select InactiveSites)
                                 .FirstOrDefault();

            if (_organisation == null)
            {
                _log.Add("ERROR: Tried to remove non-existant organisation from tbRPT_InactiveSites. Org: " + organisation);
                return;
            }

            _ReportingEntity.DeleteObject(_organisation);
            _ReportingEntity.SaveChanges();
        }
    }
}
