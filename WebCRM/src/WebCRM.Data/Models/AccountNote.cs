namespace WebCRM.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    /// <summary>
    /// CRM data model for account notes
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [Index(nameof(AccountID))]
    public class AccountNote: ICRMDataModel<AccountNote>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int AccountID { get; set; }

        public string NoteText { get; set; }

        public string CreatedBy { get; set; }

        public DateTime NoteCreationDate { get; set; }

        public DateTime? NoteRemovalDate { get; set; }

        public string NoteRemovedBy { get; set; }

        public void RestrictedModelUpdate(AccountNote model)
        {
            this.NoteRemovalDate = model.NoteRemovalDate;
            this.NoteRemovedBy = model.NoteRemovedBy;
            this.NoteText = model.NoteText;
        }
    }
}