using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // Return organisation's healthboard value from tbOrganisation
        public string GetOrganisationHealthBoard(string organisation)
        {
            string _healthBoard = null;

            //TODO: Tidy this query!
            _healthBoard = (from Organisation in _ReportingEntity.tbOrganisation
                            where Organisation.id == organisation
                            select Organisation.healthBoardName)
                            .FirstOrDefault();

            if (_healthBoard == null)
            {
                _log.Add("WARNING: No health board was found for organisation: " + organisation);
            }

            return _healthBoard;
        }
    }
}
