using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ActivityMonitor
{
    class UpdateActivityData
    {
        IRepository _repository;
        ILogger _log;

        #region Constructors
        
        public UpdateActivityData(IRepository rep, ILogger log)
        {
            _repository = rep;
            _log = log;
        }

        #endregion

        /// <summary>
        /// Checks that tbDailyActivityGP has been updated in the last 24 hours
        /// </summary>
        /// <returns></returns>
        public bool CheckActivityDataHasBeenUpdated()
        {
            return _repository.ActivityTableHasBeenUpdated();
        }

        /// <summary>
        /// Checks the previous days data for all organisations belonning to the suppliers to be checked
        /// </summary>
        public void UpdateData()
        {
            // Load suppliers to be checked from config file
            List<String> _suppliers = _repository.GetSuppliersToBeChecked();

            if (_suppliers.Count == 0)
                throw new Exception("Unable to load any suppliers to check. Check config file.");

            foreach (string _supplier in _suppliers)
            {
                // Create a list of all organisations running selected supplier's software
                List<String> _organisations = _repository.GetSupplierOrganisations(_supplier);
                if (_organisations.Count == 0)
                {
                    _log.Add("Unable to find any organisations to check for supplier: " + _supplier);
                    continue;
                }

                foreach (string _organisation in _organisations)
                {
                    // Check site's activity
                    UpdateOrganisationActivity(_organisation);
                }
            }
        }

        /// <summary>
        /// Checks sites activity and updates tbRPT_InactiveSites if necessary
        /// </summary>
        /// <param name="organisation"></param>
        internal void UpdateOrganisationActivity(string organisation)
        {
            // Check to see they are not already listed in tbRPT_InactiveSites
            bool _organisationAlreadyKnownToBeInactive = _repository.IsOrganisationListedAsInactive(organisation);

            // If site is inactive
            if (_repository.IsOrganisationActive(organisation) == false)
            {
                if (_organisationAlreadyKnownToBeInactive == false)
                {
                    // Add new record to tbRPT_InactiveSites
                    _repository.SaveNewlyInactiveOrganisation(organisation);
                    _log.Add("New inactive site: " + organisation);
                }
                else
                {
                    // Update tbRPT_InactiveSites
                    _repository.UpdateInactiveOrganisation(organisation);
                    _log.Add("Site still inactive:" + organisation);
                }
            }
            else    // Messages have been seen from org
            {
                // Check to see if site is listed as inactive
                if (_organisationAlreadyKnownToBeInactive == true)
                {
                    // Remove org from Inactive table
                    _repository.MarkOrganisationAsActive(organisation);
                    _log.Add("Site no longer inactive: " + organisation);
                }
            }
        }
    }
}
