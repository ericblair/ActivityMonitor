using System;
using System.Collections.Generic;

namespace ActivityMonitor
{
    public interface IRepository
    {
        bool ActivityTableHasBeenUpdated();
        bool ActivityTableHasBeenUpdated(string date);
        System.Collections.Generic.List<string> GetAllSuppliers();
        System.Collections.Generic.List<string> GetHealthBoardContactsEmailAddresses(string healthBoard);
        System.Collections.Generic.List<string> GetNewlyInactiveSites();
        // tbGPdailyactivity GetOrganisationActivityDetails(string organisation);
        string GetOrganisationHealthBoard(string organisation);
        System.Collections.Generic.List<string> GetSupplierOrganisations(string supplier);
        string GetOrganisationSupplier(string organisation);
        System.Collections.Generic.List<string> GetSupplierContactsEmailAddresses(string supplier);
        bool IsOrganisationActive(string organisation);
        bool IsOrganisationListedAsInactive(string organisation);
        void MarkOrganisationAsActive(string organisation);
        void RecordDateInactiveWarningEmailWasSent(string organisation);
        void SaveNewlyInactiveOrganisation(string organisation);
        void UpdateInactiveOrganisation(string organisation);
        List<String> GetSuppliersToBeChecked();
        string GetOrganisationSupplierReference(string organisation);
        string GetOrganisationName(string organisation);
        string GetOrganisationLatestMessageDate(string organisation);
        bool OrganisationHasSentAMSMessages(string organisation);
        bool IsOrganisationDispensingSite(string organisation);
        bool HasSiteSentAMSGPMessagesSinceYesterday(string organisation);
        Dictionary<string, DateTime> GetMigratingGPASSSites();
        bool IsOrganisationInMigratingSitesTable(string organisation);
        DateTime? GetOrganisationMigrationDate(string organisation);
        void SetOrganisationMigrationDate(string organisation, DateTime migrationDate);
        void AddNewMigratingSite(string organisation, DateTime migrationDate);
        List<String> GetAllMigratingSites();
        string GetMigratingSiteOriginalSupplier(string organisation);
        void RemoveMigratingSite(string organisation);
        Dictionary<String, DateTime> FindUnnotifiedSitesWithLateMigrations();
        void RecordDateLateMigrationEmailWasSent(string organisation);
        string GetMigratingSiteFutureSupplier(string organisation);
        string GetDateTimeOfLatestAMSMessage(string organisation);
    }
}
