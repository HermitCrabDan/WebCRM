namespace WebCRM.Shared
{
    using System;
    using WebCRM.Data;
    using WebCRM.Helpers;
    using System.Collections.Generic;
    /// <summary>
    /// ViewModel for the MemberTestimonial data and implements testimonial clipping
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public class MemberTestimonialViewModel: ICRMViewModel<MemberTestimonial>
    {
        public MemberTestimonialViewModel() 
        {
            this.ValidationErrorMessages = new List<string>();
        }

        public MemberTestimonialViewModel(MemberTestimonial model)
        {
            this.ValidationErrorMessages = new List<string>();
            SetModelValues(model);
        }

        #region MemberTestimonial
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

        public string CreationDateString { get; set; }

        public string LastUpdatedDateString { get; set; }

        public string DeletionDateString { get; set; }
        #endregion
        
        public List<string> ValidationErrorMessages { get; set; }

        /// <summary>
        /// Clipped Text
        /// </summary>
        /// <value>Calculated from the testimonial text with the clip start and end values</value>
        public string ClippedString { get; set; }

        public bool IsValid()
        {
            bool valid = true;
            this.ValidationErrorMessages = new List<string>();
            if (String.IsNullOrWhiteSpace(this.TestimonialText))
            {
                this.ValidationErrorMessages.Add("Testimonial Text cannot be blank");
                return false;
            }
            if (this.TestimonialText.Length < (this.TestimonialClipEnd ?? 0))
            {
                this.ValidationErrorMessages.Add("Clip end is greater than the tesimonial length");
                valid = false;
            }
            if (this.TestimonialText.Length < (this.TestimonialClipStart ?? 0))
            {
                this.ValidationErrorMessages.Add("Clip start is after end of testimonial");
                valid = false;
            }
            if (this.TestimonialClipStart.HasValue 
                && this.TestimonialClipEnd.HasValue 
                && (this.TestimonialClipStart.Value > this.TestimonialClipEnd.Value))
                {
                    this.ValidationErrorMessages.Add("Clip start must be before clip end");
                }
            return valid;
        }

        public void SetModelValues(MemberTestimonial model)
        {
            this.ApprovalDate = model.ApprovalDate;
            this.ApprovedBy = XSSFilterHelper.FilterForXSS(model.ApprovedBy);
            this.MemberID = model.MemberID;
            this.Id = model.Id;
            this.TestimonialClipEnd = model.TestimonialClipEnd;
            this.TestimonialClipStart = model.TestimonialClipStart;
            this.TestimonialText = XSSFilterHelper.FilterForXSS(model.TestimonialText);

            this.CreationDate = model.CreationDate;
            this.CreatedBy = XSSFilterHelper.FilterForXSS(model.CreatedBy);
            this.LastUpdatedBy = XSSFilterHelper.FilterForXSS(model.LastUpdatedBy);
            this.LastUpdatedDate = model.LastUpdatedDate;
            this.DeletionDate = model.DeletionDate;
            this.DeletionBy = XSSFilterHelper.FilterForXSS(model.DeletionBy);

            this.CreationDateString = String.Format("{0:MM-dd-YYYY", model.CreationDate);
            this.LastUpdatedDateString = String.Format("{0:MM-dd-YYYY", model.LastUpdatedDate);
            this.DeletionDateString = String.Format("{0:MM-dd-YYYY", model.DeletionDate);
            
            if (!String.IsNullOrWhiteSpace(model.TestimonialText)
                && (model.TestimonialClipEnd.HasValue || model.TestimonialClipStart.HasValue))
            {
                var clipStart = 
                    (model.TestimonialClipStart.HasValue 
                    && (model.TestimonialClipStart.Value <= model.TestimonialText.Length))?
                        model.TestimonialClipStart.Value
                        :0;
                
                var clipstringLength = 
                    (model.TestimonialClipEnd.HasValue && model.TestimonialClipEnd.Value <= model.TestimonialText.Length)?
                        model.TestimonialClipEnd.Value - clipStart
                        : model.TestimonialText.Length - clipStart;
                this.ClippedString = XSSFilterHelper
                    .FilterForXSS(
                        model.TestimonialText
                        .Substring(clipStart, clipstringLength));
            }
            else 
            {
                this.ClippedString = XSSFilterHelper.FilterForXSS(this.TestimonialText);
            }
        }

        public override string ToString()
        {
            return $"MemberTestimonialID:{this.Id}, Member:{this.MemberID}, Date:{this.CreationDate.ToShortDateString()}";
        }

        public MemberTestimonial GetBaseModel()
        {
            return new MemberTestimonial
            {
                ApprovalDate = this.ApprovalDate,
                ApprovedBy = this.ApprovedBy,
                MemberID = this.MemberID,
                Id = this.Id,
                TestimonialClipEnd = this.TestimonialClipEnd,
                TestimonialClipStart = this.TestimonialClipStart,
                TestimonialText = this.TestimonialText,

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