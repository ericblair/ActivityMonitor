using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Diagnostics;

namespace ActivityMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger log = new Logger();
            EPMS_StatisticsEntities db = new EPMS_StatisticsEntities();
            ActivityMonitor.Repository.Repository repository = new ActivityMonitor.Repository.Repository(log, db);
            
            try
            {
                if ((DateTime.Today.DayOfWeek == DayOfWeek.Sunday) || (DateTime.Today.DayOfWeek == DayOfWeek.Monday))
                {
                    log.Add("Running app today would pick up weekend activity. Run aborted.");
                    return;
                }

                UpdateActivityData updateActivityData = new UpdateActivityData(repository, log);
                if (updateActivityData.CheckActivityDataHasBeenUpdated() == false)
                {
                    log.Add("tbGPDailyActivity was not updated yesterday");
                    return;
                }
                updateActivityData.UpdateData();

                // this is probably the best location to insert a check 
                // for the number of emails to be sent

                ReportInactiveSites reportInactiveSites = new ReportInactiveSites(repository, log);
                if (reportInactiveSites.NumberOfInactiveSitesPerHealthBoardLimitExceeded() == false)
                {
                    reportInactiveSites.SendInactiveReports();
                }
            }
            catch (Exception ex)
            {
                log.Add("EXCEPTION: \n\n" + "Error Message: " + ex.Message + "\n\n" + "Error Stack Trace: " + ex.StackTrace + "\n\n" + "Inner Exception: " + ex.InnerException);
                return;
            }
            finally
            {
                log.Write();
                Email _email = new Email(repository, log);
                _email.SendLog();
            }
        }
    }
}
