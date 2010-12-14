﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitor;

namespace ActivityMonitorTests.TestHelpers
{
    public static partial class PopulateTable
    {
        public static tbOrgSupplier AddOrgSupplierDataRow(string organisation, string supplier)
        {
            tbOrgSupplier row = new tbOrgSupplier
            {
                org = organisation,
                epoc = "test",
                supplier = supplier,
                product = "test",
                version = "test",
                latestMsg = DateTime.Today.AddDays(-3),
                X509SerialNumber = "test",
                downloaddate = DateTime.Today.AddDays(-5),
                AuthCertSerialNumber = "test",
                AuthCertDownloadDate = DateTime.Today.AddDays(-5),
                ipAddress = "127.0.0.1",
                reportingSupplier = "test",
                disp = false,
                extended = null,
                previousSupplier = "test",
                previousProduct = "test",
                CmsBetaSite = false,
                EpocCertBy = "test",
                ResignCertBy = "test",
                latestAMS = DateTime.Today.AddDays(-3),
                latestMAS = DateTime.Today.AddDays(-3),
                latestCMS = DateTime.Today.AddDays(-3)
            };

            return row;
        }
    }
}