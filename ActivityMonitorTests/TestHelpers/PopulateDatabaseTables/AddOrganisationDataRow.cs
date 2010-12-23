using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitor;

namespace ActivityMonitorTests.TestHelpers
{
    public static partial class PopulateTable
    {
        public static tbOrganisation AddOrganisationDataRow(string orgID)
        {
            tbOrganisation row = new tbOrganisation
            {
                rid = 1,
                id = orgID,
                organisationTypeRid = 1,
                shortName = "",
                name = "",
                healthBoardName = "",
                alternateName = "",
                prsRefreshEnabled = true,
                epsServicesEnabled = true,
                startDate = DateTime.Today.AddDays(-10),
                endDate = null,
                archived = false,
                auditCreatedOn = DateTime.Today.AddDays(-10),
                auditCreatedBy = "",
                auditUpdatedOn = null,
                auditUpdatedBy = "",
                address1 = "",
                address2 = "",
                address3 = "",
                address4 = "",
                postCode = "",
                country = "",
                telephone = "",
                fax = "",
                email = "",
                supplier = "",
                supplierReference = "",
                dispensing = false,
                notes = ""
            };

            return row;
        }

        public static tbOrganisation AddOrganisationDataRow(string orgID, bool dispensing)
        {
            if (dispensing == false)
                return AddOrganisationDataRow(orgID);

            tbOrganisation row = new tbOrganisation
            {
                rid = 1,
                id = orgID,
                organisationTypeRid = 1,
                shortName = "",
                name = "",
                healthBoardName = "",
                alternateName = "",
                prsRefreshEnabled = true,
                epsServicesEnabled = true,
                startDate = DateTime.Today.AddDays(-10),
                endDate = null,
                archived = false,
                auditCreatedOn = DateTime.Today.AddDays(-10),
                auditCreatedBy = "",
                auditUpdatedOn = null,
                auditUpdatedBy = "",
                address1 = "",
                address2 = "",
                address3 = "",
                address4 = "",
                postCode = "",
                country = "",
                telephone = "",
                fax = "",
                email = "",
                supplier = "",
                supplierReference = "",
                dispensing = true,
                notes = ""
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

        public static tbOrganisation AddOrganisationDataRow(string orgID, string healthBoard, string supplierReference)
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
                supplierReference = supplierReference,
                dispensing = false,
                notes = "test"
            };

            return row;
        }
    }
}
