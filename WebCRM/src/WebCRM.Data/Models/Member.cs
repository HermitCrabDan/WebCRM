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

        public DateTime MemberAddedDate { get; set; }

        public string MemberAddedBy { get; set; }

        public DateTime? MemberRemovalDate { get; set; }

        public string MemberRemovedBy { get; set; }

        public string UserID { get; set; }

        public void RestrictedModelUpdate(Member model)
        {
            this.MemberRemovalDate = model.MemberRemovalDate;
            this.MemberRemovedBy = model.MemberRemovedBy;
            this.MemberName = model.MemberName;
            this.UserID = model.UserID;
        }
    }
}