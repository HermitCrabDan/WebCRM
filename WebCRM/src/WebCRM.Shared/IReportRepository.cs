namespace WebCRM.Shared
{
    using System;
    using WebCRM.Shared.ViewModels;

    /// <summary>
    /// Interface for Report Repository
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public interface IReportRepository
    {
        ReportSummaryViewModel GetSummaryData();

    }
}