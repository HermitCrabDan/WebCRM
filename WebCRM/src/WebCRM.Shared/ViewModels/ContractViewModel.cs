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
    public class ContractViewModel: ICRMViewModel<Contract>
    {
        public ContractViewModel() 
        {
            this.ValidationErrorMessages = new List<string>();
        }

        public ContractViewModel(Contract model)
        {
            this.ValidationErrorMessages = new List<string>();
            SetModelValues(model);
        }

        #region  Contract
        public int Id { get; set; }

        public int AccountID { get; set; }

        public int MemberID { get; set; }

        public int? OriginalContractID { get; set; }

        public string ContractName { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ContractStartDate { get; set; }

        public DateTime ContractEndDate { get; set; }

        public decimal ContractAmount { get; set; }

        public decimal TotalPaidAmount { get; set; }

        public DateTime? LastPaymentRecievedDate { get; set; }

        public DateTime? ContractCloseDate { get; set; }

        public string ClosedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string LastUpdatedBy { get; set; }
        #endregion

        public bool IsValid()
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

        public void SetModelValues(Contract model)
        {
            this.AccountID = model.AccountID;
            this.ClosedBy = XSSFilterHelper.FilterForXSS(model.ClosedBy);
            this.ContractAmount = model.ContractAmount;
            this.ContractCloseDate = model.ContractCloseDate;
            this.ContractEndDate = model.ContractEndDate;
            this.Id = model.Id;
            this.ContractName = XSSFilterHelper.FilterForXSS(model.ContractName);
            this.ContractStartDate = model.ContractStartDate;
            this.CreatedBy = XSSFilterHelper.FilterForXSS(model.CreatedBy);
            this.CreationDate = model.CreationDate;
            this.LastPaymentRecievedDate = model.LastPaymentRecievedDate;
            this.LastUpdatedBy = XSSFilterHelper.FilterForXSS(model.LastUpdatedBy);
            this.LastUpdatedDate = model.LastUpdatedDate;
            this.MemberID = model.MemberID;
            this.OriginalContractID = model.OriginalContractID;
            this.TotalPaidAmount = model.TotalPaidAmount;
        }
        public List<string> ValidationErrorMessages { get; set; }
        
        public override string ToString()
        {
            return $"ContractID:{this.Id},Account:{this.AccountID},Member:{this.MemberID},ContractName:{this.ContractName}";
        }

        public Contract GetBaseModel()
        {
            return new Contract
            {
                AccountID = this.AccountID,
                ClosedBy = this.ClosedBy,
                ContractAmount = this.ContractAmount,
                ContractCloseDate = this.ContractCloseDate,
                ContractEndDate = this.ContractEndDate,
                Id = this.Id,
                ContractName = this.ContractName,
                ContractStartDate = this.ContractStartDate,
                CreatedBy = this.CreatedBy,
                CreationDate = this.CreationDate,
                LastPaymentRecievedDate = this.LastPaymentRecievedDate,
                LastUpdatedBy = this.LastUpdatedBy,
                LastUpdatedDate = this.LastUpdatedDate,
                MemberID = this.MemberID,
                OriginalContractID = this.OriginalContractID,
                TotalPaidAmount = this.TotalPaidAmount
            };
        }
    }
}