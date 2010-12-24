using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // Saves new inactive site
        public void SaveNewlyInactiveOrganisation(string organisation)
        {
            try
            {
                tbInactiveSites _inactiveOrganisation = new tbInactiveSites();
                _inactiveOrganisation.Org = organisation;
                _inactiveOrganisation.DateEmailSent = null;
                _inactiveOrganisation.DateCreated = DateTime.Today;
                // db.AddTotbInactiveSites(_inactiveOrganisation); commented out after playing with my ed data model, below line seems to fit but will need tested
                _ReportingEntity.tbInactiveSites.AddObject(_inactiveOrganisation);
                _ReportingEntity.SaveChanges();
                _log.Add("INFO: New Org Added to tbInactiveSites : " + organisation);
            }
            catch (Exception ex)
            {
                _log.Add("ERROR: Occured while trying to save new organisation to tbInactiveSites. Org: " + organisation);
                _log.Add(ex.Message);
                // throw new Exception(ex.Message);
            }
        }
    }
}
