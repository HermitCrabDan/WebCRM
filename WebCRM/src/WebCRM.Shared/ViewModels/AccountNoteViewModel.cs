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
    public class AccountNoteViewModel: CRMViewModelBase<AccountNote>
    {
        public AccountNoteViewModel() {}

        public AccountNoteViewModel(AccountNote model)
        {
            this.ValidationErrorMessages = new List<string>();
            SetModelValues(model);
        }

        #region AccountNote
        public int AccountID { get; set; }

        public string NoteText { get; set; }
        #endregion

        public override bool IsValid()
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
        
        public override void SetModelValues(AccountNote model)
        {
            this.AccountID = model.AccountID;
            this.NoteText = XSSFilterHelper.FilterForXSS(model.NoteText);
            
            base.SetModelValues(model);
        }

        public override string ToString()
        {
            return $"AccountNoteID:{this.Id},Account:{this.AccountID},By:{this.LastUpdatedBy}-{this.LastUpdatedDate?.ToShortDateString()}";
        }

        public override AccountNote GetBaseModel()
        {
            var model = base.GetBaseModel();

            model.AccountID = this.AccountID;
            model.NoteText = this.NoteText;

            return model;
        }
    }
}