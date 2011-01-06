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
            ReportingEntities db = new ReportingEntities();
            ActivityMonitor.Repository.Repository repository = new ActivityMonitor.Repository.Repository(log, db);
            try
            {
                if (args[0] == null)
                {
                    log.Add("No job was selected.");
                    return;
                } 

                string parameter = args[0];
                if (parameter == "CheckGPActivityForSelectedSuppliers")
                {
                    log.Add("Job to run = CheckGPActivityForSelectedSuppliers, healthboard inactive site limit = true");
                    CheckGPActivityForSelectedSupplier _checkGPActivity = new CheckGPActivityForSelectedSupplier(repository, log, true);
                    _checkGPActivity.RunCheck();
                }
                else if (parameter == "CheckGPActivityForSelectedSuppliersIgnoreHealthboardLimit")
                {
                    log.Add("Job to run = CheckGPActivityForSelectedSuppliers, healthboard inactive site limit = false");
                    CheckGPActivityForSelectedSupplier _checkGPActivity = new CheckGPActivityForSelectedSupplier(repository, log, false);
                    _checkGPActivity.RunCheck();
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
