namespace WebCRM.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    /// <summary>
    /// CRM data model for contracts
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [Index(nameof(AccountID),nameof(MemberID))]
    public class Contract: ICRMDataModel<Contract>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int AccountID { get; set; }

        public int MemberID { get; set; }

        public int? OriginalContractID { get; set; }

        public string ContractName { get; set; }

        public DateTime ContractStartDate { get; set; }

        public DateTime ContractEndDate { get; set; }

        public decimal ContractAmount { get; set; }

        public decimal TotalPaidAmount { get; set; }

        public DateTime? LastPaymentRecievedDate { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? DeletionDate { get; set; }

        public string DeletionBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public void RestrictedModelUpdate(Contract model)
        {
            this.ContractName = model.ContractName;
            this.LastPaymentRecievedDate = model.LastPaymentRecievedDate;
            this.TotalPaidAmount = model.TotalPaidAmount;
            
            this.DeletionDate =  model.DeletionDate;
            this.DeletionBy = model.DeletionBy;
            this.LastUpdatedBy = model.LastUpdatedBy;
            this.LastUpdatedDate = model.LastUpdatedDate;
        }
    }
}