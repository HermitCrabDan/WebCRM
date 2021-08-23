namespace WebCRM.Shared.ViewModels
{
    using System;

    /// <summary>
    /// View Model for summary report results
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class ReportSummaryViewModel
    {
        public ReportSummaryViewModel()
        {
        }

        public int TotalActiveAccounts { get; set; }

        public int TotalDeletedAccounts { get; set; }

        public int TotalActiveMembers { get; set; }

        public int TotalDeletedMembers { get; set; }

        public int TotalDeletedContracts { get; set; }

        public int TotalClosedContracts { get; set; }

        public int TotalActiveContracts { get; set; }

        public int ActiveDelinquentContracts { get; set; }

        public int ActiveCurrentContracts { get; set; }

        public decimal ActiveContractAmount { get; set; }

        public decimal ActiveContractValueCollected { get; set; }

        public decimal ActiveContractValueToBeCollected { get; set; }

        public decimal ActiveContractExpensesPaid { get; set; }
    }
}
