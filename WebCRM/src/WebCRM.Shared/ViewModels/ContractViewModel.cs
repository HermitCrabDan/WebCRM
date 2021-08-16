namespace WebCRM.Shared
{
    using System;
    using WebCRM.Data;
    using WebCRM.Helpers;
    using System.Collections.Generic;
    /// <summary>
    /// ViewModel for the Contract data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class ContractViewModel: CRMViewModelBase<Contract>
    {
        public ContractViewModel() 
            :base()
        {
        }

        public ContractViewModel(Contract model)
            :base()
        {
            SetModelValues(model);
        }

        #region  Contract
        public int AccountID { get; set; }

        public int MemberID { get; set; }

        public int? OriginalContractID { get; set; }

        public string ContractName { get; set; }

        public DateTime ContractStartDate { get; set; }

        public DateTime ContractEndDate { get; set; }

        public decimal ContractAmount { get; set; }

        public decimal TotalPaidAmount { get; set; }

        public DateTime? LastPaymentRecievedDate { get; set; }
        #endregion

        public string ContractStartDateString { get; set; }

        public string ContractEndDateString { get; set; }

        public string ContractAmountString { get; set; }

        public string TotalPaidAmountString { get; set; }

        public string LastPaymentRecievedDateString { get; set; }

        public override bool IsValid()
        {
            this.ValidationErrorMessages = new List<string>();
            if (this.AccountID <= 0)
            {
                this.ValidationErrorMessages.Add("Invalid Account Id");
            }
            if (this.MemberID <= 0)
            {
                this.ValidationErrorMessages.Add("Invalid Member Id");
            }
            if (String.IsNullOrWhiteSpace(this.ContractName))
            {
                this.ValidationErrorMessages.Add("Contract Name cannot be blank");
            }
            return this.AccountID > 0 && this.MemberID > 0 && !String.IsNullOrWhiteSpace(this.ContractName);
        }

        public override void SetModelValues(Contract model)
        {
            this.AccountID = model.AccountID;
            this.ContractAmount = model.ContractAmount;
            this.ContractEndDate = model.ContractEndDate;
            this.ContractName = XSSFilterHelper.FilterForXSS(model.ContractName);
            this.ContractStartDate = model.ContractStartDate;
            this.LastPaymentRecievedDate = model.LastPaymentRecievedDate;
            this.MemberID = model.MemberID;
            this.OriginalContractID = model.OriginalContractID;
            this.TotalPaidAmount = model.TotalPaidAmount;

            this.ContractAmountString = String.Format("{0:c2}", model.ContractAmount);
            this.TotalPaidAmountString = String.Format("{0:c2}", model.TotalPaidAmount);
            this.LastPaymentRecievedDateString = String.Format("{0:MM-dd-yyyy}", model.LastPaymentRecievedDate);
            this.ContractEndDateString = String.Format("{0:MM-dd-yyyy}", model.ContractStartDate);
            this.ContractEndDateString = String.Format("{0:MM-dd-yyyy}", model.ContractEndDate);
            
            base.SetModelValues(model);
        }
        
        public override string ToString()
        {
            return $"ContractID:{this.Id},Account:{this.AccountID},Member:{this.MemberID},ContractName:{this.ContractName}";
        }

        public override Contract GetBaseModel()
        {
            var model = base.GetBaseModel();

            model.AccountID = this.AccountID;
            model.ContractAmount = this.ContractAmount;
            model.ContractEndDate = this.ContractEndDate;
            model.ContractName = this.ContractName;
            model.ContractStartDate = this.ContractStartDate;
            model.LastPaymentRecievedDate = this.LastPaymentRecievedDate;
            model.MemberID = this.MemberID;
            model.OriginalContractID = this.OriginalContractID;
            model.TotalPaidAmount = this.TotalPaidAmount;

            return model;
        }
    }
}