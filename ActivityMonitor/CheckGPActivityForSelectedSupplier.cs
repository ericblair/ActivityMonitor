using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor
{
    public class CheckGPActivityForSelectedSupplier
    {
        IRepository _repository;
        ILogger _log;
        bool _applyHealthBoardInactiveSiteLimit;

        public CheckGPActivityForSelectedSupplier(IRepository rep, ILogger log, bool applyHealthBoardInactiveSiteLimit)
        {
            _repository = rep;
            _log = log;
            _applyHealthBoardInactiveSiteLimit = applyHealthBoardInactiveSiteLimit;
        }

        public void RunCheck()
        {
            
            if ((DateTime.Today.DayOfWeek == DayOfWeek.Sunday) || (DateTime.Today.DayOfWeek == DayOfWeek.Monday))
            {
                _log.Add("Running app today would pick up weekend activity. Run aborted.");
                return;
            }

            UpdateActivityData updateActivityData = new UpdateActivityData(_repository, _log);
            if (updateActivityData.CheckActivityDataHasBeenUpdated() == false)
            {
                _log.Add("tbGPDailyActivity was not updated yesterday");
                return;
            }
            updateActivityData.UpdateData();

            // if number of inactive sites per healthboard limit is to be observed
            if (_applyHealthBoardInactiveSiteLimit == true)
            {
                ReportInactiveSites reportInactiveSites = new ReportInactiveSites(_repository, _log);
                if (reportInactiveSites.NumberOfInactiveSitesPerHealthBoardLimitExceeded() == false)
                {
                    reportInactiveSites.SendInactiveReports();
                }
            }
            else
            {
                ReportInactiveSites reportInactiveSites = new ReportInactiveSites(_repository, _log);
                reportInactiveSites.SendInactiveReports();
            }
        }
    }
}
