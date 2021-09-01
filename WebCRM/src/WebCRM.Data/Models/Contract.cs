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
    [Index(nameof(AccountMembershipID))]
    public class Contract:CRMDataModelBase<Contract> 
    {   
        public int AccountMembershipID { get; set; }

        public int? OriginalContractID { get; set; }

        public string ContractName { get; set; }

        public int PaymentDate { get; set; }

        public DateTime ContractStartDate { get; set; }

        public DateTime ContractEndDate { get; set; }

        public decimal ContractAmount { get; set; }

        public decimal TotalPaidAmount { get; set; }

        public DateTime? LastPaymentRecievedDate { get; set; }

        public decimal TotalExpenseAmount { get; set; }

        public DateTime? LastExpensePaymentDate { get; set; }

        public override void RestrictedModelUpdate(Contract model)
        {
            this.ContractName = model.ContractName;
            this.LastPaymentRecievedDate = model.LastPaymentRecievedDate;
            this.TotalPaidAmount = model.TotalPaidAmount;
            this.ContractAmount = model.ContractAmount;
            this.ContractStartDate = model.ContractStartDate;
            this.ContractEndDate = model.ContractEndDate;
            this.TotalExpenseAmount = model.TotalExpenseAmount;
            this.LastExpensePaymentDate = model.LastExpensePaymentDate;
            this.PaymentDate =  model.PaymentDate;
            base.RestrictedModelUpdate(model);
        }
    }
}