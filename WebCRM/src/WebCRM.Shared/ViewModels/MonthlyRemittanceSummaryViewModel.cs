namespace WebCRM.Shared.ViewModels
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class MonthlyRemittanceSummaryViewModel
    {
        public MonthlyRemittanceSummaryViewModel()
        {
        }

        public string MonthEndDate { get; set; }

        public string TotalActiveContracts { get; set; }

        public string ActiveContractsTotalMonthlyPayment { get; set; }

        public string ActiveContractsPaymentCollected { get; set; }

        public string ActiveContractsTotalExpensesPaid { get; set; }

        public string ActiveContractsMonthlyRemmitance { get; set; }

        public string ActiveContractsFeesPaidInMonth { get; set; }

        public string ActiveContractsTotalValue { get; set; }

        public string ActiveContractsTotalAmountRemaining { get; set; }

        public string ActiveContractsFirstMonth { get; set; }

        public string ActiveContractsClosingInMonth { get; set; }

        public string ContractsDeletedInMonth { get; set; }
    }
}
