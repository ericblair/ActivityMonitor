using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository 
    {
        public string GetDateTimeOfLatestAMSMessage(string organisation)
        {
            DateTime? _latestMsg = new DateTime?();

            _latestMsg = (from Msg in _ReportingEntity.tbEPS_Msg
                          where Msg.msgTxSenderId == organisation
                          && Msg.msgTypeRid == 21
                          | Msg.msgTypeRid == 22
                          | Msg.msgTypeRid == 23
                          orderby Msg.datetime descending
                          select Msg.datetime)
                          .FirstOrDefault();

            if (_latestMsg == DateTime.MinValue)
            {
                _log.Add("No latest AMS message could be found in tbEPS_Msg for site: " + organisation);
                return "(No AMS Messages found)";
            }

            return _latestMsg.ToString();
        }
    }
}
