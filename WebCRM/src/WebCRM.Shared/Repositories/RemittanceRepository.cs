namespace WebCRM.Shared.Repositories
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using WebCRM.Data;
    using WebCRM.Shared.ViewModels;

    /// <summary>
    /// 
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class RemittanceRepository
    {
        private CRMDBContext _ctx;

        public RemittanceRepository(CRMDBContext ctx)
        {
            this._ctx = ctx;
        }

        public List<MonthlyRemittanceSummaryViewModel> GetMonthlyRemittanceSummary(int remittanceYear)
        {
            var remittanceSummaries = new List<MonthlyRemittanceSummaryViewModel>();

            if (remittanceYear <= DateTime.Now.Year)
            {
                int maxMonth = (remittanceYear == DateTime.Now.Year) ? DateTime.Now.Month - 1 : 12;

                for (int month = 1; month <= maxMonth; month++)
                {
                    var monthlySummary = new MonthlyRemittanceSummaryViewModel();
                    var monthEndDate = new DateTime(remittanceYear, month, 1).AddMonths(1).AddDays(-1);

                    monthlySummary.MonthEndDate = monthEndDate.ToShortDateString();

                    var activeContracts = (
                            from c in this._ctx.Contracts.Where(w => !w.DeletionDate.HasValue
                                && w.ContractStartDate <= monthEndDate
                                && w.ContractEndDate >= monthEndDate).AsEnumerable()
                            join t in this._ctx.ContractTransactions.Where(w => !w.DeletionDate.HasValue
                                && w.PaymentMonth == month
                                && w.PaymentYear == remittanceYear).AsEnumerable()
                            on c.Id equals t.ContractID into transactionData
                            join e in this._ctx.ContractExpenses.Where(w => !w.DeletionDate.HasValue
                                && w.ExpenseDate.Year == remittanceYear
                                && w.ExpenseDate.Month == month).AsEnumerable()
                            on c.Id equals e.ContractID into expenseData
                            from td in transactionData.DefaultIfEmpty()
                            from ed in expenseData.DefaultIfEmpty()
                            select new
                            {
                                Id = c.Id,
                                ContractData = c,
                                TransactionData = td,
                                ExpenseData = ed
                            }
                        )
                        .ToList()
                        .GroupBy(g => g.Id)
                        .Select(s => new
                        {
                            contract = s.FirstOrDefault().ContractData,
                            transactionList = s
                                .Where(w => w.TransactionData != null)
                                .Select(f => f.TransactionData)
                                .ToList(),
                            expenseData = s
                                .Where(w => w.ExpenseData != null)
                                .Select(f => f.ExpenseData)
                                .ToList()

                        })
                        .ToList();

                    monthlySummary.TotalActiveContracts = String.Format("{0:N0}", activeContracts.Count);

                    monthlySummary.ActiveContractsTotalValue = String.Format("${0:N2}",
                        activeContracts.Sum(s => s.contract.ContractAmount));

                    var paymentsCollected = activeContracts.Sum(s =>
                            (s.transactionList.Count > 0) ?
                            s.transactionList.Sum(s => s.TransactionAmount)
                            : 0);
                    var expensesPaid =
                        activeContracts.Sum(s =>
                            (s.expenseData.Count > 0) ?
                            s.expenseData.Sum(s => s.ExpenseAmount)
                            : 0);
                    monthlySummary.ActiveContractsPaymentCollected = String.Format("${0:N2}", paymentsCollected);
                    monthlySummary.ActiveContractsTotalExpensesPaid = String.Format("${0:N2}", expensesPaid);

                    monthlySummary.ActiveContractsMonthlyRemmitance = String.Format("${0:N2}"
                        , (paymentsCollected - expensesPaid));

                    monthlySummary.ActiveContractsFeesPaidInMonth = String.Format("${0:N2}",
                        activeContracts.Sum(s =>
                            (s.transactionList.Count > 0)?
                            s.transactionList.Sum(s => s.FeeAmount ?? 0)
                            :0));
                    monthlySummary.ActiveContractsTotalAmountRemaining = String.Format("${0:N2}",
                        activeContracts.Sum(s => s.contract.ContractAmount - s.contract.TotalPaidAmount));

                    monthlySummary.ActiveContractsFirstMonth = String.Format("{0:N0}",
                        activeContracts.Where(w => w.contract.ContractStartDate.Year == remittanceYear
                            && w.contract.ContractStartDate.Month == month).Count());

                    monthlySummary.ActiveContractsClosingInMonth = String.Format("{0:N0}",
                        activeContracts.Where(w => w.contract.ContractEndDate.Year == remittanceYear
                            && w.contract.ContractEndDate.Month == month).Count());

                    monthlySummary.ContractsDeletedInMonth = String.Format("{0:N0}", this._ctx
                        .Contracts
                        .Where(w =>
                            w.DeletionDate.HasValue
                            && w.DeletionDate.Value.Year == remittanceYear
                            && w.DeletionDate.Value.Month == month)
                        .Count());

                    remittanceSummaries.Add(monthlySummary);
                }
            }

            return remittanceSummaries;
        }
    }
}
