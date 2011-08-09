using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        public bool IsOrganisationActive(string organisation)
        {
            // No AMS messages have been received and site is not a dispensing site
            if ((this.OrganisationHasSentAMSMessages(organisation) == false) && (this.IsOrganisationDispensingSite(organisation) == false))
            {
                DateTime currentDate = DateTime.Now;

                // Check if we need to avoid checking yesterdays data to avoid reporting on weekends
                if (currentDate.DayOfWeek != (DayOfWeek.Sunday | DayOfWeek.Monday))
                {
                    // Check for any activity from the site since tbDailyActivityGP was updated
                    if (!this.HasSiteSentAMSGPMessagesSinceYesterday(organisation))
                        return false;
                }
                else
                {
                    _log.Add("Checking for activity on last week day as current day is: " + currentDate.DayOfWeek.ToString());

                    int daysSinceLastWeekDay = 0;

                    if (currentDate.DayOfWeek == DayOfWeek.Sunday)
                        daysSinceLastWeekDay = 2;
                    else if (currentDate.DayOfWeek == DayOfWeek.Monday)
                        daysSinceLastWeekDay = 3;

                    if (!this.DidSiteSendAMSGPMessagesOnLastWeekDay(organisation, daysSinceLastWeekDay))
                        return false;
                }
                
            }

            // otherwise, site is active or a dispensing site
            return true;
        }
    }
}
