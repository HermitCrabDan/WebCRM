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

        public string TransactionEnteredBy { get; set; }

        public DateTime TransactionEnteredDate { get; set; }

        public DateTime? TransactionCancelDate { get; set; }

        public string TransactionCanceledBy { get; set; }
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
            this.TransactionEnteredBy = XSSFilterHelper.FilterForXSS(model.TransactionEnteredBy);
            this.TransactionEnteredDate = model.TransactionEnteredDate;
            this.TransactionCancelDate = model.TransactionCancelDate;
            this.TransactionCanceledBy = XSSFilterHelper.FilterForXSS(model.TransactionCanceledBy);
        }

        public override string ToString()
        {
            return (this.TransactionCancelDate.HasValue)?
                $"Canceled:{this.TransactionCancelDate.Value.ToShortDateString()},TransactionID:{this.Id},Contract:{this.ContractID},Ammount:{this.TransactionAmount}"
                :$"Active:{this.TransactionDate.ToShortDateString()},TransactionID:{this.Id},Contract:{this.ContractID},Ammount:{this.TransactionAmount}";
                
        }

        public ContractTransaction GetBaseModel()
        {
            return new ContractTransaction
            {
                ContractID = this.ContractID,
                Id = this.Id,
                TransactionAmount = this.TransactionAmount,
                TransactionDate = this.TransactionDate,
                TransactionEnteredBy = this.TransactionEnteredBy,
                TransactionEnteredDate = this.TransactionEnteredDate,
                TransactionCancelDate = this.TransactionCancelDate,
                TransactionCanceledBy = this.TransactionCanceledBy
            };
        }
    }
}