using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor
{
    public class CheckMigratingSites
    {
        IRepository _repository;
        ILogger _log;
        IEmail _email;

        public CheckMigratingSites(IRepository rep, ILogger log)
        {
            _repository = rep;
            _log = log;
            _email = new Email(_repository, _log);
        }

        // Added for unit testing
        public CheckMigratingSites(IRepository rep, ILogger log, IEmail email)
        {
            _repository = rep;
            _log = log;
            _email = email;
        }

        public void UpdateMigratingSitesTable()
        {
            Dictionary<String, DateTime> _sites = _repository.GetMigratingGPASSSites();

            if (_sites.Count == 0)
            {
                _log.Add("No Migrating GPASS sites could be found. Abandoned Migrating site check");
                return;
            }

            foreach (KeyValuePair<String, DateTime> value in _sites)
            {
                if (_repository.IsOrganisationInMigratingSitesTable(value.Key))
                {
                    // Site is already listed in tbMigratingSites

                    // Update migration date if change is detected
                    if (value.Value != _repository.GetOrganisationMigrationDate(value.Key))
                    {
                        // Set new migration date
                        _repository.SetOrganisationMigrationDate(value.Key, value.Value);
                    }
                }
                else  // Add site to tbMigratingSites
                {
                    _repository.AddNewMigratingSite(value.Key, value.Value);
                }
            }
        }

        public void CheckForCompletedMigrations()
        {
            // Iterate through each organisation in tbMigratingSites
            // Check to see if the site's supplier value has changed and remove it from the
            // table if necessary
            List<String> _migratingSites = _repository.GetAllMigratingSites();

            if (_migratingSites.Count == 0)
            {
                return;
            }

            foreach (string org in _migratingSites)
            {
                // string originalSupplier = _repository.GetMigratingSiteOriginalSupplier(org);
                string futureSupplier = _repository.GetMigratingSiteFutureSupplier(org);
                string currentSupplier = _repository.GetOrganisationSupplier(org);
                if (currentSupplier == futureSupplier)
                {
                    _log.Add("INFO: Record removed from tbMigratingSites.\n"
                             + " New supplier detected for site: " + org
                             + " Original Supplier: " + _repository.GetMigratingSiteOriginalSupplier(org)
                             + " New Supplier: " + currentSupplier);
                    _repository.RemoveMigratingSite(org);
                }
            }
        }


        public void SendNotificationEmailsForLateMigrations()
        {
            Dictionary<String, DateTime> _lateMigrations = _repository.FindUnnotifiedSitesWithLateMigrations();

            if (_lateMigrations.Count == 0)
            {
                _log.Add("No un-notified sites with expired migrations were found.");
                return;
            }

            foreach (KeyValuePair<String, DateTime> value in _lateMigrations)
            {
                List<String> _contacts = GetContactsForOrganisation(value.Key);

                if (_contacts.Count == 0)
                {
                    _log.Add("WARNING: No contacts for organisation: " + value.Key + " could be found.");
                    continue;
                }
                try
                {
                    // Add parameter to pass in expired date
                    _email.Send(_contacts, value.Key);

                    RecordLateMigrationReportHasBeenSent(value.Key);
                    _log.Add("Late migration report was sent for site: " + value.Key);
                }
                catch (Exception ex)
                {
                    _log.Add("ERROR: Unable to send Late migration email for organisation: " + value.Key +
                        ". Error message: " + ex.Message);
                    continue;
                }
            }
        }

        private List<String> GetContactsForOrganisation(string organisation)
        {
            List<String> _contacts = new List<string>();

            string _supplier = _repository.GetOrganisationSupplier(organisation);
            string _healthBoard = _repository.GetOrganisationHealthBoard(organisation);

            _contacts.AddRange(_repository.GetSupplierContactsEmailAddresses(_supplier));
            _contacts.AddRange(_repository.GetHealthBoardContactsEmailAddresses(_healthBoard));

            return _contacts;
        }

        private void RecordLateMigrationReportHasBeenSent(string organisation)
        {
            _repository.RecordDateLateMigrationEmailWasSent(organisation);
        }
    }
}
