namespace WebCRM.Shared
{
    using System;
    using WebCRM.Data;
    using WebCRM.Helpers;
    using System.Collections.Generic;
    /// <summary>
    /// ViewModel for the Member data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class MemberViewModel: CRMViewModelBase<Member>
    {
        public MemberViewModel() 
            :base()
        {
        }

        public MemberViewModel(Member model)
            :base()
        {
            SetModelValues(model);
        }

        #region Member
        public string MemberName { get; set; }

        public int? UserID { get; set; }
        #endregion

        public override string ToString()
        {
            return $"MemberID:{this.Id},Name:{this.MemberName}";
        }

        public override void SetModelValues(Member model)
        {
            this.MemberName = XSSFilterHelper.FilterForXSS(model.MemberName);
            this.UserID = model.UserID;
            
            base.SetModelValues(model);
        }

        public override bool IsValid()
        {
            bool valid = true;
            this.ValidationErrorMessages = new List<string>();
            
            if (String.IsNullOrWhiteSpace(this.MemberName))
            {
                this.ValidationErrorMessages.Add("Member Name cannot be blank");
                valid = false;
            }
            return valid;
        }

        public override Member GetBaseModel()
        {
            var model = base.GetBaseModel();

            model.MemberName = this.MemberName;
            model.UserID = this.UserID;

            return model;
        }
    }
}
