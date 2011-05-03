using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitor;

namespace ActivityMonitorTests.TestHelpers
{
    public static partial class PopulateTable
    {
        public static tbRPT_OrgSupplier AddOrgSupplierDataRow(string organisation, string supplier)
        {
            tbRPT_OrgSupplier row = new tbRPT_OrgSupplier
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

        public static tbRPT_OrgSupplier AddOrgSupplierDataRow(string organisation, string supplier, DateTime? latestAmsMsg)
        {
            tbRPT_OrgSupplier row = new tbRPT_OrgSupplier
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
                latestAMS = latestAmsMsg,
                latestMAS = DateTime.Today.AddDays(-3),
                latestCMS = DateTime.Today.AddDays(-3)
            };

            return row;
        }

        public static tbRPT_OrgSupplier AddOrgSupplierDataRow(string organisation, bool dispensing)
        {
            tbRPT_OrgSupplier row = new tbRPT_OrgSupplier
            {
                org = organisation,
                epoc = "test",
                supplier = "test",
                product = "test",
                version = "test",
                latestMsg = DateTime.Today.AddDays(-3),
                X509SerialNumber = "test",
                downloaddate = DateTime.Today.AddDays(-5),
                AuthCertSerialNumber = "test",
                AuthCertDownloadDate = DateTime.Today.AddDays(-5),
                ipAddress = "127.0.0.1",
                reportingSupplier = "test",
                disp = dispensing,
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

        public static tbRPT_OrgSupplier AddOrgSupplierDataRow(string organisation, string epoc, string supplier, string reportingSupplier)
        {   // Added EPOC parameter as without it the complier wouldn't differentiate between a call to this and a call to 
            // (string organisation, string supplier, DateTime? latestAmsMsg) when the date passed in is null
            tbRPT_OrgSupplier row = new tbRPT_OrgSupplier
            {
                org = organisation,
                epoc = epoc,
                supplier = supplier,
                product = "test",
                version = "test",
                latestMsg = DateTime.Today.AddDays(-3),
                X509SerialNumber = "test",
                downloaddate = DateTime.Today.AddDays(-5),
                AuthCertSerialNumber = "test",
                AuthCertDownloadDate = DateTime.Today.AddDays(-5),
                ipAddress = "127.0.0.1",
                reportingSupplier = reportingSupplier,
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
