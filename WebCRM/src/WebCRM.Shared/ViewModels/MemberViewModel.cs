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
    public class MemberViewModel: ICRMViewModel<Member>
    {
        public MemberViewModel() 
        {
            this.ValidationErrorMessages = new List<string>();
        }

        public MemberViewModel(Member model)
        {
            this.ValidationErrorMessages = new List<string>();
            SetModelValues(model);
        }

        #region Member
        public int Id { get; set; }

        /// <summary>
        /// Name of member entered by site manager
        /// </summary>
        /// <value>user input</value>
        public string MemberName { get; set; }

        public DateTime MemberAddedDate { get; set; }

        /// <summary>
        /// Account log in of site manager when added to the system
        /// </summary>
        /// <value>user input</value>
        public string MemberAddedBy { get; set; }

        public DateTime? MemberRemovalDate { get; set; }

        /// <summary>
        /// Account log in of site manager when removing user from system
        /// </summary>
        /// <value>user input</value>
        public string MemberRemovedBy { get; set; }

        /// <summary>
        /// User email address from registration
        /// </summary>
        /// <value>user input</value>
        public string UserID { get; set; }
        #endregion

        public List<string> ValidationErrorMessages { get; set; }

        public override string ToString()
        {
            return $"MemberID:{this.Id},Name:{this.MemberName}";
        }

        public void SetModelValues(Member model)
        {
            this.MemberAddedBy = XSSFilterHelper.FilterForXSS(model.MemberAddedBy);
            this.MemberAddedDate = model.MemberAddedDate;
            this.Id = model.Id;
            this.MemberName = XSSFilterHelper.FilterForXSS(model.MemberName);
            this.MemberRemovalDate = model.MemberRemovalDate;
            this.MemberRemovedBy = XSSFilterHelper.FilterForXSS(model.MemberRemovedBy);
            this.UserID = XSSFilterHelper.FilterForXSS(model.UserID);
        }

        public bool IsValid()
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

        public Member GetBaseModel()
        {
            return new Member
            {
                MemberAddedBy = this.MemberAddedBy,
                MemberAddedDate = this.MemberAddedDate,
                Id = this.Id,
                MemberName = this.MemberName,
                MemberRemovalDate = this.MemberRemovalDate,
                MemberRemovedBy = this.MemberRemovedBy,
                UserID = this.UserID
            };
        }
    }
}
