namespace WebCRM.Shared.ViewModels
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class MonthlyContractRemittanceViewModel
    {
        public MonthlyContractRemittanceViewModel()
        {
        }

        public int AccountMembershipID { get; set; }

        public int ContractID { get; set; }

        public DateTime MonthEndDate { get; set; }

        public DateTime FirstPaymentDate { get; set; }

        public DateTime FinalPaymentDate { get; set; }

        public decimal MontlyPayment { get; set; }

        public decimal AmountCollected { get; set; }

        public decimal ExpensesPaid { get; set; }

        public decimal RemmitanceAmount { get; set; }

        public decimal FeesPaid { get; set; }

        public decimal TotalAmountRemaining { get; set; }

        public decimal TotalContractAmount { get; set; }
    }
}
