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

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? DeletionDate { get; set; }

        public string DeletionBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public void RestrictedModelUpdate(ContractTransaction model)
        {
            this.TransactionDate = model.TransactionDate;
            this.TransactionAmount = model.TransactionAmount;
            
            this.DeletionDate =  model.DeletionDate;
            this.DeletionBy = model.DeletionBy;
            this.LastUpdatedBy = model.LastUpdatedBy;
            this.LastUpdatedDate = model.LastUpdatedDate;
        }
    }
}