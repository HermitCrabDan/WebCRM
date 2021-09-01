namespace WebCRM.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    /// <summary>
    /// CRM data model for account notes
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [Index(nameof(AccountID))]
    public class AccountNote: CRMDataModelBase<AccountNote>
    {
        public int AccountID { get; set; }

        public string NoteText { get; set; }

        public override void RestrictedModelUpdate(AccountNote model)
        {
            this.NoteText = model.NoteText;
            base.RestrictedModelUpdate(model);
        }
    }
}