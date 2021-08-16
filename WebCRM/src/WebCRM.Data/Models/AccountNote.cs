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

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? DeletionDate { get; set; }

        public string DeletionBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public void RestrictedModelUpdate(AccountNote model)
        {
            this.NoteText = model.NoteText;
            
            this.DeletionDate =  model.DeletionDate;
            this.DeletionBy = model.DeletionBy;
            this.LastUpdatedBy = model.LastUpdatedBy;
            this.LastUpdatedDate = model.LastUpdatedDate;
        }
    }
}