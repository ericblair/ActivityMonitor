using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        public string GetOrganisationLatestMessageDate(string organisation)
        {
            DateTime? _latestAMSMessageRaw = null;

            _latestAMSMessageRaw = (from OrgSupplier in _EPMS_StatisticsContext.tbOrgSupplier
                                 where OrgSupplier.org == organisation
                                 select OrgSupplier.latestAMS)
                                 .FirstOrDefault();

            DateTime _latestAMSMessage = new DateTime();

            if (_latestAMSMessageRaw == null)
            {
                _log.Add("No latest AMS value found for: " + organisation);
                return "Could not retrieve date";
            }
            else
            {
                _latestAMSMessage = (DateTime)_latestAMSMessageRaw;
            }

            return _latestAMSMessage.ToShortDateString();
        }
    }
}
