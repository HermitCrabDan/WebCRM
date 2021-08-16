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
    public class MemberTestimonialViewModel: CRMViewModelBase<MemberTestimonial>
    {
        public MemberTestimonialViewModel() 
            :base()
        {
        }

        public MemberTestimonialViewModel(MemberTestimonial model)
            :base()
        {
            SetModelValues(model);
        }

        #region MemberTestimonial
        public int MemberID { get; set; }

        public string TestimonialText { get; set; }

        public int? TestimonialClipStart { get; set; }

        public int? TestimonialClipEnd { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public string ApprovedBy { get; set; }
        #endregion

        public string ApprovalDateString { get; set; }

        /// <summary>
        /// Clipped Text
        /// </summary>
        /// <value>Calculated from the testimonial text with the clip start and end values</value>
        public string ClippedString { get; set; }

        public override bool IsValid()
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

        public override void SetModelValues(MemberTestimonial model)
        {
            this.ApprovalDate = model.ApprovalDate;
            this.ApprovedBy = XSSFilterHelper.FilterForXSS(model.ApprovedBy);
            this.MemberID = model.MemberID;
            this.TestimonialClipEnd = model.TestimonialClipEnd;
            this.TestimonialClipStart = model.TestimonialClipStart;
            this.TestimonialText = XSSFilterHelper.FilterForXSS(model.TestimonialText);

            this.ApprovalDateString = String.Format("{0:MM-dd-yyyy}", model.ApprovalDate);
            
            base.SetModelValues(model);
            
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

        public override MemberTestimonial GetBaseModel()
        {
            var model = base.GetBaseModel();

            model.ApprovalDate = this.ApprovalDate;
            model.ApprovedBy = this.ApprovedBy;
            model.MemberID = this.MemberID;
            model.TestimonialClipEnd = this.TestimonialClipEnd;
            model.TestimonialClipStart = this.TestimonialClipStart;
            model.TestimonialText = this.TestimonialText;

            return model;
        }
    }
}