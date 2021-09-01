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
    public class AccountMembershipViewModel: CRMViewModelBase<AccountMembership>
    {
        public AccountMembershipViewModel() 
            :base()
        {
        }

        public AccountMembershipViewModel(AccountMembership model)
            :base()
        {
            SetModelValues(model);
        }

        public AccountMembershipViewModel(AccountMembership model, string memberName, string accountName)
            :base()
        {
            SetModelValues(model);
            this.AccountName = XSSFilterHelper.FilterForXSS(accountName);
            this.MemberName = XSSFilterHelper.FilterForXSS(memberName);
        }

        #region AccountMembership
        public int AccountID { get; set; }

        public int MemberID { get; set; }

        public bool IsPrimaryAccountMember { get; set; }
        #endregion

        public string AccountName { get; set; }

        public string MemberName { get; set; }

        public override bool IsValid()
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

        public override void SetModelValues(AccountMembership model)
        {
            this.AccountID = model.AccountID;
            this.IsPrimaryAccountMember = model.IsPrimaryAccountMember;
            this.MemberID = model.MemberID;
            
            base.SetModelValues(model);
        }

        public override string ToString()
        {
            return $"AccountMembershipID:{this.Id},Account:{this.AccountID},Member:{this.MemberID}";
        }

        public override AccountMembership GetBaseModel()
        {
            var model = base.GetBaseModel();

            model.AccountID = this.AccountID;
            model.IsPrimaryAccountMember = this.IsPrimaryAccountMember;
            model.MemberID = this.MemberID;

            return model;
        }
    }
}