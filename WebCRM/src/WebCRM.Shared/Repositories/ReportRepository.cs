namespace WebCRM.Shared
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;
    using WebCRM.Data;

    /// <summary>
    /// Repository for report data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class ReportRepository: IReportRepository
    {
        private ILogger _logger;
        private CRMDBContext _ctx;

        public ReportRepository(CRMDBContext ctx) 
        {
            this._ctx = ctx;
        }

        public ReportRepository(ILogger logger, CRMDBContext ctx)
        {
            this._logger = logger;
            this._ctx = ctx;
        }

    }
}