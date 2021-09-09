namespace WebCRM.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// CRM data model for contract transactions
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [Index(nameof(ContractID))]
    public class ContractTransaction: CRMDataModelBase<ContractTransaction>
    {
        public int ContractID { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal TransactionAmount { get; set; }

        public decimal? FeeAmount { get; set; }

        public int PaymentMonth { get; set; }

        public int PaymentYear { get; set; }

        public override void RestrictedModelUpdate(ContractTransaction model)
        {
            this.PaymentYear = model.PaymentYear;
            this.PaymentMonth = model.PaymentMonth;
            this.FeeAmount = model.FeeAmount;
            this.TransactionDate = model.TransactionDate;
            this.TransactionAmount = model.TransactionAmount;
            base.RestrictedModelUpdate(model);
        }
    }
}