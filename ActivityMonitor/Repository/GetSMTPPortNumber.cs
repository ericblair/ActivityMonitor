using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        public int GetSMTPPortNumber()
        {
            int _smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPortNumber"]);

            return _smtpPort;
        }
    }
}
