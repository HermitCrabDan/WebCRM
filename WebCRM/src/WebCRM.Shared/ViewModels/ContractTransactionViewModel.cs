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

        public decimal? FeeAmount { get; set; }

        public int PaymentMonth { get; set; }

        public int PaymentYear { get; set; }
        #endregion

        public string TransactionDateString { get; set; }

        public string TransactionAmountString { get; set; }

        public string FeeAmountString { get; set; }

        public override bool IsValid()
        {
            this.ValidationErrorMessages = new List<string>();
            bool valid = true;

            if (this.ContractID <= 0)
            {
                valid = false;
                this.ValidationErrorMessages.Add("Invalid Contract Id");
            }
            if (this.TransactionAmount == 0 && (!this.FeeAmount.HasValue || this.FeeAmount.Value == 0))
            {
                valid = false;
                this.ValidationErrorMessages.Add("Cannot enter a transaction with a zero amount");
            }
            if (this.PaymentYear <= (DateTime.Now.Year - 10) || this.PaymentYear > (DateTime.Now.Year + 1))
            {
                valid = false;
                this.ValidationErrorMessages.Add("Payment Year must be within the last 10 years");
            }
            if (this.PaymentMonth < 1 || this.PaymentMonth > 12)
            {
                valid = false;
                this.ValidationErrorMessages.Add("Payment Month must be between 1 and 12");
            }

            return valid;
        }

        public override void SetModelValues(ContractTransaction model)
        {
            this.ContractID = model.ContractID;
            this.TransactionAmount = model.TransactionAmount;
            this.TransactionDate = model.TransactionDate;
            this.FeeAmount = model.FeeAmount;
            this.PaymentMonth = model.PaymentMonth;
            this.PaymentYear = model.PaymentYear;

            this.TransactionAmountString = String.Format("${0:N2}", model.TransactionAmount);
            this.TransactionDateString = String.Format("{0:MM-dd-yyyy}", model.TransactionDate);
            this.FeeAmountString = String.Format("${0:N2}", (model.FeeAmount ?? 0));

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
            model.PaymentYear = this.PaymentYear;
            model.PaymentMonth = this.PaymentMonth;
            model.FeeAmount = this.FeeAmount;

            return model;
        }
    }
}