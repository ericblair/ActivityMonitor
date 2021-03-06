﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // return organisation's supplier value from tbRPT_OrgSupplier
        public string GetOrganisationSupplier(string organisation)
        {
            string _supplier = null;

            _supplier = (from OrgSupplier in _ReportingEntity.tbRPT_OrgSupplier
                         where OrgSupplier.org == organisation
                         select OrgSupplier.supplier)
                        .FirstOrDefault();

            if (_supplier == null)
            {
                _log.Add("WARNING: No supplier was found for organisation: " + organisation);
            }

            return _supplier;
        }
    }
}
