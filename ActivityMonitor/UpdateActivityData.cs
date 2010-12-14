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

        public UpdateActivityData(IRepository rep, ILogger log)
        {
            _repository = rep;
            _log = log;
        }

        public bool CheckActivityDataHasBeenUpdated()
        {
            return _repository.ActivityTableHasBeenUpdated();
        }

        // This method checks the previous days data for all organisations belonning to the suppliers to be checked
        public void UpdateData()
        {
            //List<String> _suppliers = LoadSuppliersToCheck();  // Ensure LoadSuppliersToCheck throws an error if it finds nothing
            //string _supplier = "EMIS";
            List<String> _suppliers = _repository.GetSuppliersToBeChecked();

            if (_suppliers.Count == 0)
                throw new Exception("Unable to load any suppliers to check. Check config file.");

            foreach (string _supplier in _suppliers)
            {
                List<String> _organisations = _repository.GetSupplierOrganisations(_supplier);
                if (_organisations.Count == 0)
                    throw new Exception("Unable to find any organisations to check for supplier: " + _supplier);

                foreach (string _organisation in _organisations)
                {
                    UpdateOrganisationActivity(_organisation);
                }
            }
        }

        internal void UpdateOrganisationActivity(string organisation)
        {
            // Check to see they are not already listed in tbInactiveSites
            bool _organisationAlreadyKnownToBeInactive = _repository.IsOrganisationListedAsInactive(organisation);

            // If site is inactive
            if (_repository.IsOrganisationActive(organisation) == false)
            {
                if (_organisationAlreadyKnownToBeInactive == false)
                {
                    // Add new record to tbInactiveSites
                    _repository.SaveNewlyInactiveOrganisation(organisation);
                    _log.Add("New inactive site: " + organisation);
                }
                else
                {
                    // Update tbInactiveSites
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
