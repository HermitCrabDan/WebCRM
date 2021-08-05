namespace WebCRM.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    /// <summary>
    /// CRM data model for accounts
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class CRMAccount : ICRMDataModel<CRMAccount>
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string AccountName { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime? AccountRetirementDate { get; set; }

        public void RestrictedModelUpdate(Account model)
        {
            this.AccountName = model.AccountName;
            this.AccountRetirementDate =  model.AccountRetirementDate;
            this.LastUpdatedBy = model.LastUpdatedBy;
            this.LastUpdatedDate = model.LastUpdatedDate;
        }
    }
}