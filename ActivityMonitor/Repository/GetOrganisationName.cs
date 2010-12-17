using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        public string GetOrganisationName(string organisation)
        {
            string _organisationName = null;

            _organisationName = (from Org in _EPMS_StatisticsContext.tbOrganisation
                                 where Org.id == organisation
                                 select Org.name)
                                .FirstOrDefault();

            if ((_organisationName == null) || (_organisationName == ""))
            {
                _log.Add("WARNING: No organisation name was found for organisation: " + organisation);
            }

            return _organisationName;
        }
    }
}
