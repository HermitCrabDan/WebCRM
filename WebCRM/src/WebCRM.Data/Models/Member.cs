namespace WebCRM.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    /// <summary>
    /// CRM data model for members
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class Member: ICRMDataModel<Member>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string MemberName { get; set; }

        public string UserID { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? DeletionDate { get; set; }

        public string DeletionBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public void RestrictedModelUpdate(Member model)
        {
            this.MemberName = model.MemberName;
            this.UserID = model.UserID;
            
            this.DeletionDate =  model.DeletionDate;
            this.DeletionBy = model.DeletionBy;
            this.LastUpdatedBy = model.LastUpdatedBy;
            this.LastUpdatedDate = model.LastUpdatedDate;
        }
    }
}