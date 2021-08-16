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
    public class CRMAccountViewModel: CRMViewModelBase<CRMAccount>
    {
        public CRMAccountViewModel() 
            :base()
        {
        }

        public CRMAccountViewModel(CRMAccount model)
            :base()
        {
            SetModelValues(model);
        }

        #region Account
        public string AccountName { get; set; }
        #endregion`

        public override bool IsValid()
        {
            this.ValidationErrorMessages = new List<string>();
            if (String.IsNullOrWhiteSpace(this.AccountName))
            {
                this.ValidationErrorMessages.Add("Must provide an Account name");
            }
            return !String.IsNullOrWhiteSpace(this.AccountName);
        }

        public override void SetModelValues(CRMAccount model)
        {
            this.AccountName = XSSFilterHelper.FilterForXSS(model.AccountName);
            
            base.SetModelValues(model);
        }

        public override string ToString()
        {
            return $"AccountID:{this.Id}, Name:{this.AccountName}";
        }

        public override CRMAccount GetBaseModel()
        {
            var model = base.GetBaseModel();
            model.AccountName = this.AccountName;
            return model;
        }
    }
}