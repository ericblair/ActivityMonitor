using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        public bool HasSiteSentAMSGPMessagesSinceYesterday(string organisation)
        {
            DateTime _latestMsg = new DateTime();

            _latestMsg = (from Msg in _ReportingEntity.tbEPS_Msg
                          where Msg.msgTxSenderId == organisation
                          && Msg.msgTypeRid == 21 
                          | Msg.msgTypeRid == 22
                          | Msg.msgTypeRid == 23
                          orderby Msg.datetime descending
                          select Msg.datetime)
                          .FirstOrDefault();

            if (_latestMsg == null)
            {
                _log.Add("No records exists in tbEPS_Msg for site: " + organisation);
                return false;
            }

            if (_latestMsg > DateTime.Today.AddDays(-1))
            {
                _log.Add("Activity has been seen since tbDailyActivityGP was updated for site: " + organisation);
                return true;
            }

            return false;
        }
    }
}
