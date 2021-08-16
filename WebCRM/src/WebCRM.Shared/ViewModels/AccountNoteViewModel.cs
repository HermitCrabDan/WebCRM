namespace WebCRM.Shared
{
    using System;
    using WebCRM.Data;
    using WebCRM.Helpers;
    using System.Collections.Generic;
    /// <summary>
    /// ViewModel for the AccountNote data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class AccountNoteViewModel: ICRMViewModel<AccountNote>
    {
        public AccountNoteViewModel() {}

        public AccountNoteViewModel(AccountNote model)
        {
            this.ValidationErrorMessages = new List<string>();
            SetModelValues(model);
        }

        #region AccountNote
        public int Id { get; set; }

        public int AccountID { get; set; }

        public string NoteText { get; set; }
        
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

        public bool IsValid()
        {
            this.ValidationErrorMessages = new List<string>();
            if (this.AccountID <= 0)
            {
                this.ValidationErrorMessages.Add("Missing Account Id");
            }
            if (String.IsNullOrWhiteSpace(this.NoteText))
            {
                this.ValidationErrorMessages.Add("Note text cannot be empty");
            }
            return this.AccountID > 0 && !String.IsNullOrWhiteSpace(this.NoteText);
        }
        
        public void SetModelValues(AccountNote model)
        {
            this.AccountID = model.AccountID;
            this.Id = model.Id;
            this.NoteText = XSSFilterHelper.FilterForXSS(model.NoteText);

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

        public override string ToString()
        {
            return $"AccountNoteID:{this.Id},Account:{this.AccountID},By:{this.CreatedBy}-{this.CreationDate.ToShortDateString()}";
        }

        public AccountNote GetBaseModel()
        {
            return new AccountNote
            {
                AccountID = this.AccountID,
                Id = this.Id,
                NoteText = this.NoteText,

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