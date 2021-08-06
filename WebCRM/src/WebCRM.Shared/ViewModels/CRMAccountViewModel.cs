namespace WebCRM.Shared
{
    using System;
    using WebCRM.Data;
    using WebCRM.Helpers;
    using System.Collections.Generic;
    /// <summary>
    /// ViewModel For the Account data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class CRMAccountViewModel: ICRMViewModel<CRMAccount>
    {
        public CRMAccountViewModel() 
        {
            this.ValidationErrorMessages = new List<string>();
        }

        public CRMAccountViewModel(CRMAccount model)
        {
            this.ValidationErrorMessages = new List<string>();
            SetModelValues(model);
        }

        #region Account
        public int Id { get; set; }

        public string AccountName { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime? AccountRetirementDate { get; set; }
        #endregion

        public List<string> ValidationErrorMessages { get; set; }

        public bool IsValid()
        {
            this.ValidationErrorMessages = new List<string>();
            if (String.IsNullOrWhiteSpace(this.AccountName))
            {
                this.ValidationErrorMessages.Add("Must provide an Account name");
            }
            return !String.IsNullOrWhiteSpace(this.AccountName);
        }

        public void SetModelValues(CRMAccount model)
        {
            this.Id = model.Id;
            this.AccountName = XSSFilterHelper.FilterForXSS(model.AccountName);
            this.AccountRetirementDate = model.AccountRetirementDate;
            this.CreatedBy = XSSFilterHelper.FilterForXSS(model.CreatedBy);
            this.CreationDate = model.CreationDate;
            this.LastUpdatedBy = XSSFilterHelper.FilterForXSS(model.LastUpdatedBy);
            this.LastUpdatedDate = model.LastUpdatedDate;
        }

        public override string ToString()
        {
            return $"AccountID:{this.Id}, Name:{this.AccountName}";
        }

        public CRMAccount GetBaseModel()
        {
            return new CRMAccount
            {
                Id = this.Id,
                AccountName = this.AccountName,
                AccountRetirementDate = this.AccountRetirementDate,
                CreatedBy = this.CreatedBy,
                CreationDate = this.CreationDate,
                LastUpdatedBy = this.LastUpdatedBy,
                LastUpdatedDate = this.LastUpdatedDate
            };
        }
    }
}