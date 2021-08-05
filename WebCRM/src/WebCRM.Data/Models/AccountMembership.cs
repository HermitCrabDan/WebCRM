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
    public class AccountMembership: ICRMDataModel<AccountMembership>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int AccountID { get; set; }

        public int MemberID { get; set; }

        public bool IsPrimaryAccountMember { get; set; }

        public DateTime MembershipCreationDate { get; set; }

        public string MembershipCreatedBy { get; set; }

        public DateTime? MembershipRemovalDate { get; set; }

        public string MembershipRemovedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public void RestrictedModelUpdate(AccountMembership model)
        {
            this.IsPrimaryAccountMember = model.IsPrimaryAccountMember;
            this.MembershipRemovalDate =  model.MembershipRemovalDate;
            this.MembershipRemovedBy = model.MembershipRemovedBy;
            this.LastUpdatedBy = model.LastUpdatedBy;
            this.LastUpdatedDate = model.LastUpdatedDate;
        }
    }
}