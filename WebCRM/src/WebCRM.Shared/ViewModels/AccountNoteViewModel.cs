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

        public int Id { get; set; }

        public int AccountID { get; set; }

        public string NoteText { get; set; }

        public string CreatedBy { get; set; }

        public DateTime NoteCreationDate { get; set; }

        public DateTime? NoteRemovalDate { get; set; }

        public string NoteRemovedBy { get; set; }

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
            this.CreatedBy = XSSFilterHelper.FilterForXSS(model.CreatedBy);
            this.NoteCreationDate = model.NoteCreationDate;
            this.NoteRemovalDate = model.NoteRemovalDate;
            this.NoteRemovedBy = XSSFilterHelper.FilterForXSS(model.NoteRemovedBy);
            this.NoteText = XSSFilterHelper.FilterForXSS(model.NoteText);
        }

        public override string ToString()
        {
            return $"AccountNoteID:{this.Id},Account:{this.AccountID},By:{this.CreatedBy}-{this.NoteCreationDate.ToShortDateString()}";
        }

        public AccountNote GetBaseModel()
        {
            return new AccountNote
            {
                AccountID = this.AccountID,
                Id = this.Id,
                CreatedBy = this.CreatedBy,
                NoteCreationDate = this.NoteCreationDate,
                NoteRemovalDate = this.NoteRemovalDate,
                NoteRemovedBy = this.NoteRemovedBy,
                NoteText = this.NoteText
            };
        }
    }
}