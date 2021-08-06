namespace WebCRM.Shared
{
    using System;
    using WebCRM.Data;
    using WebCRM.Helpers;
    using System.Collections.Generic;

    /// <summary>
    /// CRM view model for contract expenses
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class ContractExpenseViewModel: ICRMViewModel<ContractExpense>
    {
        public ContractExpenseViewModel() 
        {
            this.ValidationErrorMessages = new List<string>();
        }

        public ContractExpenseViewModel(ContractExpense model)
        {
            this.ValidationErrorMessages = new List<string>();
            SetModelValues(model);
        }

        #region ContractExpense
        public int Id { get; set; }

        public int ContractID { get; set; }
        
        public DateTime ExpenseDate { get; set; }

        public decimal ExpenseAmount { get; set; }

        public string ExpenseEnteredBy { get; set; }

        public DateTime ExpenseEnteredDate { get; set; }

        public DateTime? ExpenseCancelDate { get; set; }

        public string ExpenseCanceledBy { get; set; }
        #endregion
        
        public List<string> ValidationErrorMessages { get; set; }

        public bool IsValid()
        {
            this.ValidationErrorMessages = new List<string>();     
            if (this.ContractID <= 0)
            {
                this.ValidationErrorMessages.Add("Invalid Contract Id");
            }
            if (this.ExpenseAmount == 0)
            {
                this.ValidationErrorMessages.Add("Cannot enter a expense with a zero amount");
            }
            if (this.ExpenseDate <= DateTime.Now.AddYears(-5))
            {
                this.ValidationErrorMessages.Add("Expense must be within the last 10 years to be entered or updated");
            }
            return this.ContractID > 0 && this.ExpenseAmount != 0 && this.ExpenseDate > DateTime.Now.AddYears(-5);
        
        }

        public void SetModelValues(ContractExpense model)
        {
            this.ContractID = model.ContractID;
            this.Id = model.Id;
            this.ExpenseAmount = model.ExpenseAmount;
            this.ExpenseDate = model.ExpenseDate;
            this.ExpenseCancelDate = model.ExpenseCancelDate;
            this.ExpenseCanceledBy = XSSFilterHelper.FilterForXSS(model.ExpenseCanceledBy);
            this.ExpenseEnteredDate = model.ExpenseEnteredDate;
            this.ExpenseEnteredBy = XSSFilterHelper.FilterForXSS(model.ExpenseEnteredBy);
        }

        public ContractExpense GetBaseModel()
        {
            return new ContractExpense
            {
                ContractID = this.ContractID,
                Id = this.Id,
                ExpenseAmount = this.ExpenseAmount,
                ExpenseDate = this.ExpenseDate,
                ExpenseCancelDate = this.ExpenseCancelDate,
                ExpenseCanceledBy = this.ExpenseCanceledBy,
                ExpenseEnteredDate = this.ExpenseEnteredDate,
                ExpenseEnteredBy = this.ExpenseEnteredBy
            };
        }
    }
}