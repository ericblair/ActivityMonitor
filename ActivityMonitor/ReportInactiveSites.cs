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

        public ReportInactiveSites(IRepository rep, ILogger log)
        {
            _repository = rep;
            _log = log;
        }

        public void SendInactiveReports(IEmail emailHelper)
        {
            List<String> _organisations = GetAllInactiveSites();

            foreach (string _organisation in _organisations)
            {
                List<String> _contacts = GetContactsForOrganisation(_organisation);

                emailHelper.Send(_contacts, _organisation);

                RecordOrganisationInactiveReportHasBeenSent(_organisation);
                _log.Add("Inactive email report was sent for site: " + _organisation);
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
