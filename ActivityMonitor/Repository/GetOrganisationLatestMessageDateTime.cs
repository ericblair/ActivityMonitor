using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        public string GetOrganisationLatestMessageDateTime(string organisation)
        {
            // Need to get the linq book out and get this done in a single query.....

            DateTime? _latestAMSMessage = null;

            _latestAMSMessage = (from OrgSupplier in _EPMS_StatisticsContext.tbOrgSupplier
                                 where OrgSupplier.org == organisation
                                 select OrgSupplier.latestAMS)
                                 .FirstOrDefault();

            DateTime? _latestCMSMessage = null;

            _latestCMSMessage = (from OrgSupplier in _EPMS_StatisticsContext.tbOrgSupplier
                                 where OrgSupplier.org == organisation
                                 select OrgSupplier.latestCMS)
                                 .FirstOrDefault();

            List<DateTime?> _dates = new List<DateTime?>();
            _dates.Add(_latestAMSMessage);
            _dates.Add(_latestCMSMessage);
            _dates.Sort(((x, y) => y.Value.CompareTo(x.Value)));

            string _latestMessage = _dates[0].ToString();
                              
            return _latestMessage;
        }
    }
}
