using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace ActivityMonitor
{
    class ReportInactiveSites
    {
        IRepository _repository;
        ILogger _log;
        IEmail _email;

        #region Constructors
        
        public ReportInactiveSites(IRepository rep, ILogger log)
        {
            _repository = rep;
            _log = log;
            _email = new Email(_repository, _log);
        }

        // Added for unit testing
        public ReportInactiveSites(IRepository rep, ILogger log, IEmail email)
        {
            _repository = rep;
            _log = log;
            _email = email;
        }

        #endregion

        public bool NumberOfInactiveSitesPerHealthBoardLimitExceeded()
        {
            List<String> _newlyInactiveSites = _repository.GetNewlyInactiveSites();

            if (_newlyInactiveSites.Count == 0)
            {
                _log.Add("No newly inactive sites");
                return false;   
                // Don't need to worry about stopping program execution here as 
                // it's caught in SendInactiveReports()
            }

            Dictionary<String, Int16> _healthBoardCount = new Dictionary<string, short>();

            foreach (string org in _newlyInactiveSites)
            {
                string _healthboard = _repository.GetOrganisationHealthBoard(org);

                if (_healthBoardCount.ContainsKey(_healthboard))
                    _healthBoardCount[_healthboard]++;
                else
                    _healthBoardCount.Add(_healthboard, 1);
            }

            bool _limitExceeded = false;
            int _limit = 10;    // Move this value to config file

            foreach (KeyValuePair<String, Int16> value in _healthBoardCount)
            {
                if (value.Value >= _limit)
                {
                    _limitExceeded = true;
                    _log.Add("WARNING: Healthboard limit exceeded for : " + value.Key + " : number of inactive sites: " + value.Value.ToString());
                }
            }

            return _limitExceeded;
        }

        public void SendInactiveReports()
        {
            List<String> _organisations = GetAllInactiveSites();

            if (_organisations.Count == 0)
            {
                // There are no new inactive sites
                _log.Add("INFO: There are no newly inactive sites to send reports to.");
                return;
            }

            foreach (string _organisation in _organisations)
            {
                // List<String> _contacts = GetContactsForOrganisation(_organisation);
                List<String> _supplierContacts = _repository.GetSupplierContactsEmailAddresses(_repository.GetOrganisationSupplier(_organisation));
                List<String> _healthBoardContacts = _repository.GetHealthBoardContactsEmailAddresses(_repository.GetOrganisationHealthBoard(_organisation));

                if (_supplierContacts.Count == 0 && _healthBoardContacts.Count == 0)
                {
                    _log.Add("WARNING: No contacts for organisation: " + _organisation + " could be found.");
                    continue;
                }
                try
                {
                    _email.Send(_supplierContacts, _healthBoardContacts, _organisation);

                    RecordOrganisationInactiveReportHasBeenSent(_organisation);
                    _log.Add("Inactive email report was sent for site: " + _organisation);
                }
                catch (Exception ex)
                {
                    _log.Add("ERROR: Unable to send inactive report email for organisation: " + _organisation + 
                        ". Error message: " + ex.Message);
                    continue;
                }
            }
        }

        private List<String> GetAllInactiveSites()
        {
            return _repository.GetNewlyInactiveSites();
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

        private void RecordOrganisationInactiveReportHasBeenSent(string organisation)
        {
            _repository.RecordDateInactiveWarningEmailWasSent(organisation);
        }
    }
}
