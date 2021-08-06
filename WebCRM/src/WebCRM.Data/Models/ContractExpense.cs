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

        public string ExpenseEnteredBy { get; set; }

        public DateTime ExpenseEnteredDate { get; set; }

        public DateTime? ExpenseCancelDate { get; set; }

        public string ExpenseCanceledBy { get; set; }

        public void RestrictedModelUpdate(ContractExpense model)
        {
            this.ExpenseEnteredBy = model.ExpenseEnteredBy;
            this.ExpenseDate = model.ExpenseDate;
            this.ExpenseAmount = model.ExpenseAmount;
            this.ExpenseCancelDate = model.ExpenseCancelDate;
            this.ExpenseCanceledBy = model.ExpenseCanceledBy;
        }
    }
}