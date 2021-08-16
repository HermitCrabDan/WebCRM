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
        
        public string MemberName { get; set; }

        public string UserID { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? DeletionDate { get; set; }

        public string DeletionBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public string CreationDateString { get; set; }

        public string LastUpdatedDateString { get; set; }

        public string DeletionDateString { get; set; }
        #endregion

        public List<string> ValidationErrorMessages { get; set; }

        public override string ToString()
        {
            return $"MemberID:{this.Id},Name:{this.MemberName}";
        }

        public void SetModelValues(Member model)
        {
            this.Id = model.Id;
            this.MemberName = XSSFilterHelper.FilterForXSS(model.MemberName);
            this.UserID = XSSFilterHelper.FilterForXSS(model.UserID);

            this.CreationDate = model.CreationDate;
            this.CreatedBy = XSSFilterHelper.FilterForXSS(model.CreatedBy);
            this.LastUpdatedBy = XSSFilterHelper.FilterForXSS(model.LastUpdatedBy);
            this.LastUpdatedDate = model.LastUpdatedDate;
            this.DeletionDate = model.DeletionDate;
            this.DeletionBy = XSSFilterHelper.FilterForXSS(model.DeletionBy);

            this.CreationDateString = String.Format("{0:MM-dd-YYYY", model.CreationDate);
            this.LastUpdatedDateString = String.Format("{0:MM-dd-YYYY", model.LastUpdatedDate);
            this.DeletionDateString = String.Format("{0:MM-dd-YYYY", model.DeletionDate);
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
                Id = this.Id,
                MemberName = this.MemberName,
                UserID = this.UserID,

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
