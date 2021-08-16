namespace WebCRM.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    /// <summary>
    /// CRM data model for member testimonials
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [Index(nameof(MemberID))]
    public class MemberTestimonial: ICRMDataModel<MemberTestimonial>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MemberID { get; set; }

        public string TestimonialText { get; set; }

        public int? TestimonialClipStart { get; set; }

        public int? TestimonialClipEnd { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public string ApprovedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? DeletionDate { get; set; }

        public string DeletionBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public void RestrictedModelUpdate(MemberTestimonial model)
        {
            this.ApprovalDate = model.ApprovalDate;
            this.ApprovedBy = model.ApprovedBy;
            this.TestimonialClipStart = model.TestimonialClipStart;
            this.TestimonialClipEnd = model.TestimonialClipEnd;
            this.TestimonialText = model.TestimonialText;
            
            this.DeletionDate =  model.DeletionDate;
            this.DeletionBy = model.DeletionBy;
            this.LastUpdatedBy = model.LastUpdatedBy;
            this.LastUpdatedDate = model.LastUpdatedDate;
        }
    }
}
