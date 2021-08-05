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
    public class ReportRepository
    {
        private ILogger _logger;

        public ReportRepository() {}

        public ReportRepository(ILogger logger)
        {
            _logger = logger;
        }

    }
}