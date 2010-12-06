using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ActivityMonitor
{
    class Repository : ActivityMonitor.IRepository
    {
        private IEPMS_StatisticsEntities _EPMS_StatisticsContext;
        private ILogger _log;

        public Repository(ILogger log, IEPMS_StatisticsEntities context)
        {
            _log = log;
            _EPMS_StatisticsContext = context;
        }

        // Checks for records from the previous day in tbGPDailyActivity 
        public bool ActivityTableHasBeenUpdated()
        {
            // LINQ doesn't recognise DateTime.Today.AddDays(-1) natively
            string _yesterday = DateTime.Today.AddDays(-1).ToString();

            return ActivityTableHasBeenUpdated(_yesterday);
        }

        // Checks in tbGPDailyActivity for the presence of records matching the passed in date
        // TODO: Investigate passing in a DateTime value as parameter and transforming it to correct format rather than using a string
        public bool ActivityTableHasBeenUpdated(string date)
        {
            DateTime _date = new DateTime();
            try
            {
                _date = DateTime.Parse(date);
            }
            catch (Exception ex)
            {
                _log.Add(ex.Message);
                throw new Exception(ex.Message);    // not sure about throwing this here....
            }

            var _recordsMatchingDate = from ActivityTable in _EPMS_StatisticsContext.tbGPdailyactivity
                                       where ActivityTable.date == _date
                                       select ActivityTable;

            if (_recordsMatchingDate.Count() == 0)
                return false;

            return true;
        }

        // Return all rows from tbSupplierContacts where Supplier field matches paramter value
        public List<String> GetSupplierContactsEmailAddresses(string supplier)
        {
            var _supplierContacts = from SupplierContacts in _EPMS_StatisticsContext.tbSupplierContacts
                                    where SupplierContacts.Supplier == supplier
                                    select SupplierContacts.Contact;

            if (_supplierContacts.Count() == 0)
            {
                _log.Add("WARNING: No contacts could be found in tbSupplierContacts matching value: " + supplier);
                // throw error?
            }

            return _supplierContacts.ToList<String>();
        }

        // Return all rows from tbHealthBoardContacts where HealthBoard field matches paramter value
        public List<String> GetHealthBoardContactsEmailAddresses(string healthBoard)
        {
            var _healthBoardContacts = from healthBoardContacts in _EPMS_StatisticsContext.tbHealthBoardContacts
                             where healthBoardContacts.HealthBoard == healthBoard
                             select healthBoardContacts.Contact;

            if (_healthBoardContacts.Count() == 0)
            {
                _log.Add("WARNING: No contacts could be found in tbHealthBoardContacts matching value: " + healthBoard);
                // throw error?
            }

            return _healthBoardContacts.ToList<String>();
        }

        // Return list of all sites in tbInactiveSites where email has not yet been sent
        public List<String> GetNewlyInactiveSites()
        {
            var _inactiveSites = from InactiveSites in _EPMS_StatisticsContext.tbInactiveSites
                                 where InactiveSites.DateEmailSent == null
                                 orderby InactiveSites.Org      // Added this to assist testing
                                 select InactiveSites.Org;

            if (_inactiveSites.Count() == 0)
            {
                _log.Add("INFORMATION: No newly inactive sites were found.");
            }

            return _inactiveSites.ToList<String>();
        }

        // return organisation's supplier value from tbOrgSupplier
        public string GetOrganisationSupplier(string organisation)
        {
            string _supplier = null;

            //TODO: Tidy this query!
            _supplier = (from OrgSupplier in _EPMS_StatisticsContext.tbOrgSupplier
                         where OrgSupplier.org == organisation
                         select OrgSupplier.supplier)
                        .FirstOrDefault();

            if (_supplier == null)
            {
                _log.Add("WARNING: No supplier was found for organisation: " + organisation);
            }

            return _supplier;
        }

        // Return organisation's healthboard value from tbOrganisation
        public string GetOrganisationHealthBoard(string organisation)
        {
            string _healthBoard = null;

            //TODO: Tidy this query!
            _healthBoard = (from Organisation in _EPMS_StatisticsContext.tbOrganisation
                            where Organisation.id == organisation
                            select Organisation.healthBoardName)
                            .FirstOrDefault();

            if (_healthBoard == null)
            {
                _log.Add("WARNING: No health board was found for organisation: " + organisation);
            }

            return _healthBoard;
        }

        // Return all distinct supplier values from tbOrgSupplier
        // TODO: rather than return all distict, return all that match values passed loaded from config file
        public List<String> GetAllSuppliers()
        {
            var _suppliers = (from OrgSupplier in _EPMS_StatisticsContext.tbOrgSupplier
                              orderby OrgSupplier.supplier
                             select OrgSupplier.supplier)
                            .Distinct();

            if (_suppliers.Count() == 0)
            {
                _log.Add("WARNING: No Suppliers found in tbOrgSupplier");
                // throw error?
            }

            return _suppliers.ToList<String>();
        }

        // return all organisations for specified supplier
        public List<String> GetSupplierOrganisations(string supplier)
        {
            var _organisations = from OrgSupplier in _EPMS_StatisticsContext.tbOrgSupplier
                                 where OrgSupplier.supplier == supplier
                                 orderby OrgSupplier.org
                                 select OrgSupplier.org;

            if (_organisations.Count() == 0)
            {
                _log.Add("WARNING: No organisations found for supplier: " + supplier);
            }

            return _organisations.ToList<String>();
        }

        // Returns the most recent dates activity for the organisation passed in
        // TODO: This query has to accept a date parameter (or keep this as a default and create an overload)
        public tbGPdailyactivity GetOrganisationActivityDetails(string organisation)
        {
            var _orgActivity = (from GPActivity in _EPMS_StatisticsContext.tbGPdailyactivity
                                where GPActivity.org == organisation
                                orderby GPActivity.date descending
                                select GPActivity)
                                .First();

            return _orgActivity;
        }

        // Checks if organisation has sent particular messages
        public bool IsOrganisationActive(string organisation)
        {
            var _organisation = (from GPActivity in _EPMS_StatisticsContext.tbGPdailyactivity
                                 where GPActivity.org == organisation
                                 select GPActivity)
                                 .First();
            if (_organisation.amsPrescriptions == 0 && _organisation.gpRegistrationUpdatesRequests == 0)
                return false;

            return true;
        }

        // Looks for organisation in tbInactiveSites
        public bool IsOrganisationListedAsInactive(string organisation)
        {
            var _organisation = (from x in _EPMS_StatisticsContext.tbInactiveSites
                                 where organisation == x.Org
                                 select x);

            if (_organisation.Count() == 0)
                return false;

            return true;
        }

        // Saves new inactive site
        public void SaveNewlyInactiveOrganisation(string organisation)
        {
            tbInactiveSites _inactiveOrganisation = new tbInactiveSites();
            _inactiveOrganisation.Org = organisation;
            _inactiveOrganisation.DateEmailSent = null;
            _inactiveOrganisation.DateCreated = DateTime.Today;
            // db.AddTotbInactiveSites(_inactiveOrganisation); commented out after playing with my ed data model, below line seems to fit but will need tested
            _EPMS_StatisticsContext.tbInactiveSites.AddObject(_inactiveOrganisation);
            _EPMS_StatisticsContext.SaveChanges();
        }

        // Updates known inactive site 
        public void UpdateInactiveOrganisation(string organisation)
        {
            var _organisation = (from InactiveSites in _EPMS_StatisticsContext.tbInactiveSites
                                 where organisation == InactiveSites.Org
                                 select InactiveSites)
                                 .First();

            _organisation.DateUpdated = DateTime.Today;
            _EPMS_StatisticsContext.SaveChanges();
        }

        // Remove site from tbInactiveSites
        public void MarkOrganisationAsActive(string organisation)
        {
            var _organisation = (from InactiveSites in _EPMS_StatisticsContext.tbInactiveSites
                                 where organisation == InactiveSites.Org
                                 select InactiveSites)
                                 .First();

            _EPMS_StatisticsContext.DeleteObject(_organisation);
            _EPMS_StatisticsContext.SaveChanges();
        }

        // Update tbInactiveSites.DateEmailSent
        public void RecordDateInactiveWarningEmailWasSent(string organisation)
        {
            var _organisation = (from InactiveSites in _EPMS_StatisticsContext.tbInactiveSites
                                 where InactiveSites.Org == organisation
                                 select InactiveSites)
                                 .First();

            _organisation.DateEmailSent = DateTime.Today;
            _EPMS_StatisticsContext.SaveChanges();
        }
    }
}
