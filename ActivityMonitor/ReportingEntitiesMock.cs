//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
// Architectural overview and usage guide: 
// http://blogofrab.blogspot.com/2010/08/maintenance-free-mocking-for-unit.html
//------------------------------------------------------------------------------
using System.Data.EntityClient;
using System.Data.Objects;
using ActivityMonitor.ReportingEntitiesMockObjectSet;
using System.Diagnostics;

namespace ActivityMonitor
{
    /// <summary>
    /// The concrete mock context object that implements the context's interface.
    /// Provide an instance of this mock context class to client logic when testing, 
    /// instead of providing a functional context object.
    /// </summary>
    public partial class ReportingEntitiesMock : IReportingEntities
    {
        public IObjectSet<tbRPT_DailyActivityGP> tbRPT_DailyActivityGP
        {
            get { return _tbRPT_DailyActivityGP  ?? (_tbRPT_DailyActivityGP = new MockObjectSet<tbRPT_DailyActivityGP>()); }
        }
        private IObjectSet<tbRPT_DailyActivityGP> _tbRPT_DailyActivityGP;
        public IObjectSet<tbEPS_Msg> tbEPS_Msg
        {
            get { return _tbEPS_Msg  ?? (_tbEPS_Msg = new MockObjectSet<tbEPS_Msg>()); }
        }
        private IObjectSet<tbEPS_Msg> _tbEPS_Msg;
        public IObjectSet<tbEPS_Organisation> tbEPS_Organisation
        {
            get { return _tbEPS_Organisation  ?? (_tbEPS_Organisation = new MockObjectSet<tbEPS_Organisation>()); }
        }
        private IObjectSet<tbEPS_Organisation> _tbEPS_Organisation;
        public IObjectSet<tbRPT_HealthBoardContacts> tbRPT_HealthBoardContacts
        {
            get { return _tbRPT_HealthBoardContacts  ?? (_tbRPT_HealthBoardContacts = new MockObjectSet<tbRPT_HealthBoardContacts>()); }
        }
        private IObjectSet<tbRPT_HealthBoardContacts> _tbRPT_HealthBoardContacts;
        public IObjectSet<tbRPT_InactiveSites> tbRPT_InactiveSites
        {
            get { return _tbRPT_InactiveSites  ?? (_tbRPT_InactiveSites = new MockObjectSet<tbRPT_InactiveSites>()); }
        }
        private IObjectSet<tbRPT_InactiveSites> _tbRPT_InactiveSites;
        public IObjectSet<tbRPT_MigratingSites> tbRPT_MigratingSites
        {
            get { return _tbRPT_MigratingSites  ?? (_tbRPT_MigratingSites = new MockObjectSet<tbRPT_MigratingSites>()); }
        }
        private IObjectSet<tbRPT_MigratingSites> _tbRPT_MigratingSites;
        public IObjectSet<tbRPT_OrgSupplier> tbRPT_OrgSupplier
        {
            get { return _tbRPT_OrgSupplier  ?? (_tbRPT_OrgSupplier = new MockObjectSet<tbRPT_OrgSupplier>()); }
        }
        private IObjectSet<tbRPT_OrgSupplier> _tbRPT_OrgSupplier;
        public IObjectSet<tbRPT_SupplierContacts> tbRPT_SupplierContacts
        {
            get { return _tbRPT_SupplierContacts  ?? (_tbRPT_SupplierContacts = new MockObjectSet<tbRPT_SupplierContacts>()); }
        }
        private IObjectSet<tbRPT_SupplierContacts> _tbRPT_SupplierContacts;
        public IObjectSet<tbRPT_PRSErrorMonitor> tbRPT_PRSErrorMonitor
        {
            get { return _tbRPT_PRSErrorMonitor  ?? (_tbRPT_PRSErrorMonitor = new MockObjectSet<tbRPT_PRSErrorMonitor>()); }
        }
        private IObjectSet<tbRPT_PRSErrorMonitor> _tbRPT_PRSErrorMonitor;

        public int SaveChanges() { return 0; }   
        public void DeleteObject(object entity)
        {
            // Grab the name of the calling method and action as appropriate

            StackTrace stackTrace = new StackTrace();
            string callingMethod = stackTrace.GetFrame(1).GetMethod().Name;

            if (callingMethod == "MarkOrganisationAsActive")
            {
                tbRPT_InactiveSites _organisation = (tbRPT_InactiveSites)entity;
                this._tbRPT_InactiveSites.DeleteObject(_organisation);
            }
            else if (callingMethod == "RemoveMigratingSite")
            {
                tbRPT_MigratingSites _organisation = (tbRPT_MigratingSites)entity;
                this._tbRPT_MigratingSites.DeleteObject(_organisation);
            }
        }  
    }
}
