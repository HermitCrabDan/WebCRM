namespace WebCRM.Shared
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;
    using WebCRM.Data;
    using WebCRM.Shared.ViewModels;

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

        public ReportSummaryViewModel GetSummaryData()
        {
            var summaryData = new ReportSummaryViewModel();

            summaryData.TotalActiveAccounts = this._ctx.CRMAccounts.Where(w => !w.DeletionDate.HasValue).Count();
            summaryData.TotalDeletedAccounts = this._ctx.CRMAccounts.Where(w => w.DeletionDate.HasValue).Count();
            summaryData.TotalActiveMembers = this._ctx.Members.Where(w => !w.DeletionDate.HasValue).Count();
            summaryData.TotalDeletedMembers = this._ctx.Members.Where(w => w.DeletionDate.HasValue).Count();
            summaryData.TotalDeletedContracts = this._ctx.Contracts.Where(w => w.DeletionDate.HasValue).Count();
            summaryData.TotalClosedContracts = this._ctx.Contracts
                .Where(w => !w.DeletionDate.HasValue && (w.TotalPaidAmount >= w.ContractAmount)).Count();

            var activeContractData = this._ctx
                .Contracts
                .Where(w => !w.DeletionDate.HasValue && (w.TotalPaidAmount < w.ContractAmount))
                .ToList()
                .Select(s => new ContractViewModel(s))
                .ToList();
            summaryData.TotalActiveContracts = activeContractData.Count();

            summaryData.ActiveContractAmount = activeContractData.Sum(s => s.ContractAmount);
            summaryData.ActiveContractValueCollected = activeContractData.Sum(s => s.TotalPaidAmount);
            summaryData.ActiveContractValueToBeCollected = summaryData.ActiveContractAmount - summaryData.ActiveContractValueCollected;
            summaryData.ActiveCurrentContracts = activeContractData.Where(w => !w.IsContractDelinquent).Count();
            summaryData.ActiveDelinquentContracts = activeContractData.Where(w => w.IsContractDelinquent).Count();
            summaryData.ActiveContractExpensesPaid = 0;

            return summaryData;
        }

    }
}