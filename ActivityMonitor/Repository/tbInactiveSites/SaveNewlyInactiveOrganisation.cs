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
                tbRPT_InactiveSites _inactiveOrganisation = new tbRPT_InactiveSites();
                _inactiveOrganisation.Org = organisation;
                _inactiveOrganisation.DateEmailSent = null;
                _inactiveOrganisation.DateCreated = DateTime.Today;
                // db.AddTotbRPT_InactiveSites(_inactiveOrganisation); commented out after playing with my ed data model, below line seems to fit but will need tested
                _ReportingEntity.tbRPT_InactiveSites.AddObject(_inactiveOrganisation);
                _ReportingEntity.SaveChanges();
                _log.Add("INFO: New Org Added to tbRPT_InactiveSites : " + organisation);
            }
            catch (Exception ex)
            {
                _log.Add("ERROR: Occured while trying to save new organisation to tbRPT_InactiveSites. Org: " + organisation);
                _log.Add(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
