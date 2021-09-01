namespace WebCRM.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    /// <summary>
    /// CRM data model for accounts
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class CRMAccount : CRMDataModelBase<CRMAccount>
    {
        public string AccountName { get; set; }

        public override void RestrictedModelUpdate(CRMAccount model)
        {
            this.AccountName = model.AccountName;
            base.RestrictedModelUpdate(model);
        }
    }
}