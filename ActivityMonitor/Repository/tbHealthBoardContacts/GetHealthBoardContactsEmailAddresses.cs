using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // Return all rows from tbRPT_HealthBoardContacts where HealthBoard field matches paramter value
        public List<String> GetHealthBoardContactsEmailAddresses(string healthBoard)
        {
            var _healthBoardContacts = from healthBoardContacts in _ReportingEntity.tbRPT_HealthBoardContacts
                                       where healthBoardContacts.HealthBoard == healthBoard
                                       select healthBoardContacts.Contact;

            if (_healthBoardContacts.Count() == 0)
            {
                _log.Add("WARNING: No contacts could be found in tbRPT_HealthBoardContacts matching value: " + healthBoard);
                // throw error?
            }

            return _healthBoardContacts.ToList<String>();
        }
    }
}
