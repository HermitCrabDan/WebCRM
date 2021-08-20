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
    public class ContractTransactionViewModel: CRMViewModelBase<ContractTransaction>
    {
        public ContractTransactionViewModel() 
            :base()
        {
        }

        public ContractTransactionViewModel(ContractTransaction model)
            :base()
        {
            SetModelValues(model);
        }

        #region ContractTransaction
        public int ContractID { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal TransactionAmount { get; set; }
        #endregion

        public string TransactionDateString { get; set; }

        public string TransactionAmountString { get; set; }

        public override bool IsValid()
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
                this.ValidationErrorMessages.Add("Transaction must be within the last 5 years to be entered or updated");
            }
            return this.ContractID > 0 && this.TransactionAmount != 0 && this.TransactionDate > DateTime.Now.AddYears(-5);
        }

        public override void SetModelValues(ContractTransaction model)
        {
            this.ContractID = model.ContractID;
            this.TransactionAmount = model.TransactionAmount;
            this.TransactionDate = model.TransactionDate;

            this.TransactionAmountString = String.Format("${0:N2}", model.TransactionAmount);
            this.TransactionDateString = String.Format("{0:MM-dd-yyyy}", model.TransactionDate);

            base.SetModelValues(model);
        }

        public override string ToString()
        {
            return $"Contract Transaction:{this.TransactionDate.ToShortDateString()},TransactionID:{this.Id},Contract:{this.ContractID},Ammount:{this.TransactionAmount}";
                
        }

        public override ContractTransaction GetBaseModel()
        {
            var model = base.GetBaseModel();

            model.ContractID = this.ContractID;
            model.TransactionAmount = this.TransactionAmount;
            model.TransactionDate = this.TransactionDate;

            return model;
        }
    }
}