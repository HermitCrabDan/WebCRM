namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// CRM Api controller for Member Testimonial Data
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    [ApiController]
    [Route("api/[controller]")]
    public class MemberTestimonialDataController
        :CRMApiControllerBase<MemberTestimonial,MemberTestimonialViewModel, ICRMRepository<MemberTestimonial, MemberTestimonialViewModel>>
    {
        public MemberTestimonialDataController(MemberTestimonialRepository repo, IAppSecurityService security)
            :base(repo, security)
            {
                
            }

        protected override bool CanDelete()
        {
            return false;
        }

        protected override bool CanUpdate()
        {
            if (this._security.IsMember)
            {
                return true;
            }
            return base.CanUpdate();
        }

        protected override Func<MemberTestimonial, bool> RestrictedSelection()
        {
            if (this._security.IsMember)
            {
                return n => n.MemberID == this._security.MemberId;
            }
            return base.RestrictedSelection();
        }

        [HttpPost]
        public override IActionResult Create([FromBody] MemberTestimonialViewModel model)
        {
            model.CreatedBy = this._security.UserID;
            model.CreationDate = DateTime.Now;
            return base.Create(model);
        }

        [HttpPut]
        public override IActionResult Update([FromBody] MemberTestimonialViewModel model)
        {
            
            model.LastedUpdatedBy = this._security.UserID;
            model.LastUpdatedDate = DateTime.Now;
            if (this._security.IsMember)
            {
                if (model.MemberID != this._security.MemberId)
                {
                    return BadRequest(model);
                }
                model.ApprovalDate = null;
                model.ApprovedBy = string.Empty;
            }
            else if(model.ApprovalDate.HasValue && String.IsNullOrWhiteSpace(model.ApprovedBy))
            {
                model.ApprovedBy = this._security.UserID;
            }
            if (model.TestimonialRemovedDate.HasValue && String.IsNullOrWhiteSpace(model.TestimonialRemovedBy))
            {
                model.TestimonialRemovedBy = this._security.UserID;
            }
            return base.Update(model);
        }
    }
}
