using System;
using System.Collections.Generic;
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
            List<String> _suppliers = LoadSuppliersToCheck();  // Ensure LoadSuppliersToCheck throws an error if it finds nothing
            foreach (string _supplier in _suppliers)
            {
                List<String> _organisations = ReturnAllSupplierOrganisations(_supplier);
                foreach (string _organisation in _organisations)
                {
                    UpdateOrganisationActivity(_organisation);
                }
            }
        }

        private void UpdateOrganisationActivity(string organisation)
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

        private List<String> LoadSuppliersToCheck()
        {
            // Need to update the method this routine calls. Rather than return all suppliers in table, load a list from a config file
            return _repository.GetAllSuppliers();
        }

        private List<String> ReturnAllSupplierOrganisations(string supplier)
        {
            return _repository.GetSupplierOrganisations(supplier);
        }
    }
}
