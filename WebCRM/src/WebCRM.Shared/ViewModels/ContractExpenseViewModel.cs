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

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? DeletionDate { get; set; }

        public string DeletionBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public string CreationDateString { get; set; }

        public string LastUpdatedDateString { get; set; }

        public string DeletionDateString { get; set; }
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

            this.CreationDate = model.CreationDate;
            this.CreatedBy = XSSFilterHelper.FilterForXSS(model.CreatedBy);
            this.LastUpdatedBy = XSSFilterHelper.FilterForXSS(model.LastUpdatedBy);
            this.LastUpdatedDate = model.LastUpdatedDate;
            this.DeletionDate = model.DeletionDate;
            this.DeletionBy = XSSFilterHelper.FilterForXSS(model.DeletionBy);

            this.CreationDateString = String.Format("{0:MM-dd-YYYY", model.CreationDate);
            this.LastUpdatedDateString = String.Format("{0:MM-dd-YYYY", model.LastUpdatedDate);
            this.DeletionDateString = String.Format("{0:MM-dd-YYYY", model.DeletionDate);
        }

        public ContractExpense GetBaseModel()
        {
            return new ContractExpense
            {
                ContractID = this.ContractID,
                Id = this.Id,
                ExpenseAmount = this.ExpenseAmount,
                ExpenseDate = this.ExpenseDate,

                LastUpdatedBy = this.LastUpdatedBy,
                LastUpdatedDate = this.LastUpdatedDate,
                CreatedBy = this.CreatedBy,
                CreationDate = this.CreationDate,
                DeletionDate = this.DeletionDate,
                DeletionBy = this.DeletionBy
            };
        }
    }
}