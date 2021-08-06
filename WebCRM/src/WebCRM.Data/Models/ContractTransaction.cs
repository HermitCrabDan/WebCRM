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
    public class ContractTransaction: ICRMDataModel<ContractTransaction>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ContractID { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal TransactionAmount { get; set; }

        public string TransactionEnteredBy { get; set; }

        public DateTime TransactionEnteredDate { get; set; }

        public DateTime? TransactionCancelDate { get; set; }

        public string TransactionCanceledBy { get; set; }

        public void RestrictedModelUpdate(ContractTransaction model)
        {
            this.TransactionEnteredBy = model.TransactionEnteredBy;
            this.TransactionDate = model.TransactionDate;
            this.TransactionAmount = model.TransactionAmount;
            this.TransactionCancelDate = model.TransactionCancelDate;
            this.TransactionCanceledBy = model.TransactionCanceledBy;
        }
    }
}