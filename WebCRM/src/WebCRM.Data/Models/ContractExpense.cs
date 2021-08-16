namespace  WebCRM.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// CRM data model for contract expenses
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [Index(nameof(ContractID))]
    public class ContractExpense: ICRMDataModel<ContractExpense>
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public void RestrictedModelUpdate(ContractExpense model)
        {
            this.ExpenseDate = model.ExpenseDate;
            this.ExpenseAmount = model.ExpenseAmount;
            
            this.DeletionDate =  model.DeletionDate;
            this.DeletionBy = model.DeletionBy;
            this.LastUpdatedBy = model.LastUpdatedBy;
            this.LastUpdatedDate = model.LastUpdatedDate;
        }
    }
}