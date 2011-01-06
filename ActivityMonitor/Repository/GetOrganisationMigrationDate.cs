using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        public DateTime? GetOrganisationMigrationDate(string organisation)
        {
            var _migrationDate = (from x in _ReportingEntity.tbMigratingSites
                                  where x.Organisation == organisation
                                  select x.PlannedMigrationDate)
                                  .FirstOrDefault();

            return _migrationDate;
        }
    }
}
