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

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? DeletionDate { get; set; }

        public string DeletionBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public void RestrictedModelUpdate(AccountMembership model)
        {
            this.IsPrimaryAccountMember = model.IsPrimaryAccountMember;
            
            this.DeletionDate =  model.DeletionDate;
            this.DeletionBy = model.DeletionBy;
            this.LastUpdatedBy = model.LastUpdatedBy;
            this.LastUpdatedDate = model.LastUpdatedDate;
        }
    }
}