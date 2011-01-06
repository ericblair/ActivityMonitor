using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        public Dictionary<string, DateTime> GetMigratingGPASSSites()
        {
            var _migratingSitesRaw = (from OrgSupplier in _ReportingEntity.tbOrgSupplier
                                   where OrgSupplier.supplier == "Gpass"
                                   && OrgSupplier.reportingSupplier.StartsWith("MIGRATING TO EMIS ON", true, System.Globalization.CultureInfo.CurrentCulture) == true
                                   select OrgSupplier);
                                       
            Dictionary<string, DateTime> _migratingSites = new Dictionary<string, DateTime>();

            foreach (tbOrgSupplier org in _migratingSitesRaw)
            {
                // Is this the best way?
                //int stringLength = org.reportingSupplier.Length;
                DateTime _migrationDate = Convert.ToDateTime(org.reportingSupplier.Substring(21).ToString());
                _migratingSites.Add(org.org, _migrationDate);
            }

            return _migratingSites;
        }
    }
}
