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
    public class ContractExpenseViewModel: CRMViewModelBase<ContractExpense>
    {
        public ContractExpenseViewModel() 
            :base()
        {
        }

        public ContractExpenseViewModel(ContractExpense model)
            :base()
        {
            SetModelValues(model);
        }

        #region ContractExpense
        public int ContractID { get; set; }
        
        public DateTime ExpenseDate { get; set; }

        public decimal ExpenseAmount { get; set; }
        #endregion

        public override bool IsValid()
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

        public override void SetModelValues(ContractExpense model)
        {
            this.ContractID = model.ContractID;
            this.ExpenseAmount = model.ExpenseAmount;
            this.ExpenseDate = model.ExpenseDate;
            
            base.SetModelValues(model);
        }

        public ContractExpense GetBaseModel()
        {
            var model = base.GetBaseModel();

            model.ContractID = this.ContractID;
            model.ExpenseAmount = this.ExpenseAmount;
            model.ExpenseDate = this.ExpenseDate;

            return model;
        }
    }
}