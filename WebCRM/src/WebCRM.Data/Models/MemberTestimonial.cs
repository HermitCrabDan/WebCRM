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

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public int? TestimonialClipStart { get; set; }

        public int? TestimonialClipEnd { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public string ApprovedBy { get; set; }

        public DateTime? TestimonialRemovedDate { get; set; }

        public string TestimonialRemovedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string LastedUpdatedBy { get; set; }

        public void RestrictedModelUpdate(MemberTestimonial model)
        {
            this.ApprovalDate = model.ApprovalDate;
            this.ApprovedBy = model.ApprovedBy;
            this.TestimonialClipStart = model.TestimonialClipStart;
            this.TestimonialClipEnd = model.TestimonialClipEnd;
            this.LastedUpdatedBy = model.LastedUpdatedBy;
            this.LastUpdatedDate = model.LastUpdatedDate;
            this.TestimonialRemovedDate = model.TestimonialRemovedDate;
            this.TestimonialRemovedBy = model.TestimonialRemovedBy;
            this.TestimonialText = model.TestimonialText;
        }
    }
}
