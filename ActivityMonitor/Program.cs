using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ActivityMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger log = new Logger();
            EPMS_StatisticsEntities db = new EPMS_StatisticsEntities();
            Repository repository = new Repository(log, db);

            try
            {
                UpdateActivityData updateActivityDate = new UpdateActivityData(repository, log);
                if (updateActivityDate.CheckActivityDataHasBeenUpdated() == false)
                {
                    log.Add("tbGPDailyActivity was not updated yesterday");
                    return;
                }
                updateActivityDate.UpdateData();

                ReportInactiveSites reportInactiveSites = new ReportInactiveSites(repository, log);
                reportInactiveSites.SendInactiveReports();
            }
            catch (Exception ex)
            {
                log.Add("EXCEPTION: \r\n" + "Error Message: " + ex.Message + "\r\n" + "Error Stack Trace: " + ex.StackTrace);
                return;
            }
            finally
            {
                log.Write();
            }
        }
    }
}
