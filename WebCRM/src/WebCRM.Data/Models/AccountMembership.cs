namespace WebCRM.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    /// <summary>
    /// CRM data model for account memberships
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [Index(nameof(AccountID),nameof(MemberID))]
    public class AccountMembership: CRMDataModelBase<AccountMembership>
    {   
        public int AccountID { get; set; }

        public int MemberID { get; set; }

        public bool IsPrimaryAccountMember { get; set; }

        public override void RestrictedModelUpdate(AccountMembership model)
        {
            this.MemberID = model.MemberID;
            this.IsPrimaryAccountMember = model.IsPrimaryAccountMember;
            base.RestrictedModelUpdate(model);
        }
    }
}