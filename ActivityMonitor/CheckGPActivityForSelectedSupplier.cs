using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor
{
    /// <summary>
    /// This class will load a list of suppliers from the config file.
    /// For each supplier, we check for activity in the last 24 hours for each organisation running the supplier software. 
    /// </summary>
    public class CheckGPActivityForSelectedSupplier
    {
        #region Properties
        
        IRepository _repository;
        ILogger _log;

        // If this bool is set to true if more than 10 (current limit) inactive sites are found for any health board then a warning will be
        // logged and no emails will be sent. The reason for this limit is to try and catch events that we should not be reporting on 
        // (i.e bank holidays), the reasoning being that it is unlikely that more than 10 sites in one health board will be inactive at any one point
        // so if more than this number are found we should investigate before sending inactive reports. 
        // Setting this value to false will mean the limit is ignored and the inactive reports will be sent.
        bool _applyHealthBoardInactiveSiteLimit;

        #endregion

        #region Constructors

        public CheckGPActivityForSelectedSupplier(IRepository rep, ILogger log, bool applyHealthBoardInactiveSiteLimit)
        {
            _repository = rep;
            _log = log;
            _applyHealthBoardInactiveSiteLimit = applyHealthBoardInactiveSiteLimit;
        }

        #endregion

        /// <summary>
        /// This method first checks that the tool should be run (correct day of week to avoid picking up weekend activity + tbDailyActivityGP 
        /// // has been updated in the last 24 hours) before checking for any inactive sites and sending any inactive reports.
        /// </summary>
        public void RunCheck()
        {
            // Check that tbDailyActivityGP has been updated 
            UpdateActivityData updateActivityData = new UpdateActivityData(_repository, _log);
            // Removed following section as it no longer makes complete sense after enabling last active weekday checking
            //if (updateActivityData.CheckActivityDataHasBeenUpdated() == false)
            //{
            //    _log.Add("tbDailyActivityGP was not updated yesterday");
            //    return;
            //}

            // Update tbRPT_InactiveSites
            updateActivityData.UpdateData();

            ReportInactiveSites reportInactiveSites = new ReportInactiveSites(_repository, _log);
            // if number of inactive sites per healthboard limit is to be observed
            if (_applyHealthBoardInactiveSiteLimit == true)
            {
                //if (reportInactiveSites.NumberOfInactiveSitesPerHealthBoardLimitExceeded() == false)
                //{
                //    // Send inactive reports
                //    reportInactiveSites.SendInactiveReports();
                //}
                Dictionary<String, Int16> countOfInactiveSitesPerHealthboard = reportInactiveSites.GetCountOfInactiveSitesPerHealthboard();

                List<String> healthboardsWithTooManyInactiveSitesToReport = new List<string>();

                int inactiveSitesLimitPerHealthboard = 15;  // Config file!!!

                foreach (KeyValuePair<String, Int16> value in countOfInactiveSitesPerHealthboard)
                {
                    if (value.Value >= inactiveSitesLimitPerHealthboard)
                    {
                        healthboardsWithTooManyInactiveSitesToReport.Add(value.Key);
                        _log.Add("WARNING: Healthboard limit exceeded for : " + value.Key + " : number of inactive sites: " + value.Value.ToString());
                    }
                }

                reportInactiveSites.SendInactiveReports(healthboardsWithTooManyInactiveSitesToReport);
            }
            else   // Ignore limit - send inactive email reports regardless of how many inactive sites there are
            {
                
                // Send inactive reports
                reportInactiveSites.SendInactiveReports();
            }
        }

        /// <summary>
        /// Check that the current day is not Sunday or Monday. 
        /// As this app checks the previous 24 hours activity, running it on either of these days will generate a report based on weekend data
        /// which would mean that many sites which were just shut ovcr the weekend would be reported as inactive
        /// </summary>
        /// <returns></returns>
        private bool RunningCheckWillPickUpWeekendActivity()
        {
            if ((DateTime.Today.DayOfWeek == DayOfWeek.Sunday) || (DateTime.Today.DayOfWeek == DayOfWeek.Monday))
            {
                return true;
            }
            return false;
        }
    }
}
