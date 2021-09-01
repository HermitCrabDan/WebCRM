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
    public class ContractExpense: CRMDataModelBase<ContractExpense>
    {
        public int ContractID { get; set; }
        
        public DateTime ExpenseDate { get; set; }

        public decimal ExpenseAmount { get; set; }

        public override void RestrictedModelUpdate(ContractExpense model)
        {
            this.ExpenseDate = model.ExpenseDate;
            this.ExpenseAmount = model.ExpenseAmount;
            base.RestrictedModelUpdate(model);
        }
    }
}