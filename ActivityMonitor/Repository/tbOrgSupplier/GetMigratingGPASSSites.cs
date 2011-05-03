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
            var _migratingSitesRaw = (from OrgSupplier in _ReportingEntity.tbRPT_OrgSupplier
                                   where OrgSupplier.supplier == "Gpass"
                                      // Following line caused issues when ran against live. Error: LINQ to Entities does not recognize the method boolean startswith
                                      // && OrgSupplier.reportingSupplier.StartsWith("MIGRATING TO EMIS ON", true, System.Globalization.CultureInfo.CurrentCulture) == true
                                      && OrgSupplier.reportingSupplier.Contains("MIGRATING TO EMIS ON") 
                                   select OrgSupplier);
                                       
            Dictionary<string, DateTime> _migratingSites = new Dictionary<string, DateTime>();

            foreach (tbRPT_OrgSupplier org in _migratingSitesRaw)
            {
                try
                {
                    DateTime _migrationDate = Convert.ToDateTime(org.reportingSupplier.Substring(21).ToString());
                    _migratingSites.Add(org.org, _migrationDate);
                }
                catch
                {
                    _log.Add("Error occured converting date for site: " + org.org + ". ReportingSupplier value: " + org.reportingSupplier);
                    continue;
                }
                
            }

            return _migratingSites;
        }
    }
}
