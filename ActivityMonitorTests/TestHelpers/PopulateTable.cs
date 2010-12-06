using ActivityMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;

namespace ActivityMonitorTests.TestHelpers
{
    public static class PopulateTable
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

        public static tbGPdailyactivity AddGPDailyActivityDataRow(string organisation, int daysToSubtractFromDate)
        {
            tbGPdailyactivity row = new tbGPdailyactivity
            {
                org = organisation,
                date = DateTime.Today.AddDays(-daysToSubtractFromDate),
                amsPrescriptions = 1,
                amsAmendments = 1,
                amsCancellations = 1,
                cmsPrescriptions = 1,
                cmsCancellations = 1,
                cmsUpdatesRequests = 1,
                cmsTreatmentUpdates = 1,
                cmsComplianceUpdates = 1,
                gpRegistrationUpdatesRequests = 1
            };

            return row;
        }

        public static tbHealthBoardContacts AddHealthBoardContactsDataRow(int rid, string healthBoard, string contact)
        {
            tbHealthBoardContacts row = new tbHealthBoardContacts
            {
                Rid = rid,
                HealthBoard = healthBoard,
                Contact = contact
            };

            return row;
        }

        public static tbSupplierContacts AddSupplierContactsDataRow(int rid, string supplier, string contact)
        {
            tbSupplierContacts row = new tbSupplierContacts
            {
                Rid = rid,
                Supplier = supplier,
                Contact = contact
            };

            return row;
        }

        public static tbInactiveSites AddInactiveSitesDataRow(string organisation, Nullable<System.DateTime> dateEmailSent, DateTime dateCreated, Nullable<System.DateTime> dateUpdated)
        {
            tbInactiveSites row = new tbInactiveSites
            {
                Org = organisation,
                DateEmailSent = dateEmailSent,
                DateCreated = dateCreated,
                DateUpdated = dateUpdated
            };

            return row;
        }

        public static tbOrganisation AddOrganisationDataRow(string orgID, string healthBoard)
        {
            tbOrganisation row = new tbOrganisation
            {
                rid = 1,
                id = orgID,
                organisationTypeRid = 1,
                shortName = "test",
                name = "test",
                healthBoardName = healthBoard,
                alternateName = "test",
                prsRefreshEnabled = true,
                epsServicesEnabled = true,
                startDate = DateTime.Today.AddDays(-10),
                endDate = null,
                archived = false,
                auditCreatedOn = DateTime.Today.AddDays(-10),
                auditCreatedBy = "test",
                auditUpdatedOn = null,
                auditUpdatedBy = "test",
                address1 = "test",
                address2 = "test",
                address3 = "test",
                address4 = "test",
                postCode = "test",
                country = "test",
                telephone = "test",
                fax = "test",
                email = "test",
                supplier = "test",
                supplierReference = "test",
                dispensing = false,
                notes = "test"
            };

            return row;
        }
    }
}
