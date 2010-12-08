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
        private IEPMS_StatisticsEntities _EPMS_StatisticsContext;
        private ILogger _log;

        public Repository(ILogger log, IEPMS_StatisticsEntities context)
        {
            _log = log;
            _EPMS_StatisticsContext = context;
        }
    }
}
