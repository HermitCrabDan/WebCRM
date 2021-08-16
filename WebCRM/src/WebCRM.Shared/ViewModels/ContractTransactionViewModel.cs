namespace WebCRM.Shared
{
    using System;
    using WebCRM.Data;
    using WebCRM.Helpers;
    using System.Collections.Generic;
    
    /// <summary>
    /// ViewModel for the ContractTransaction data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class ContractTransactionViewModel: ICRMViewModel<ContractTransaction>
    {
        public ContractTransactionViewModel() 
        {
            this.ValidationErrorMessages = new List<string>();
        }

        public ContractTransactionViewModel(ContractTransaction model)
        {
            this.ValidationErrorMessages = new List<string>();
            SetModelValues(model);
        }

        #region ContractTransaction
        public int Id { get; set; }

        public int ContractID { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal TransactionAmount { get; set; }

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
            if (this.TransactionAmount == 0)
            {
                this.ValidationErrorMessages.Add("Cannot enter a transaction with a zero amount");
            }
            if (this.TransactionDate <= DateTime.Now.AddYears(-5))
            {
                this.ValidationErrorMessages.Add("Transaction must be within the last 10 years to be entered or updated");
            }
            return this.ContractID > 0 && this.TransactionAmount != 0 && this.TransactionDate > DateTime.Now.AddYears(-5);
        }

        public void SetModelValues(ContractTransaction model)
        {
            this.ContractID = model.ContractID;
            this.Id = model.Id;
            this.TransactionAmount = model.TransactionAmount;
            this.TransactionDate = model.TransactionDate;

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

        public override string ToString()
        {
            return $"Contract Transaction:{this.TransactionDate.ToShortDateString()},TransactionID:{this.Id},Contract:{this.ContractID},Ammount:{this.TransactionAmount}";
                
        }

        public ContractTransaction GetBaseModel()
        {
            return new ContractTransaction
            {
                ContractID = this.ContractID,
                Id = this.Id,
                TransactionAmount = this.TransactionAmount,
                TransactionDate = this.TransactionDate,

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