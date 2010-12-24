using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ActivityMonitor.Repository
{
    partial class Repository : ActivityMonitor.IRepository
    {
        private IReportingEntities _ReportingEntity;
        private ILogger _log;

        public Repository(ILogger log, IReportingEntities context)
        {
            _log = log;
            _ReportingEntity = context;
        }
    }
}
