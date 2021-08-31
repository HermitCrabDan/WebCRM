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

        public string ExpenseAmountString { get; set; }

        public string ExpenseDateString { get; set; }

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
            return this.ContractID > 0 && this.ExpenseAmount != 0;
        
        }

        public override void SetModelValues(ContractExpense model)
        {
            this.ContractID = model.ContractID;
            this.ExpenseAmount = model.ExpenseAmount;
            this.ExpenseDate = model.ExpenseDate;

            this.ExpenseDateString = string.Format("{0:d}", model.ExpenseDate);
            this.ExpenseAmountString = string.Format("${0:N2}", model.ExpenseAmount);
            
            base.SetModelValues(model);
        }

        public override ContractExpense GetBaseModel()
        {
            var model = base.GetBaseModel();

            model.ContractID = this.ContractID;
            model.ExpenseAmount = this.ExpenseAmount;
            model.ExpenseDate = this.ExpenseDate;

            return model;
        }
    }
}