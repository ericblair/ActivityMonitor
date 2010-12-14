using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        public string GetSMTPIPAddress()
        {
            string _smtpIP = null;

            _smtpIP = ConfigurationManager.AppSettings["SMTPIPAddress"];

            return _smtpIP;
        }
    }
}
