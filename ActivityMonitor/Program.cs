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
                string parameter = args[0];
                
                if (parameter == "CheckGPActivityForSelectedSuppliers")
                {
                    // Check GP Activity For Selected Suppliers  - will look for any sites running the selected suppliers software and issue an inactive email
                    // report for any sites found to be inactive in the last 24 hours
                    // Note: If more than 10 inactive sites are found for any healthboards then no reports will be sent to the affected healthboard
                    // This allows us to investigate whether the sites are genuinely inactive or whether or not we've tried to report on non-suitable data
                    // (ie bank holiday, etc)

                    log.Add("Job to run = CheckGPActivityForSelectedSuppliers, healthboard inactive site limit = true");
                    CheckGPActivityForSelectedSupplier _checkGPActivity = new CheckGPActivityForSelectedSupplier(repository, log, true);
                    _checkGPActivity.RunCheck();
                }
                else if (parameter == "CheckGPActivityForSelectedSuppliersIgnoreHealthboardLimit")
                {
                    // Check GP Activity For Selected Suppliers - Ignore Healthboard Limit
                    // This will send inactive reports out regardless of how many sites are found to be inactive.

                    log.Add("Job to run = CheckGPActivityForSelectedSuppliers, healthboard inactive site limit = false");
                    CheckGPActivityForSelectedSupplier _checkGPActivity = new CheckGPActivityForSelectedSupplier(repository, log, false);
                    _checkGPActivity.RunCheck();
                }
                else if (parameter == "CheckSitesMigratingFromGpassToEmis")
                {
                    // Check all sites migrating from GPASS to EMIS and send out reports if the migration date expires without the switch being detected

                    log.Add("Job to run = CheckSitesMigratingFromGpassToEmis");
                    CheckMigratingSites _checkMigratingSites = new CheckMigratingSites(repository, log);
                    _checkMigratingSites.UpdateMigratingSitesTable();
                    _checkMigratingSites.CheckForCompletedMigrations();
                    _checkMigratingSites.SendNotificationEmailsForLateMigrations();
                }
                else
                {
                    // this doesn't work. 
                    Console.WriteLine("SupplierAutoEmailer.exe written by Eric");
                    Console.WriteLine("Available parameters:");
                    Console.WriteLine("CheckGPActivityForSelectedSuppliers");
                    Console.WriteLine("CheckGPActivityForSelectedSuppliersIgnoreHealthBoardLimit");
                    Console.WriteLine("CheckSitesMigratingFromGpassToEmis");
                    Console.WriteLine("Refer to documentation for more information.");
                    Console.ReadLine();
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
