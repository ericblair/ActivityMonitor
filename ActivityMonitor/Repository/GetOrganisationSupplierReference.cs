﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        public string GetOrganisationSupplierReference(string organisation)
        {
            string _supplierRef = null;

            _supplierRef = (from Org in _EPMS_StatisticsContext.tbOrganisation
                            where Org.id == organisation
                            select Org.supplierReference)
                            .FirstOrDefault();

            if (_supplierRef == null)
            {
                _log.Add("WARNING: No supplier reference was found for organisation: " + organisation);
            }

            return _supplierRef;
        }
    }
}