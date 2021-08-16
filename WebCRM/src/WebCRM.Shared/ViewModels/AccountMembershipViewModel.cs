namespace WebCRM.Shared
{
    using System;
    using WebCRM.Data;
    using WebCRM.Helpers;
    using System.Collections.Generic;
    /// <summary>
    /// ViewModel for the AccountMembership data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class AccountMembershipViewModel: ICRMViewModel<AccountMembership>
    {
        public AccountMembershipViewModel() 
        {
            this.ValidationErrorMessages = new List<string>();
        }

        public AccountMembershipViewModel(AccountMembership model)
        {
            this.ValidationErrorMessages = new List<string>();
            SetModelValues(model);
        }

        #region AccountMembership
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
        #endregion

        public List<string> ValidationErrorMessages { get; set; }

        public bool IsValid()
        {
            this.ValidationErrorMessages = new List<string>();
            if (this.AccountID <= 0)
            {
                this.ValidationErrorMessages.Add("Missing Account Id");
            }
            if (this.MemberID <= 0)
            {
                this.ValidationErrorMessages.Add("Missing Member Id");
            }
            return (this.AccountID > 0) && (this.MemberID > 0); 
        }

        public void SetModelValues(AccountMembership model)
        {
            this.AccountID = model.AccountID;
            this.Id = model.Id;
            this.IsPrimaryAccountMember = model.IsPrimaryAccountMember;
            this.MemberID = model.MemberID;

            this.CreationDate = model.CreationDate;
            this.CreatedBy = XSSFilterHelper.FilterForXSS(model.CreatedBy);
            this.LastUpdatedBy = XSSFilterHelper.FilterForXSS(model.LastUpdatedBy);
            this.LastUpdatedDate = model.LastUpdatedDate;
            this.DeletionDate = model.DeletionDate;
            this.DeletionBy = XSSFilterHelper.FilterForXSS(model.DeletionBy);
        }

        public override string ToString()
        {
            return $"AccountMembershipID:{this.Id},Account:{this.AccountID},Member:{this.MemberID}";
        }

        public AccountMembership GetBaseModel()
        {
            return new AccountMembership
            {
                AccountID = this.AccountID,
                Id = this.Id,
                IsPrimaryAccountMember = this.IsPrimaryAccountMember,
                MemberID = this.MemberID,

                LastUpdatedBy = this.LastUpdatedBy,
                LastUpdatedDate = this.LastUpdatedDate,
                CreatedBy = this.CreatedBy,
                CreationDate = this.CreationDate,
                DeletionDate = this.DeletionDate,
                DeletionBy = this.DeletionBy
            };
        }
    }
}