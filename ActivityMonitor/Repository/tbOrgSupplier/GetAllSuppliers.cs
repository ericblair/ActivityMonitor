﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    public partial class Repository
    {
        // This has been replaced with GetSuppliersToBeChecked() which loads values from the config file.
        public List<String> GetAllSuppliers()
        {
            var _suppliers = (from OrgSupplier in _ReportingEntity.tbRPT_OrgSupplier
                              orderby OrgSupplier.supplier
                              select OrgSupplier.supplier)
                             .Distinct();

            if (_suppliers.Count() == 0)
            {
                _log.Add("WARNING: No Suppliers found in tbRPT_OrgSupplier");
                // throw error?
            }

            return _suppliers.ToList<String>();
        }
    }
}
